using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Practices.ServiceLocation;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Wpf.ViewModels;

namespace My.CoachManager.Presentation.Prism.Wpf
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// The splash screen model.
        /// </summary>
        private SplashScreenViewModel _splashScreenModel;

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        [STAThread]
        protected override void OnStartup(StartupEventArgs e)
        {
            Current.DispatcherUnhandledException += OnAppDispatcherUnhandledException;

            _splashScreenModel = new SplashScreenViewModel();

            //// Create a thread for the splash screen
            //var splashScreenThread = new Thread(
            //    () =>
            //    {
            //        // Create our context, and install it:
            //        SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(Dispatcher.CurrentDispatcher));

            //            var splash = new SplashScreenView { DataContext = _splashScreenModel };

            //            splash.Closed += (s, closeEvent) =>
            //        {
            //            Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.Background);
            //        };

            //            splash.Show();


            //        Dispatcher.Run();
            //    });

            //splashScreenThread.SetApartmentState(ApartmentState.STA);
            //splashScreenThread.Start();
            
            base.OnStartup(e);

            _splashScreenModel.Message = "InformationMessage.ApplicationStartLoading";
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            InitializeDefaultThread();

                var bootstrapper = new Bootstrapper(_splashScreenModel);
                bootstrapper.Run();

            stopwatch.Stop();
            _splashScreenModel.Message = "InformationMessage.ApplicationStartLoading" + stopwatch.ElapsedMilliseconds;

            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        /// <summary>
        /// The initialize default thread.
        /// </summary>
        private static void InitializeDefaultThread()
        {
            //var identity = new AbsoluIdentity("AbsoluIdentity");
            //var principal = new AbsoluPrincipal(identity, new string[0]);

            //Thread.CurrentPrincipal = principal;
            //AppDomain.CurrentDomain.SetThreadPrincipal(principal);
        }

        /// <summary>
        /// Called when [application dispatcher unhandled exception].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void OnAppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
                // Log the unhandled exception
                ServiceLocator.Current.TryResolve<ILogger>().Error(e.Exception);
        }

    }
}