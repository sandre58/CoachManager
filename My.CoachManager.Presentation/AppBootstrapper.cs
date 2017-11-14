using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using My.CoachManager.Application.Dtos.Users;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Core.Security;
using My.CoachManager.CrossCutting.Unity;
using My.CoachManager.Presentation.Resources.Strings;
using My.CoachManager.Presentation.Services;
using My.CoachManager.Presentation.ViewModels;
using My.CoachManager.Presentation.ViewModels.Mapping;
using My.CoachManager.Presentation.ViewModels.Shell;

namespace My.CoachManager.Presentation
{
    public class AppBoostrapper : BootstrapperBase
    {
        public AppBoostrapper()
        {
            Initialize();
        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            // Set accent
            ServiceLocator.AppearanceService.Accent = "MyAccent";

            DisplayRootViewFor<ShellViewModel>();

            ShowSplashScreen(MessageResources.UserConnection);

            UserDto userLogged = null;

            // Windows Authentication

            if (ConfigurationManager.WindowsAuthentication)
            {
                await Task.Run(() =>
                {
                    userLogged = ServiceLocator.AuthenticationService.AuthenticateByWindowsCredentials();
                });
            }

            // User connection
            if (userLogged == null)
            {
                // Display the splash while 1 second.
                await Task.Run(() =>
                {
                    Thread.Sleep(1000);
                });

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

                // Logging
                while (userLogged == null)
                {
                    // Close spash screen
                    SplashScreenViewModel.Instance.TryClose();

                    // Show dialog login
                    var login = await ServiceLocator.DialogService.ShowLoginDialog(defaultUsername, defaultPassword);

                    if (login.DialogResult)
                    {
                        // Show splash screen
                        ShowSplashScreen(MessageResources.UserConnection);

                        // Loggin action
                        await Task.Run(() =>
                        {
                            userLogged = ServiceLocator.AuthenticationService.Authenticate(login.Username, login.Password);
                        });

                        if (userLogged == null)
                        {
                            LoginViewModel.Instance.ErrorMessage = MessageResources.ConnectionFailed;
                            defaultUsername = login.Username;
                            defaultPassword = login.Password;
                        }
                    }
                    else
                    {
                        OnLogginFailed();
                    }
                }
            }

            if (userLogged != null)
            {
                OnLoggingSuccess(userLogged);
            }
        }

        /// <summary>
        /// Load the application.
        /// </summary>
        protected void OnLoggingSuccess(UserDto userDto)
        {
            var principal = new Principal()
            {
                Identity = new Identity(userDto.Login, userDto.Name, userDto.Mail, userDto.Roles.SelectMany(r => r.Permissions).Select(p => p.Code))
            };

            AppDomain.CurrentDomain.SetThreadPrincipal(principal);
            Thread.CurrentPrincipal = principal;

            SplashScreenViewModel.Instance.Message = MessageResources.AppOpening;
            UnityFactory.Resolve<ShellViewModel>().Load(userDto.ToViewModel<UserViewModel>());

            SplashScreenViewModel.Instance.TryClose();
        }

        /// <summary>
        /// Show spash screen.
        /// </summary>
        /// <param name="message"></param>
        protected void ShowSplashScreen(string message = "")
        {
            var settings = new CustomDialogSettings()
            {
                FullWidth = false,
                Theme = MetroDialogColorScheme.Accented,
                Header = ""
            };

            SplashScreenViewModel.Instance.Message = message;
            ServiceLocator.DialogService.ShowDialogAsync(SplashScreenViewModel.Instance, settings);
        }

        /// <summary>
        /// Load the application.
        /// </summary>
        protected void OnLogginFailed()
        {
            Application.Shutdown();
        }

        protected override void Configure()
        {
            // Custom accent
            ServiceLocator.AppearanceService.AddAccent("MyAccent", "pack://application:,,,/My.CoachManager.Presentation;component/Resources/Styles/MyAccent.xaml");
        }

        protected override object GetInstance(Type service, string key)
        {
            return UnityFactory.Resolve(service);
        }

        protected override async void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            base.OnUnhandledException(sender, e);

            ServiceLocator.Logger.Fatal(e.Exception);
            await ServiceLocator.DialogService.ShowError(MessageResources.FatalError);
            e.Handled = true;

            Application.Shutdown();
        }
    }
}