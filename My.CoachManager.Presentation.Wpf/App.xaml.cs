using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.CrossCutting.Logging.Supervision;
using My.CoachManager.CrossCutting.Resources;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Wpf.Core;
using My.CoachManager.Presentation.Wpf.Core.Dialog;
using My.CoachManager.Presentation.Wpf.Core.Ioc;
using My.CoachManager.Presentation.Wpf.Core.Manager;
using My.CoachManager.Presentation.Wpf.Core.Services;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;
using My.CoachManager.Presentation.Wpf.Dialog;
using My.CoachManager.Presentation.Wpf.Ioc;
using My.CoachManager.Presentation.Wpf.Properties;
using My.CoachManager.Presentation.Wpf.Services;
using My.CoachManager.Presentation.Wpf.ViewModels.Shell;
using My.CoachManager.Presentation.Wpf.Views.Administration;
using My.CoachManager.Presentation.Wpf.Views.Misc;
using My.CoachManager.Presentation.Wpf.Views.Shell;
using Unity;
using Unity.Lifetime;
using UnityServiceLocator = My.CoachManager.Presentation.Wpf.Ioc.UnityServiceLocator;

namespace My.CoachManager.Presentation.Wpf
{
    /// <inheritdoc />
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public sealed partial class App
    {
        private readonly ILogger _logger;
        private SplashScreenViewModel _splashScreenViewModel;
        private readonly IUnityContainer _iocContainer = new UnityContainer();

        /// <inheritdoc />
        /// <summary>
        ///     Initialise a new instance of <see cref="T:My.CoachManager.Presentation.Wpf.App" />.
        /// </summary>
        public App()
        {
            DispatcherHelper.Initialize();

            // Configure logger
            Logger.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/NLog.config"));
            _logger = LoggerFactory.CreateLogger();

            DispatcherUnhandledException += OnAppDispatcherUnhandledException;
            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _logger.Debug("************************ Application Start ************************");

            Initialize();

            _splashScreenViewModel = new SplashScreenViewModel();
            var splashScreen = new Views.Shell.SplashScreen(_splashScreenViewModel);

            splashScreen.Show();

            await Task.Run(() =>
            {
                using (LogManager.TraceGroup(GetType().Name + "." + nameof(LoadData)))
                {
                    LoadData();
                }
            }).ContinueWith(x =>
            {
                // After
                Dispatcher?.Invoke(() =>
                {
                    LoadShell();
                    splashScreen.Hide();
                    OnInitialized();
                });

                // Exception
                if (x.IsFaulted && x.Exception != null)
                {
                    if (x.Exception.InnerException is ApiException apiException)
                    {
                        if (apiException.CastInnerException is BusinessException businessException)
                            OnBusinessExceptionOccured(businessException);
                        else if (apiException.CastInnerException is ValidationBusinessException
                            validationBusinessException)
                            foreach (var error in validationBusinessException.Errors)
                                OnBusinessExceptionOccured(new BusinessException(error.ToString()));
                    }
                    else
                    {
                        OnExceptionOccured(x.Exception.InnerException ?? x.Exception);
                    }
                }
            });
        }

        /// <inheritdoc />
        /// <summary>
        ///     Calls on exit. Save skin.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            SaveSettings();

            _logger.Debug("************************ Application End ************************");
        }

        #region SplashScreen

        /// <summary>
        ///     Updates splash message.
        /// </summary>
        /// <param name="message"></param>
        private void UpdateSplashMessage(string message)
        {
            _splashScreenViewModel.UpdateMessage(message);
        }

        #endregion

        #region Ioc

        /// <summary>
        /// Configure service locator.
        /// </summary>
        /// <param name="serviceLocator"></param>
        private void ConfigureServiceLocator(IServiceLocator serviceLocator)
        {
            ViewModelProvider.ViewModelFactory = serviceLocator.GetInstance<IViewModelProvider>().GetViewModel;
            ViewModelProvider.ViewTypeToViewModelTypeResolver = serviceLocator.GetInstance<IViewModelTypeLocator>().LocateViewModel;

            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }

        /// <summary>
        /// Registers the <see cref="Type"/>s of the Exceptions
        /// </summary>
        private void RegisterFrameworkExceptionTypes()
        {
            _iocContainer.RegisterType<ActivationException>();
        }
        /// <summary>
        ///     Registers all types that are required.
        /// </summary>
        private void RegisterServices()
        {
            // Register Logger
            _iocContainer.RegisterInstance(_logger);

            // Register Presentation Services
            _iocContainer.RegisterType<IMessenger, Messenger>(new ContainerControlledLifetimeManager());
            _iocContainer.RegisterType<IViewModelProvider, ViewModelLocationProvider>(new ContainerControlledLifetimeManager());
            _iocContainer.RegisterType<IViewModelTypeLocator, NamingConventionDialogTypeLocator>(new ContainerControlledLifetimeManager());
            _iocContainer.RegisterType<IDialogFactory, DialogFactory>(new ContainerControlledLifetimeManager());
            _iocContainer.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            _iocContainer.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            _iocContainer.RegisterType<ISettingsService, SettingsService>(new ContainerControlledLifetimeManager());
            _iocContainer.RegisterType<INotificationService, NotificationService>(new ContainerControlledLifetimeManager());
            _iocContainer.RegisterType<IAuthenticationService, AuthenticationService>(new ContainerControlledLifetimeManager());

        }

