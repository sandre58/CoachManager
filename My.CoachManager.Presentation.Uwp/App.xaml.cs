using System;
using System.Globalization;
using System.Threading.Tasks;

using My.CoachManager.Presentation.Uwp.Views;

using Unity;

using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using My.CoachManager.CrossCutting.Logging.Supervision;
using My.CoachManager.Presentation.Uwp.Constants;
using My.CoachManager.Presentation.Uwp.Core.ViewModels;
using My.CoachManager.Presentation.Uwp.Services;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Unity.Windows;
using Prism.Windows.AppModel;
using Unity.Lifetime;

namespace My.CoachManager.Presentation.Uwp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        protected override void ConfigureContainer()
        {
            // register a singleton using Container.RegisterType<IInterface, Type>(new ContainerControlledLifetimeManager());
            base.ConfigureContainer();
            Container.RegisterType<IToastNotificationsService, ToastNotificationsService>(new ContainerControlledLifetimeManager());
            Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()));
        }

        protected override async Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            await LaunchApplicationAsync(PageTokens.HomePage, null);
        }

        private async Task LaunchApplicationAsync(string page, object launchParam)
        {
            await ThemeSelectorService.SetRequestedThemeAsync();
            ExtendAcrylicIntoTitleBar();
            NavigationService.Navigate(page, launchParam);
            Window.Current.Activate();
        }

        protected override async Task OnActivateApplicationAsync(IActivatedEventArgs args)
        {
            if (args.Kind == ActivationKind.ToastNotification && args.PreviousExecutionState != ApplicationExecutionState.Running)
            {
                // Handle a toast notification here
                // Since dev center, toast, and Azure notification hub will all active with an ActivationKind.ToastNotification
                // you may have to parse the toast data to determine where it came from and what action you want to take
                // If the app isn't running then launch the app here
                await OnLaunchApplicationAsync(args as LaunchActivatedEventArgs);
            }

            await Task.CompletedTask;
        }

        protected override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            await ThemeSelectorService.InitializeAsync().ConfigureAwait(false);

            await base.OnInitializeAsync(args);
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                 var viewModelName = viewType.Name.Replace("Page", "ViewModel");
                var viewModelTypeName = viewType.FullName.Replace(".Views.", ".ViewModels.").Replace(viewType.Name, viewModelName);
                return Type.GetType(viewModelTypeName);
            });

            // Set the default view model factory
            ViewModelLocationProvider.SetDefaultViewModelFactory(
                viewModelType =>
                {
                    if (!(Container.TryResolve(viewModelType) is ScreenViewModel viewModel))
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

        protected override UIElement CreateShell(Frame rootFrame)
        {
            var shell = Container.Resolve<ShellPage>();
            shell.SetRootFrame(rootFrame);
            return shell;
        }

        private void ExtendAcrylicIntoTitleBar()
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }

        /// <inheritdoc />
        /// <summary>
        /// Create the logger.
        /// </summary>
        /// <returns>The <see cref="T:Prism.Logging.ILoggerFacade" />.</returns>
        protected override ILoggerFacade CreateLogger()
        {
            return LoggerFactory.CreateLogger() as ILoggerFacade;
        }

    }
}
