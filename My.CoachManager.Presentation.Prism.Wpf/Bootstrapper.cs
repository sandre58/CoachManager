using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.Resources;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Modules.Administration;
using My.CoachManager.Presentation.Prism.Modules.Common;
using My.CoachManager.Presentation.Prism.Modules.Home;
using My.CoachManager.Presentation.Prism.Wpf.Properties;
using My.CoachManager.Presentation.Prism.Wpf.Services;
using My.CoachManager.Presentation.Prism.Wpf.ViewModels;
using My.CoachManager.Presentation.Prism.Wpf.Views;
using My.CoachManager.Presentation.ServiceAgent;
using My.CoachManager.Presentation.ServiceAgent.CategoryServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PersonServiceReference;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;
using My.CoachManager.Presentation.ServiceAgent.SeasonServiceReference;
using My.CoachManager.Presentation.ServiceAgent.UserServiceReference;
using Prism.Logging;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;

namespace My.CoachManager.Presentation.Prism.Wpf
{
    public class Bootstrapper : UnityBootstrapper
    {
        #region Fields

        private readonly SplashScreenViewModel _splashScreenViewModel;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initialise a new instance.
        /// </summary>
        /// <param name="splashScreenViewModel"></param>
        public Bootstrapper(SplashScreenViewModel splashScreenViewModel)
        {
            _splashScreenViewModel = splashScreenViewModel;
        }

        #endregion Constructor

        #region Configuration

        /// <inheritdoc />
        /// <summary>
        /// Create the Shell of the Application.
        /// </summary>
        /// <returns>The main window.</returns>
        protected override DependencyObject CreateShell()
        {
            var view = Container.TryResolve<Shell>();
            return view;
        }

        /// <inheritdoc />
        /// <summary>
        /// Configure the unity container.
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            // Register Logger
            Container.RegisterInstance(typeof(ILogger), Logger);

            // Register Wcf Services
            Container.RegisterInstance(typeof(ICategoryService), ServiceClientFactory.Create<CategoryServiceClient, ICategoryService>());
            Container.RegisterInstance(typeof(IRosterService), ServiceClientFactory.Create<RosterServiceClient, IRosterService>());
            Container.RegisterInstance(typeof(IUserService), ServiceClientFactory.Create<UserServiceClient, IUserService>());
            Container.RegisterInstance(typeof(ISeasonService), ServiceClientFactory.Create<SeasonServiceClient, ISeasonService>());
            Container.RegisterInstance(typeof(IPersonService), ServiceClientFactory.Create<PersonServiceClient, IPersonService>());

            // Register Presentation Services
            Container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<INotificationService, NotificationService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IAuthenticationService, AuthenticationService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IKeyBoardService, KeyboardService>(new ContainerControlledLifetimeManager());
        }

        /// <inheritdoc />
        /// <summary>
        /// Configure the Module Catalog.
        /// </summary>
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            // Add the Common Module
            ModuleCatalog.AddModule(new ModuleInfo
            {
                InitializationMode = InitializationMode.WhenAvailable,
                ModuleName = typeof(CommonModule).Name,
                ModuleType = typeof(CommonModule).AssemblyQualifiedName
            });

