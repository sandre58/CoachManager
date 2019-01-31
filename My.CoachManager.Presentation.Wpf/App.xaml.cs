using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Practices.ServiceLocation;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Core.Manager;
using My.CoachManager.Presentation.Modules.Home.Views;
using My.CoachManager.Presentation.Wpf.ViewModels;
using SplashScreen = My.CoachManager.Presentation.Wpf.Views.SplashScreen;

namespace My.CoachManager.Presentation.Wpf
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App
    {
        private SplashScreenViewModel _splashScreenViewModel;

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            DispatcherUnhandledException += OnAppDispatcherUnhandledException;

            _splashScreenViewModel = new SplashScreenViewModel();
            _splashScreenViewModel.Initialize();

            // Create a thread for the splash screen
            var splashScreenThread = new Thread(
                () =>
                {
                    // Create our context, and install it:
                    SynchronizationContext.SetSynchronizationContext(
                        new DispatcherSynchronizationContext(Dispatcher.CurrentDispatcher));

                    var splash = new SplashScreen() { DataContext = _splashScreenViewModel };

                    splash.Closed += (s, closeEvent) =>
                    {
                        Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.Background);
                    };

                    splash.Show();

                    Dispatcher.Run();
                });

            splashScreenThread.SetApartmentState(ApartmentState.STA);
            //splashScreenThread.Start();

            _splashScreenViewModel.UpdateMessage(MessageResources.ApplicationStartLoading);
            base.OnStartup(e);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var bootstrapper = new Bootstrapper(_splashScreenViewModel);
            bootstrapper.Run();

            stopwatch.Stop();
            _splashScreenViewModel.UpdateMessage(MessageResources.ApplicationEndLoading);


            NavigationManager.NavigateTo(typeof(HomeView));

            ShutdownMode = ShutdownMode.OnMainWindowClose;

            // Close splash
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            SettingsManager.SaveSkin();
        }

        /// <summary>
        /// Called when [application dispatcher unhandled exception].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private static void OnAppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Log the unhandled exception
            ServiceLocator.Current.GetInstance<ILogger>().Error(e.Exception);
        }
    }
}