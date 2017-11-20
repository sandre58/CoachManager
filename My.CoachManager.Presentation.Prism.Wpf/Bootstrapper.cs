using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Practices.Unity;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.CrossCutting.Logging.Supervision;
using My.CoachManager.Presentation.Prism.Administration;
using My.CoachManager.Presentation.Prism.Controls.Helpers;
using My.CoachManager.Presentation.Prism.Core.EventAggregator;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Home;
using My.CoachManager.Presentation.Prism.StatusBar;
using My.CoachManager.Presentation.Prism.Wpf.Services;
using My.CoachManager.Presentation.Prism.Wpf.ViewModels;
using My.CoachManager.Presentation.Prism.Wpf.Views;
using My.CoachManager.Presentation.ServiceAgent;
using My.CoachManager.Presentation.ServiceAgent.AdminServiceReference;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;
using SplashScreen = My.CoachManager.Presentation.Prism.Wpf.Views.SplashScreen;

namespace My.CoachManager.Presentation.Prism.Wpf
{
    public class Bootstrapper : UnityBootstrapper
    {
        #region Private Properties

        private IEventAggregator EventAggregator
        {
            get { return Container.Resolve<IEventAggregator>(); }
        }

        #endregion Private Properties

        protected override DependencyObject CreateShell()
        {
            var shell = Container.Resolve<Shell>();
            return shell;
        }

        private async void OpenShell()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(
                (Action)(() =>
                {
                    var splash = Container.Resolve<SplashScreen>();
                    splash.Close();
                }));

            Application.Current.MainWindow = (Window)Shell;
            if (Application.Current.MainWindow != null) Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            // Unity
            Container.RegisterType<ILogger, Logger>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());

            // Services
            Container.RegisterInstance<IAdminService>(ServiceClientFactory.Create<AdminServiceClient, IAdminService>(), new ContainerControlledLifetimeManager());

            // Views
            Container.RegisterType<SplashScreen>(new ContainerControlledLifetimeManager());

            // ViewModels
            Container.RegisterType<ISplashScreenViewModel, SplashScreenViewModel>();
            Container.RegisterType<IShellViewModel, ShellViewModel>();

            // base
            base.ConfigureContainer();
        }

        /// <summary>
        /// Creates the modules catalog.
        /// </summary>
        /// <returns></returns>
        [STAThread]
        protected override void InitializeModules()
        {
            //return new ConfigurationModuleCatalog();

            var thread = new Thread(
                delegate ()
                {
                    ShowSplashScreen();
                    Dispatcher.Run();
                });
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();

            EventAggregator.GetEvent<SplashScreenMessageEvent>().Publish("StatusBarModule");
            Thread.Sleep(2000);
            IModule statusBarModule = Container.Resolve<StatusBarModule>();
            statusBarModule.Initialize();

            EventAggregator.GetEvent<SplashScreenMessageEvent>().Publish("HomeModule");
            Thread.Sleep(2000);
            IModule homeModule = Container.Resolve<HomeModule>();
            homeModule.Initialize();

            EventAggregator.GetEvent<SplashScreenMessageEvent>().Publish("AdministrationModule");
            Thread.Sleep(2000);
            IModule adminModule = Container.Resolve<AdministrationModule>();
            adminModule.Initialize();

            HideSplashScreen();
        }

        public void ShowSplashScreen()
        {
            var splash = Container.Resolve<SplashScreen>();
            if (splash != null)
                splash.Show();
        }

        public void HideSplashScreen()
        {
            var splash = Container.Resolve<SplashScreen>();
            if (splash == null) return;

            if (!splash.Dispatcher.CheckAccess())
            {
                Thread thread = new Thread(
                    delegate ()
                    {
                        splash.Dispatcher.Invoke(
                            DispatcherPriority.Normal,
                            new Action(delegate ()
                                {
                                    Thread.Sleep(2000);
                                    splash.Hide();
                                }
                            ));
                    });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            else
                splash.Hide();
        }
    }
}