            // Add Modules
            ModuleCatalog.AddModule(new ModuleInfo
            {
                InitializationMode = InitializationMode.WhenAvailable,
                ModuleName = typeof(HomeModule).Name,
                ModuleType = typeof(HomeModule).AssemblyQualifiedName
            });
            ModuleCatalog.AddModule(new ModuleInfo
            {
                InitializationMode = InitializationMode.WhenAvailable,
                ModuleName = typeof(AdministrationModule).Name,
                ModuleType = typeof(AdministrationModule).AssemblyQualifiedName
            });
        }

        /// <inheritdoc />
        /// <summary>
        /// The configure view model locator.
        /// </summary>
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            InitializeViewModelResolver();
        }

        #endregion Configuration

        #region Initialisation

        /// <inheritdoc />
        /// <summary>
        /// Create the logger.
        /// </summary>
        /// <returns>The <see cref="T:Prism.Logging.ILoggerFacade" />.</returns>
        protected override ILoggerFacade CreateLogger()
        {
            return CrossCutting.Logging.Supervision.Logger.CreateLogger();
        }

        /// <inheritdoc />
        /// <summary>
        /// The initialize modules.
        /// </summary>
        protected override void InitializeModules()
        {
            var manager = Container.Resolve<IModuleManager>();
            manager.LoadModuleCompleted += Manager_LoadModuleCompleted;

            UpdateSplachMessage(MessageResources.UserConnection);
            if (!ConnectUser()) return;

            base.InitializeModules();

            var actionToLoad = new List<Tuple<Action, string>>
                {
                    new Tuple<Action, string>(LoadSkin, MessageResources.SkinLoading)
                };

            foreach (var action in actionToLoad)
            {
                ExecuteAction(action.Item1, action.Item2);
            }

            OpenShell();
        }

        private void Manager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            Logger.Log($"Module Loaded : {e.ModuleInfo.ModuleName}", Category.Debug, Priority.None);
            UpdateSplachMessage(string.Format(MessageResources.ModuleLoading, e.ModuleInfo.ModuleName));
            // Thread.Sleep(3000);
        }

        /// <inheritdoc />
        /// <summary>
        /// Initialize Shell of the Application.
        /// </summary>
        protected override void InitializeShell()
        {
            base.InitializeShell();
            System.Windows.Application.Current.MainWindow = (Shell)Shell;
        }

        /// <summary>
        /// Initialize the view model resolver.
        /// </summary>
        private void InitializeViewModelResolver()
        {
            // Set the default view model factory
            ViewModelLocationProvider.SetDefaultViewModelFactory(
                viewModelType =>
                {
                    if (!(Container.TryResolve(viewModelType) is ViewModelBase viewModel))
                    {
                        throw new ArgumentException(
                            string.Format(
                                CultureInfo.InvariantCulture,
                                "The ViewModel '{0}' isn't found",
                                viewModelType.Name));
                    }

                    viewModel.Initialize();
                    return viewModel;
                });
        }

        #endregion Initialisation

        #region Methods

        /// <summary>
        /// Load default Skin.
        /// </summary>
        private static void LoadSkin()
        {
            SkinManager.SkinManager.ApplyTheme(Settings.Default.DefaultTheme);
            SkinManager.SkinManager.ApplyAccent(Settings.Default.DefaultAccent);
        }

        /// <summary>
        /// Executes and log an action.
        /// </summary>
        /// <param name="action">The action</param>
        /// <param name="message">The log message.</param>
        private void ExecuteAction(Action action, string message)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            action.Invoke();

            stopwatch.Stop();

            Logger.Log($"Action Loaded : {action.Method.Name} in {stopwatch.ElapsedMilliseconds} ms", Category.Debug, Priority.None);
            UpdateSplachMessage(message);
        }

        /// <summary>
        /// Updates splah message.
        /// </summary>
        /// <param name="message"></param>
        private void UpdateSplachMessage(string message)
        {
            _splashScreenViewModel.UpdateMessage(message);
        }

        /// <summary>
        /// Open the shell.
        /// </summary>
        private static void OpenShell()
        {
            var mainWindow = System.Windows.Application.Current.MainWindow;

            if (mainWindow == null)
            {
                return;
            }

            mainWindow.Show();
            mainWindow.Activate();
            mainWindow.Topmost = true; // important
            mainWindow.Topmost = false; // important
            mainWindow.Focus(); // important
        }

        /// <summary>
        /// Connection of the user.
        /// </summary>
        /// <returns></returns>
        private bool ConnectUser()
        {
            IPrincipal principal = null;

            if (ConfigurationManager.WindowsAuthentication)
            {
                principal = GetConnectedUser();
            }

            if (principal == null)
            {
                // Get Default Credentials
                string defaultUsername;
                var defaultPassword = string.Empty;
                if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
                {
                    defaultUsername = Thread.CurrentPrincipal.Identity.GetLogin();
                }
                else
                {
                    var currentWindowsIdentity = WindowsIdentity.GetCurrent();
                    defaultUsername = currentWindowsIdentity.GetLogin();
                }

                // Show Login Dialog
                if (DialogManager.ShowLoginDialog((newLogin, newPassword) =>
                        {
                            principal = GetConnectedUser(newLogin, newPassword, false);

                            var isConnected = principal != null;

                            if (!isConnected)
                            {
                                NotificationManager.ShowError(MessageResources.ConnectionFailed);
                            }

                            return new Tuple<bool, string>(isConnected, MessageResources.ConnectionFailed);
                        },
                        defaultUsername.ToUpper(), defaultPassword) == DialogResult.Ok)
                {
                    NotificationManager.ShowSuccess(string.Format(MessageResources.UserConnected, principal.Identity.Name));
                }
                else
                {
                    System.Windows.Application.Current.Shutdown();
                }
            }

            if (principal == null) return Thread.CurrentPrincipal.Identity.IsAuthenticated;
            AppDomain.CurrentDomain.SetThreadPrincipal(principal);
            Thread.CurrentPrincipal = principal;

            return Thread.CurrentPrincipal.Identity.IsAuthenticated;
        }

        /// <summary>
        /// Connection of the user.
        /// </summary>
        /// <returns></returns>
        private static IPrincipal GetConnectedUser(string login = "", string password = "", bool byWindowsCredentials = true)
        {
            var authentificationService = ServiceLocator.Current.GetInstance<IAuthenticationService>();

            return byWindowsCredentials ? authentificationService.AuthenticateByWindowsCredentials() : authentificationService.Authenticate(login, password);
        }

        #endregion Methods
    }
}