        /// <summary>
        /// Register view models.
        /// </summary>
        private void RegisterViewModels()
        {
           var types = Assembly.GetExecutingAssembly().GetTypes().Where( x => typeof(IScreenViewModel).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract).ToList();

           // Register these types and use reflection to instantiate each instance...
           foreach (var type in types)
           {
               var t = type;

               if (typeof(IDialogViewModel).IsAssignableFrom(type))
               {
                   _iocContainer.RegisterType(type, t.Name, new ContainerControlledTransientManager());
               }
               else
               {
                   _iocContainer.RegisterType(type, t.Name, new ContainerControlledLifetimeManager());
               }
           }

           _iocContainer.RegisterType<AboutView>(new ContainerControlledLifetimeManager());
           _iocContainer.RegisterType<CategoryEditView>(new ContainerControlledLifetimeManager());
           _iocContainer.RegisterType<SeasonEditView>(new ContainerControlledLifetimeManager());
           _iocContainer.RegisterType<RosterEditView>(new ContainerControlledLifetimeManager());

        }

        /// <summary>
        ///     Runs the initialization sequence to configure the application.
        /// </summary>
        private void Initialize()
        {
            // Initialise Wep Client Helper
            ApiHelper.InitializeHttpClient(ConfigurationManager.Server);
            
            // Register special exceptions
            RegisterFrameworkExceptionTypes();

            // Register Services
            RegisterServices();

            // Register Services
            RegisterViewModels();

            // Service Locator
            var locator = new UnityServiceLocator(_iocContainer);
            ConfigureServiceLocator(locator);

            // Load skin
            LoadSkin();
        }
        
        #endregion

        #region Data

        private void LoadData()
        {
            UpdateSplashMessage(MessageResources.UserConnection);
            if (!ConnectUser()) return;

            _logger.Debug("User connected : " + Thread.CurrentPrincipal.Identity.Name);

            // Load Roster
            UpdateSplashMessage(MessageResources.RosterLoading);
            //var roster = ApiHelper.GetData<RosterDto>("Api/rosters", Thread.CurrentPrincipal.GetRosterId());
            //AppManager.InitializeRoster(RosterFactory.Get(roster));

            //_logger.Debug("Current roster : " + AppManager.Roster.Name + "[Id=" + AppManager.Roster.Id + "]");
        }

        #endregion

        #region Shell

        /// <summary>
        ///     Creates the shell or main window of the application.
        /// </summary>
        /// <returns>The shell of the application.</returns>
        private Window CreateShell()
        {
            return new MainView();
        }

        /// <summary>
        ///     Initializes the shell.
        /// </summary>
        private void InitializeShell(Window shell)
        {
            if (shell is MainView sh) ServiceLocator.Current.GetInstance<INavigationService>().ConfigureFrame(sh.MainFrame);
            MainWindow = shell;
        }

        /// <summary>
        ///     Load shell.
        /// </summary>
        private void LoadShell()
        {
            var shell = CreateShell();
            if (shell != null) InitializeShell(shell);
        }

        /// <summary>
        ///     Contains actions that should occur last.
        /// </summary>
        private void OnInitialized()
        {
            MainWindow?.Show();
        }

        #endregion

        #region User connection

        /// <summary>
        ///     Connection of the user.
        /// </summary>
        /// <returns></returns>
        private bool ConnectUser()
        {
            IPrincipal principal = null;

            if (ConfigurationManager.WindowsAuthentication) principal = GetConnectedUser();

            if (principal == null) return Thread.CurrentPrincipal.Identity.IsAuthenticated;
            AppDomain.CurrentDomain.SetThreadPrincipal(principal);
            Thread.CurrentPrincipal = principal;

            return Thread.CurrentPrincipal.Identity.IsAuthenticated;
        }

        /// <summary>
        ///     Connection of the user.
        /// </summary>
        /// <returns></returns>
        private static IPrincipal GetConnectedUser(string login = "", string password = "",
            bool byWindowsCredentials = true)
        {
            var authenticationService = ServiceLocator.Current.GetInstance<IAuthenticationService>();

            return byWindowsCredentials
                ? authenticationService.AuthenticateByWindowsCredentials()
                : authenticationService.Authenticate(login, password);
        }

        #endregion

        #region Settings

        /// <summary>
        ///     Save Skin
        /// </summary>
        private static void SaveSettings()
        {
            // Skin
            if (SkinManager.SkinManager.CurrentTheme != null)
                Settings.Default.DefaultTheme = SkinManager.SkinManager.CurrentTheme.Name;
            if (SkinManager.SkinManager.CurrentAccent != null)
                Settings.Default.DefaultAccent = SkinManager.SkinManager.CurrentAccent.Name;
            if (SkinManager.SkinManager.CurrentSecondaryAccent != null)
                Settings.Default.DefaultSecondaryAccent = SkinManager.SkinManager.CurrentSecondaryAccent.Name;

            //Settings.Default.RosterId = AppManager.Roster.Id;

            Settings.Default.Save();
        }

        /// <summary>
        ///     Load Skin
        /// </summary>
        private static void LoadSkin()
        {
            SkinManager.SkinManager.ApplyTheme(Settings.Default.DefaultTheme);
            SkinManager.SkinManager.ApplyAccent(Settings.Default.DefaultAccent);
            SkinManager.SkinManager.ApplySecondaryAccent(Settings.Default.DefaultSecondaryAccent);
        }

        #endregion

        #region Exception Management

        /// <summary>
        ///     Called when [application dispatcher unhandled exception].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DispatcherUnhandledExceptionEventArgs" /> instance containing the event data.</param>
        private void OnAppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Log the unhandled exception
            OnExceptionOccured(e.Exception);
            e.Handled = true;
        }

        /// <summary>
        ///     Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        private void OnExceptionOccured(Exception e)
        {
            LogManager.Fatal(e);
            DialogManager.ShowErrorDialog(MessageResources.GetDataError, MessageBoxButton.OK);
        }

        /// <summary>
        ///     Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        private void OnBusinessExceptionOccured(BusinessException e)
        {
            NotificationManager.ShowError(e.Message);
        }

        #endregion
    }
}