using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using Microsoft.Practices.Unity;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Modules.Administration;
using My.CoachManager.Presentation.Prism.Wpf.Services;
using My.CoachManager.Presentation.Prism.Wpf.ViewModels;
using My.CoachManager.Presentation.Prism.Wpf.Views;
using My.CoachManager.Presentation.ServiceAgent;
using My.CoachManager.Presentation.ServiceAgent.CategoryServiceReference;
using Prism.Logging;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;

namespace My.CoachManager.Presentation.Prism.Wpf
{
    public class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// The splash screen model.
        /// </summary>
        private readonly SplashScreenViewModel _splashScreenModel;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper"/> class.
        /// </summary>
        /// <param name="splashScreenModel">The splash screen model.</param>
        public Bootstrapper(SplashScreenViewModel splashScreenModel)
        {
            _splashScreenModel = splashScreenModel;
        }

        #endregion Constructors

        #region Configuration

        /// <inheritdoc />
        /// <summary>
        /// Create the Shell of the Application.
        /// </summary>
        /// <returns>The main window.</returns>
        protected override DependencyObject CreateShell()
        {
            _splashScreenModel.Message = "Shell Start Creation";

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var view = Container.TryResolve<Shell>();

            stopwatch.Stop();

            _splashScreenModel.Message = "Shell End Creation : " + stopwatch.ElapsedMilliseconds;

            return view;
        }

        /// <inheritdoc />
        /// <summary>
        /// Configure the unity container.
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            // Container.AddExtension(new IocUnityContainer(Logger as Logger));
            Container.RegisterInstance(typeof(ILogger), Logger);

            // Register Wcf Services
            Container.RegisterInstance(typeof(ICategoryService), ServiceClientFactory.Create<CategoryServiceClient, ICategoryService>());

