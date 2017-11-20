using System;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Windows;
using Microsoft.Practices.Unity;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.CrossCutting.Logging.Supervision;
using My.CoachManager.Presentation.Prism.Administration;
using My.CoachManager.Presentation.Prism.Core.EventAggregator;
using My.CoachManager.Presentation.Prism.Core.Interactivity;
using My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Home;
using My.CoachManager.Presentation.Prism.Resources.Strings;
using My.CoachManager.Presentation.Prism.StatusBar;
using My.CoachManager.Presentation.Prism.Wpf.Services;
using My.CoachManager.Presentation.Prism.Wpf.ViewModels;
using My.CoachManager.Presentation.Prism.Wpf.Views;
using My.CoachManager.Presentation.ServiceAgent;
using My.CoachManager.Presentation.ServiceAgent.AdminServiceReference;
using My.CoachManager.Presentation.ServiceAgent.UserServiceReference;
using Prism.Events;
using Prism.Modularity;
using Prism.Unity;

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

        protected override void ConfigureContainer()
        {
            // Services
            Container.RegisterInstance<IAdminService>(ServiceClientFactory.Create<AdminServiceClient, IAdminService>(), new ContainerControlledLifetimeManager());
            Container.RegisterInstance<IUserService>(ServiceClientFactory.Create<UserServiceClient, IUserService>(), new ContainerControlledLifetimeManager());

            // Unity
            Container.RegisterType<ILogger, Logger>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IAuthenticationService, AuthenticationService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());

            // Views
            Container.RegisterType<Views.SplashScreen>(new ContainerControlledLifetimeManager());

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
            ShowSplashScreen();

            // Create a new thread for the splash screen to run on
            var thread = new Thread(Initialize);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Name = "Splash Screen";
            thread.Start();
        }

        /// <summary>
        /// Initialize the application.
        /// </summary>
        private void Initialize()
        {
            if (Connection())
            {
                InitializeModule<StatusBarModule>();
                InitializeModule<HomeModule>();

                if (Thread.CurrentPrincipal.IsInRole(PermissionConstants.AccessAdmin))
                    InitializeModule<AdministrationModule>();

                System.Windows.Application.Current.Dispatcher.Invoke(
                    delegate
                    {
                        HideSplashScreen();
                        OpenShell();
                    }
                );
            }
            else
            {
            }
        }

        /// <summary>
        /// Connection of the user.
        /// </summary>
        /// <returns></returns>
        private bool ConnectUser(string login = "", string password = "")
        {
            EventAggregator.GetEvent<SplashScreenMessageEvent>().Publish(StatusResources.UserConnection);
            bool isLogged;

            var authentificationServcice = Container.TryResolve<IAuthenticationService>();
            if (string.IsNullOrEmpty(login) && string.IsNullOrEmpty(password))
            {
                isLogged = authentificationServcice.AuthenticateByWindowsCredentials();
            }
            else
            {
                isLogged = authentificationServcice.Authenticate(login, password);
            }

            return isLogged;
        }

        /// <summary>
        /// Connection of the user.
        /// </summary>
        /// <returns></returns>
        private bool Connection()
        {
            bool isConnected = false;

            if (ConfigurationManager.WindowsAuthentication)
            {
                isConnected = ConnectUser();
            }

            if (!isConnected)
            {
                var dialog = Container.TryResolve<IDialogService>();

                HideSplashScreen();

                string defaultUsername;
                var defaultPassword = "";
                if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
                {
                    defaultUsername = Thread.CurrentPrincipal.Identity.GetLogin();
                }
                else
                {
                    var currentWindowsIdentity = WindowsIdentity.GetCurrent();
                    defaultUsername = currentWindowsIdentity.GetLogin();
                }

                dialog.ShowLoginDialog(defaultUsername, defaultPassword, string.Empty, e =>
                        {
                            var loginDialog = (ILoginDialog)e;
                            if (loginDialog.Result == DialogResult.Ok)
                            {
                                ShowSplashScreen();
                                isConnected = ConnectUser(loginDialog.Login, loginDialog.Password);
                                defaultUsername = loginDialog.Login;
                                defaultPassword = loginDialog.Password;
                            }
                            else
                            {
                                OnLogginFailed();
                            }
                        });
            }

            if (isConnected) OnLogginSuccess();

            return isConnected;
        }

        /// <summary>
        /// Open the shell.
        /// </summary>
        private void OpenShell()
        {
            System.Windows.Application.Current.MainWindow = (Window)Shell;
            if (System.Windows.Application.Current.MainWindow != null) System.Windows.Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Show the splash screen.
        /// </summary>
        public void ShowSplashScreen()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(
                delegate
                {
                    var splash = Container.Resolve<Views.SplashScreen>();
                    if (splash == null) return;
                    splash.Show();
                }
            );
        }

        /// <summary>
        /// Hide the splash screen.
        /// </summary>
        public void HideSplashScreen()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(
                delegate
                {
                    var splash = Container.Resolve<Views.SplashScreen>();
                    if (splash == null) return;
                    splash.Hide();
                }
            );
        }

        /// <summary>
        /// Initialise a module.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected void InitializeModule<T>() where T : IModule
        {
            EventAggregator.GetEvent<SplashScreenMessageEvent>().Publish(string.Format(StatusResources.ModuleLoadingMessage, typeof(T).GetTypeInfo().Name));
            IModule module = Container.Resolve<T>();
            System.Windows.Application.Current.Dispatcher.Invoke(
                delegate
                {
                    module.Initialize();
                }
            );
        }

        /// <summary>
        /// On loggin failed.
        /// </summary>
        protected void OnLogginFailed()
        {
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        /// On loggin failed.
        /// </summary>
        protected void OnLogginSuccess()
        {
            EventAggregator.GetEvent<SplashScreenMessageEvent>().Publish(string.Format(StatusResources.UserConnected, Thread.CurrentPrincipal.Identity.Name));
        }
    }
}