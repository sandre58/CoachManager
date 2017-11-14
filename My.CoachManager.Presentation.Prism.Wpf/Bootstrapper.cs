using System.Windows;
using Microsoft.Practices.Unity;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.CrossCutting.Logging.Supervision;
using My.CoachManager.Presentation.Prism.Administration;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Home;
using My.CoachManager.Presentation.Prism.StatusBar;
using My.CoachManager.Presentation.Prism.Wpf.Services;
using My.CoachManager.Presentation.Prism.Wpf.ViewModels;
using My.CoachManager.Presentation.Prism.Wpf.Views;
using My.CoachManager.Presentation.ServiceAgent;
using My.CoachManager.Presentation.ServiceAgent.AdminServiceReference;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;

namespace My.CoachManager.Presentation.Prism.Wpf
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.TryResolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)Shell;
            if (Application.Current.MainWindow != null) Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            // base
            base.ConfigureContainer();

            // Unity
            Container.RegisterType<ILogger, Logger>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());

            // Services
            Container.RegisterInstance<IAdminService>(ServiceClientFactory.Create<AdminServiceClient, IAdminService>(), new ContainerControlledLifetimeManager());

            // ViewModels
            Container.RegisterType<IShellViewModel, ShellViewModel>();

            ViewModelLocationProvider.SetDefaultViewModelFactory(type => Container.Resolve(type));
        }

        /// <summary>
        /// Creates the modules catalog.
        /// </summary>
        /// <returns></returns>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            //return new ConfigurationModuleCatalog();
            var catalog = new ModuleCatalog();
            catalog.AddModule(typeof(StatusBarModule));
            catalog.AddModule(typeof(HomeModule));
            catalog.AddModule(typeof(AdministrationModule));

            return catalog;
        }
    }
}