            // Register Presentation Services
            Container.RegisterType(typeof(INavigationService), typeof(NavigationService), new ContainerControlledLifetimeManager());
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
                ModuleName = typeof(AdministrationModule).Name,
                ModuleType = typeof(AdministrationModule).AssemblyQualifiedName
            });

            Logger.Log("Add the Administration Module", Category.Debug, Priority.None);
        }

        //protected override void ConfigureContainer()
        //{
        //    Locator.SetContainer(Container);

        //    // View Model Locator
        //    ViewModelLocationProvider.SetDefaultViewModelFactory(t => Container.Resolve(t));

        //    // Theme
        //    SkinManager.SkinManager.ApplyTheme("dark");
        //    SkinManager.SkinManager.ApplyAccent("blue");

        //    // Services
        //    Locator.RegisterInstance<ICategoryService>(ServiceClientFactory.Create<CategoryServiceClient, ICategoryService>());
        //    Locator.RegisterInstance<IPositionService>(ServiceClientFactory.Create<PositionServiceClient, IPositionService>());
        //    Locator.RegisterInstance<ISeasonService>(ServiceClientFactory.Create<SeasonServiceClient, ISeasonService>());
        //    Locator.RegisterInstance<IPersonService>(ServiceClientFactory.Create<PersonServiceClient, IPersonService>());
        //    Locator.RegisterInstance<IUserService>(ServiceClientFactory.Create<UserServiceClient, IUserService>());
        //    Locator.RegisterInstance<IRosterService>(ServiceClientFactory.Create<RosterServiceClient, IRosterService>());

        //    // Unity
        //    Locator.RegisterType<ILogger, Logger>();
        //    Locator.RegisterType<IAuthenticationService, AuthenticationService>();
        //    Locator.RegisterType<IDialogService, DialogService>();
        //    Locator.RegisterType<INavigationService, NavigationService>();

        //    // ViewModels
        //    Locator.RegisterType<IMessageViewModel, MessageViewModel>();
        //    Locator.RegisterType<IShellViewModel, ShellViewModel>();

        //    // base
        //    base.ConfigureContainer();
        //}

        ///// <summary>
        ///// Creates the modules catalog.
        ///// </summary>
        ///// <returns></returns>
        //[STAThread]
        //protected override void InitializeModules()
        //{
        //    IModule module = Container.Resolve<SplashScreenModule>();
        //    module.Initialize();

        //    module = Container.Resolve<LoginModule>();
        //    module.Initialize();

        //    ShowSplashScreen();

        //    // Create a new thread for the splash screen to run on
        //    var thread = new Thread(() =>
        //    {
        //        if (ConnectUser())
        //        {
        //            OnLoginSuccess();
        //            Initialize();

        //            System.Windows.Application.Current.Dispatcher.Invoke(
        //                delegate
        //                {
        //                    HideSplashScreen();
        //                    OpenShell();
        //                }
        //            );
        //        }
        //        else
        //        {
        //            OnLoginFailed();
        //        }
        //    });
        //    thread.SetApartmentState(ApartmentState.STA);
        //    thread.IsBackground = true;
        //    thread.Name = "Splash Screen";
        //    thread.Start();
        //}

        #endregion Configuration

        #region Initialisation

        /// <inheritdoc />
        /// <summary>
        /// The configure view model locator.
        /// </summary>
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            InitializeViewModelResolver();
        }

        /// <summary>
        /// Create the logger.
        /// </summary>
        /// <returns>The <see cref="ILoggerFacade"/>.</returns>
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
            _splashScreenModel.Message = "ModulesStartInitialization";

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var actionToLoad = new List<Action>
            {
                LoadSkin
            };

            foreach (var action in actionToLoad)
            {
                action();
                Logger.Log($"Load action : {action.Method.Name}", Category.Debug, Priority.None);
            }

            base.InitializeModules();

            Logger.Log("Initialize modules", Category.Debug, Priority.None);

            stopwatch.Stop();

            _splashScreenModel.Message = "ModulesEndInitialization : " + stopwatch.ElapsedMilliseconds;

            var mainWindow = System.Windows.Application.Current.MainWindow;

            if (mainWindow == null)
            {
                return;
            }

            mainWindow.Show();
            mainWindow.Activate();
            mainWindow.Topmost = true;  // important
            mainWindow.Topmost = false; // important
            mainWindow.Focus();         // important
        }

        /// <inheritdoc />
        /// <summary>
        /// Initialize Shell of the Application.
        /// </summary>
        protected override void InitializeShell()
        {
            _splashScreenModel.Message = "ShellStartInitialization";

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            base.InitializeShell();

            System.Windows.Application.Current.MainWindow = (Shell)Shell;

            stopwatch.Stop();

            _splashScreenModel.Message = "ShellEndInitialization : " + stopwatch.ElapsedMilliseconds;

            Logger.Log("Initialize shell", Category.Debug, Priority.None);
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

        /// <summary>
        /// Load default Skin.
        /// </summary>
        private static void LoadSkin()
        {
            SkinManager.SkinManager.ApplyTheme("dark");
            SkinManager.SkinManager.ApplyAccent("blue");
        }

        #endregion Initialisation

        ///// <summary>
        ///// Initialize the application.
        ///// </summary>
        //private void Initialize()
        //{
        //    // Load the application configuration
        //    EventAggregator.GetEvent<UpdateSplashScreenMessageRequestEvent>().Publish(StatusResources.ApplicationLoading);
        //    //

        //    // Load the application configuration
        //    EventAggregator.GetEvent<UpdateSplashScreenMessageRequestEvent>().Publish(StatusResources.UserLoading);
        //    //

        //    // Initialize the modules
        //    InitializeModule<CoreModule>();
        //    InitializeModule<SettingsModule>();
        //    InitializeModule<AboutModule>();
        //    InitializeModule<StatusBarModule>();
        //    InitializeModule<HomeModule>();

        //    if (Thread.CurrentPrincipal.IsInRole(PermissionConstants.AccessAdmin))
        //        InitializeModule<AdministrationModule>();

        //    InitializeModule<RosterModule>();
        //}

        ///// <summary>
        ///// Connection of the user.
        ///// </summary>
        ///// <returns></returns>
        //private IPrincipal GetConnectedUser(string login = "", string password = "", bool byWindowsCredentials = true)
        //{
        //    EventAggregator.GetEvent<UpdateSplashScreenMessageRequestEvent>().Publish(StatusResources.UserConnection);
        //    IPrincipal principal;

        //    var authentificationService = Container.TryResolve<IAuthenticationService>();
        //    if (byWindowsCredentials)
        //    {
        //        principal = authentificationService.AuthenticateByWindowsCredentials();
        //    }
        //    else
        //    {
        //        principal = authentificationService.Authenticate(login, password);
        //    }

        //    return principal;
        //}

        ///// <summary>
        ///// Connection of the user.
        ///// </summary>
        ///// <returns></returns>
        //private bool ConnectUser()
        //{
        //    IPrincipal principal = null;
        //    AutoResetEvent waitEvent = new AutoResetEvent(false);

        //    if (ConfigurationManager.WindowsAuthentication)
        //    {
        //        principal = GetConnectedUser();
        //    }

        //    if (principal == null)
        //    {
        //        var dialog = Container.TryResolve<IDialogService>();

        //        string defaultUsername;
        //        var defaultPassword = "";
        //        if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
        //        {
        //            defaultUsername = Thread.CurrentPrincipal.Identity.GetLogin();
        //        }
        //        else
        //        {
        //            var currentWindowsIdentity = WindowsIdentity.GetCurrent();
        //            defaultUsername = currentWindowsIdentity.GetLogin();
        //        }

        //        var failedConnection = true;
        //        while (failedConnection)
        //        {
        //            dialog.ShowLoginDialog(defaultUsername.ToUpper(), defaultPassword, string.Empty, e =>
        //                {
        //                    failedConnection = false;
        //                    var loginViewModel = (ILoginViewModel)e.Context;
        //                    if (e.Result == DialogResult.Ok)
        //                    {
        //                        principal = GetConnectedUser(loginViewModel.UserName, loginViewModel.Password, false);

        //                        if (principal == null)
        //                        {
        //                            defaultUsername = loginViewModel.UserName;
        //                            defaultPassword = loginViewModel.Password;
        //                            failedConnection = principal == null;
        //                            dialog.ShowErrorPopup(MessageResources.ConnectionFailed);
        //                        }
        //                    }
        //                    waitEvent.Set();
        //                }
        //            );

        //            waitEvent.WaitOne();
        //        }
        //    }

        //    if (principal != null)
        //    {
        //        AppDomain.CurrentDomain.SetThreadPrincipal(principal);
        //        Thread.CurrentPrincipal = principal;
        //    }

        //    return Thread.CurrentPrincipal.Identity.IsAuthenticated;
        //}

        ///// <summary>
        ///// Open the shell.
        ///// </summary>
        //private void OpenShell()
        //{
        //    System.Windows.Application.Current.MainWindow = (Window)Shell;
        //    if (System.Windows.Application.Current.MainWindow != null) System.Windows.Application.Current.MainWindow.Show();
        //}

        ///// <summary>
        ///// Show the splash screen.
        ///// </summary>
        //public void ShowSplashScreen()
        //{
        //    var splash = Container.Resolve<SplashScreenView>();
        //    if (splash == null) return;
        //    splash.Show();
        //    //System.Windows.Application.Current.Dispatcher.Invoke(
        //    //    delegate
        //    //    {
        //    //        var splash = Container.Resolve<SplashScreenView>();
        //    //        if (splash == null) return;
        //    //        splash.Show();
        //    //    }
        //    //);
        //}

        ///// <summary>
        ///// Hide the splash screen.
        ///// </summary>
        //public void HideSplashScreen()
        //{
        //    var splash = Container.Resolve<SplashScreenView>();
        //    if (splash == null) return;
        //    splash.Visibility = Visibility.Hidden;
        //    //System.Windows.Application.Current.Dispatcher.Invoke(
        //    //    delegate
        //    //    {
        //    //        var splash = Container.Resolve<SplashScreenView>();
        //    //        if (splash == null) return;
        //    //        splash.Hide();
        //    //    }
        //    //);
        //}

        ///// <summary>
        ///// Initialise a module.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        //protected void InitializeModule<T>() where T : IModule
        //{
        //    EventAggregator.GetEvent<UpdateSplashScreenMessageRequestEvent>().Publish(string.Format(StatusResources.ModuleLoading, typeof(T).GetTypeInfo().Name));
        //    IModule module = Container.Resolve<T>();
        //    System.Windows.Application.Current.Dispatcher.Invoke(
        //        delegate
        //        {
        //            module.Initialize();
        //        }
        //    );
        //}

        ///// <summary>
        ///// On loggin failed.
        ///// </summary>
        //protected void OnLoginFailed()
        //{
        //    System.Windows.Application.Current.Dispatcher.Invoke(
        //        delegate
        //        {
        //            System.Windows.Application.Current.Shutdown();
        //        }
        //    );
        //}

        ///// <summary>
        ///// On loggin failed.
        ///// </summary>
        //protected void OnLoginSuccess()
        //{
        //    EventAggregator.GetEvent<UpdateSplashScreenMessageRequestEvent>().Publish(string.Format(StatusResources.UserConnected, Thread.CurrentPrincipal.Identity.Name));
        //}
    }
}