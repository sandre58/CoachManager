using CommonServiceLocator;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.CrossCutting.Logging.Supervision;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Wpf.Core;
using My.CoachManager.Presentation.Wpf.Core.Dialog;
using My.CoachManager.Presentation.Wpf.Core.Manager;
using My.CoachManager.Presentation.Wpf.Core.Services;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Base;
using My.CoachManager.Presentation.Wpf.Modules.Shared;
using My.CoachManager.Presentation.Wpf.Properties;
using My.CoachManager.Presentation.Wpf.Services;
using My.CoachManager.Presentation.Wpf.ViewModels;
using My.CoachManager.Presentation.Wpf.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Logging;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Regions.Behaviors;
using Prism.Unity;
using Prism.Unity.Ioc;
using Prism.Unity.Regions;
using System;
using System.Globalization;
using System.IO;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using Unity;
using SplashScreen = My.CoachManager.Presentation.Wpf.Views.SplashScreen;

namespace My.CoachManager.Presentation.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private IContainerExtension _containerExtension;
        private IModuleCatalog _moduleCatalog;
        private SplashScreenViewModel _splashScreenViewModel;
        private readonly ILogger _logger;

        /// <summary>
        /// Initialise a new instance of <see cref="App"/>.
        /// </summary>
        public App()
        {
            // Configure logger
            Logger.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/NLog.config"));
            _logger = LoggerFactory.CreateLogger();

            DispatcherUnhandledException += OnAppDispatcherUnhandledException;
            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        /// <summary>
        /// The dependency injection container used to resolve objects
        /// </summary>
        public IContainerProvider Container => _containerExtension;

        protected override async void OnStartup(StartupEventArgs e)
        {
            _logger.Debug("************************ Application Start ************************");

            // Initialise Wep Client Helper
            ApiHelper.InitializeHttpClient(ConfigurationManager.Server);

            _splashScreenViewModel = new SplashScreenViewModel();
            var splashScreen = new SplashScreen(_splashScreenViewModel);

            splashScreen.Show();

            base.OnStartup(e);
            ConfigureViewModelLocator();
            Initialize();
            await InitializeInternal().ContinueWith(r =>
            {
                Dispatcher.Invoke(() =>
                {
                    LoadShell();
                    splashScreen.Hide();
                    OnInitialized();
                });
            });
        }

        /// <summary>
        /// Run the initialization process.
        /// </summary>
        private async Task InitializeInternal()
        {
            await Task.Run(LoadData);
        }

        private void LoadData()
        {
            UpdateSplachMessage(MessageResources.UserConnection);
            if (!ConnectUser()) return;

            _logger.Debug("User connected : " + Thread.CurrentPrincipal.Identity.Name);

            // Load Roster
            UpdateSplachMessage(MessageResources.RosterLoading);
            var roster = ApiHelper.GetData<RosterDto>(ApiConstants.ApiRosters, Thread.CurrentPrincipal.GetRosterId());
            AppManager.InitializeRoster(RosterFactory.Get(roster));

            _logger.Debug("Current roster : " + AppManager.Roster.Name + "[Id=" + AppManager.Roster.Id + "]");
        }

        /// <summary>
        /// Configures the <see cref="ViewModelLocator"/> used by Prism.
        /// </summary>
        protected virtual void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewModelFactory(
                viewModelType =>
                {
                    if (!(Container.Resolve(viewModelType) is ViewModelBase viewModel))
                    {
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The ViewModel '{0}' isn't found", viewModelType.Name));
                    }

                    if (viewModel is DataViewModel screenViewModel)
                    {
                        if (screenViewModel.RefreshOnInit)
                            screenViewModel.Refresh();
                    }
                    return viewModel;
                });
        }

        /// <summary>
        /// Runs the initialization sequence to configure the Prism application.
        /// </summary>
        public virtual void Initialize()
        {
            _containerExtension = CreateContainerExtension();
            _moduleCatalog = CreateModuleCatalog();
            RegisterRequiredTypes(_containerExtension);
            _containerExtension.FinalizeExtension();

            ConfigureServiceLocator();

            ConfigureModuleCatalog(_moduleCatalog);

            var regionAdapterMappings = _containerExtension.Resolve<RegionAdapterMappings>();
            ConfigureRegionAdapterMappings(regionAdapterMappings);

            var defaultRegionBehaviors = _containerExtension.Resolve<IRegionBehaviorFactory>();
            ConfigureDefaultRegionBehaviors(defaultRegionBehaviors);

            RegisterFrameworkExceptionTypes();

            // Load skin
            LoadSkin();
        }

        protected void LoadShell()
        {
            var shell = CreateShell();
            if (shell != null)
            {
                RegionManager.SetRegionManager(shell, _containerExtension.Resolve<IRegionManager>());
                RegionManager.UpdateRegions();
                InitializeShell(shell);
            }

            InitializeModules();
        }

        /// <summary>
        /// Creates the container used by Prism.
        /// </summary>
        /// <returns>The container</returns>
        protected IContainerExtension CreateContainerExtension()
        {
            return new UnityContainerExtension();
        }

        /// <summary>
        /// Creates the <see cref="IModuleCatalog"/> used by Prism.
        /// </summary>
        ///  <remarks>
        /// The base implementation returns a new ModuleCatalog.
        /// </remarks>
        protected virtual IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        /// <summary>
        /// Registers all types that are required by Prism to function with the container.
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected virtual void RegisterRequiredTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(_containerExtension);
            containerRegistry.RegisterInstance(_moduleCatalog);
            containerRegistry.RegisterSingleton<IModuleInitializer, ModuleInitializer>();
            containerRegistry.RegisterSingleton<IModuleManager, ModuleManager>();
            containerRegistry.RegisterSingleton<RegionAdapterMappings>();
            containerRegistry.RegisterSingleton<IRegionManager, RegionManager>();
            containerRegistry.RegisterSingleton<IEventAggregator, EventAggregator>();
            containerRegistry.RegisterSingleton<IRegionViewRegistry, RegionViewRegistry>();
            containerRegistry.RegisterSingleton<IRegionBehaviorFactory, RegionBehaviorFactory>();
            containerRegistry.Register<IRegionNavigationJournalEntry, RegionNavigationJournalEntry>();
            containerRegistry.Register<IRegionNavigationJournal, RegionNavigationJournal>();
            containerRegistry.Register<IRegionNavigationService, RegionNavigationService>();
            containerRegistry.RegisterSingleton<IRegionNavigationContentLoader, UnityRegionNavigationContentLoader>();
            containerRegistry.RegisterSingleton<IServiceLocator, UnityServiceLocatorAdapter>();

            // Register Logger
            containerRegistry.RegisterInstance(typeof(ILoggerFacade), _logger);
            containerRegistry.RegisterInstance(typeof(ILogger), _logger);

            // Register Presentation Services
            containerRegistry.Register<ISettingsService, SettingsService>();
            containerRegistry.Register<INavigationService, NavigationService>();
            containerRegistry.Register<INotificationService, NotificationService>();
            containerRegistry.Register<IAuthenticationService, AuthenticationService>();
            containerRegistry.Register<IDialogService, DialogService>();
        }

        /// <summary>
        /// Configures the <see cref="IRegionBehaviorFactory"/>.
        /// This will be the list of default behaviors that will be added to a region.
        /// </summary>
        protected virtual void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            if (regionBehaviors != null)
            {
                regionBehaviors.AddIfMissing(BindRegionContextToDependencyObjectBehavior.BehaviorKey, typeof(BindRegionContextToDependencyObjectBehavior));
                regionBehaviors.AddIfMissing(RegionActiveAwareBehavior.BehaviorKey, typeof(RegionActiveAwareBehavior));
                regionBehaviors.AddIfMissing(SyncRegionContextWithHostBehavior.BehaviorKey, typeof(SyncRegionContextWithHostBehavior));
                regionBehaviors.AddIfMissing(RegionManagerRegistrationBehavior.BehaviorKey, typeof(RegionManagerRegistrationBehavior));
                regionBehaviors.AddIfMissing(RegionMemberLifetimeBehavior.BehaviorKey, typeof(RegionMemberLifetimeBehavior));
                regionBehaviors.AddIfMissing(ClearChildViewsRegionBehavior.BehaviorKey, typeof(ClearChildViewsRegionBehavior));
                regionBehaviors.AddIfMissing(AutoPopulateRegionBehavior.BehaviorKey, typeof(AutoPopulateRegionBehavior));
            }
        }

        /// <summary>
        /// Configures the default region adapter mappings to use in the application, in order
        /// to adapt UI controls defined in XAML to use a region and register it automatically.
        /// May be overwritten in a derived class to add specific mappings required by the application.
        /// </summary>
        /// <returns>The <see cref="RegionAdapterMappings"/> instance containing all the mappings.</returns>
        protected virtual void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            if (regionAdapterMappings != null)
            {
                regionAdapterMappings.RegisterMapping(typeof(Selector), _containerExtension.Resolve<SelectorRegionAdapter>());
                regionAdapterMappings.RegisterMapping(typeof(ItemsControl), _containerExtension.Resolve<ItemsControlRegionAdapter>());
                regionAdapterMappings.RegisterMapping(typeof(ContentControl), _containerExtension.Resolve<ContentControlRegionAdapter>());
            }
        }

        /// <summary>
        /// Registers the <see cref="Type"/>s of the Exceptions that are not considered
        /// root exceptions by the <see cref="ExceptionExtensions"/>.
        /// </summary>
        protected virtual void RegisterFrameworkExceptionTypes()
        {
            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(ActivationException));
            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(ResolutionFailedException));
        }

        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>The shell of the application.</returns>
        protected Window CreateShell()
        {
            var view = Container.Resolve<Shell>();
            return view;
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        protected virtual void InitializeShell(Window shell)
        {
            MainWindow = shell;
        }

        /// <summary>
        /// Contains actions that should occur last.
        /// </summary>
        protected virtual void OnInitialized()
        {
            MainWindow?.Show();
        }

        /// <summary>
        /// Configures the <see cref="IModuleCatalog"/> used by Prism.
        /// </summary>
        protected virtual void ConfigureModuleCatalog(IModuleCatalog moduleCatalog) { }

        /// <summary>
        /// Initializes the modules.
        /// </summary>
        protected virtual void InitializeModules()
        {
            var manager = _containerExtension.Resolve<IModuleManager>();
            manager.Run();
        }

        /// <summary>
        /// Configures the LocatorProvider for the <see cref="ServiceLocator" />.
        /// </summary>
        protected virtual void ConfigureServiceLocator()
        {
            ServiceLocator.SetLocatorProvider(() => _containerExtension.Resolve<IServiceLocator>());
        }

        /// <inheritdoc />
        /// <summary>
        /// Calls on exit. Save skin.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            SaveSettings();

            _logger.Debug("************************ Application End ************************");
        }

        /// <summary>
        /// Called when [application dispatcher unhandled exception].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void OnAppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Log the unhandled exception
            _logger.Error(e.Exception);
            DialogManager.ShowErrorDialog(MessageResources.UnexpectedError);
            e.Handled = true;
        }

        /// <summary>
        /// Save Skin
        /// </summary>
        private static void SaveSettings()
        {
            // Skin
            if (SkinManager.SkinManager.CurrentTheme != null) Settings.Default.DefaultTheme = SkinManager.SkinManager.CurrentTheme.Name;
            if (SkinManager.SkinManager.CurrentAccent != null) Settings.Default.DefaultAccent = SkinManager.SkinManager.CurrentAccent.Name;
            if (SkinManager.SkinManager.CurrentSecondaryAccent != null) Settings.Default.DefaultSecondaryAccent = SkinManager.SkinManager.CurrentSecondaryAccent.Name;

            Settings.Default.RosterId = AppManager.Roster.Id;

            Settings.Default.Save();
        }

        /// <summary>
        /// Load Skin
        /// </summary>
        private static void LoadSkin()
        {
            SkinManager.SkinManager.ApplyTheme(Settings.Default.DefaultTheme);
            SkinManager.SkinManager.ApplyAccent(Settings.Default.DefaultAccent);
            SkinManager.SkinManager.ApplySecondaryAccent(Settings.Default.DefaultSecondaryAccent);
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
                    defaultUsername = Thread.CurrentPrincipal.Identity.Name;
                }
                else
                {
                    var currentWindowsIdentity = WindowsIdentity.GetCurrent();
                    defaultUsername = currentWindowsIdentity.Name;
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
                    Dispatcher.Invoke(() =>
                    {
                        Current.Shutdown();
                    });
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

        /// <summary>
        /// Updates splah message.
        /// </summary>
        /// <param name="message"></param>
        private void UpdateSplachMessage(string message)
        {
            _splashScreenViewModel.UpdateMessage(message);
        }
    }
}