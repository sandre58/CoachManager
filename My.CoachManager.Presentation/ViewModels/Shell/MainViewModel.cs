using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using My.CoachManager.Application.Dtos.Users;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Core.Security;
using My.CoachManager.CrossCutting.Unity;
using My.CoachManager.Presentation.Core.Services.Interfaces;
using My.CoachManager.Presentation.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces;
using My.CoachManager.Presentation.Resources.Strings;
using My.CoachManager.Presentation.ViewModels.Mapping;

namespace My.CoachManager.Presentation.ViewModels.Shell
{
    /// <summary>
    /// ViewModel for the main window.
    /// </summary>
    public class MainViewModel : Conductor<INavigatable>
    {
        #region Fields

        private readonly IMenuNavigationService _navigationService;
        private UserViewModel _loggedInUser;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainViewModel(UserViewModel user)
        {
            _navigationService = ServiceLocator.NavigationService;
            LoggedInUser = user;

            if (_navigationService != null)
                _navigationService.ViewChanged += OnNavigationViewChanged;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get or set the connected user.
        /// </summary>
        public UserViewModel LoggedInUser
        {
            get { return _loggedInUser; }
            set
            {
                if (Equals(_loggedInUser, value)) return;
                _loggedInUser = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Gets or sets the item menu selected.
        /// </summary>
        public MenuItemViewModel SelectedItemMenu
        {
            get { return NavigationService.SelectedItemMenu; }
        }

        /// <summary>
        /// Gets or sets the item menu selected.
        /// </summary>
        public MenuItemViewModel SelectedFirstLevelItemMenu
        {
            get { return NavigationService.SelectedFirstLevelItemMenu; }
        }

        /// <summary>
        /// Gets the navigationservice.
        /// </summary>
        public IMenuNavigationService NavigationService
        {
            get { return _navigationService; }
        }

        /// <summary>
        /// Can Go Back ?
        /// </summary>
        /// <returns></returns>
        public bool CanGoBack
        {
            get
            {
                if (_navigationService != null)
                {
                    return _navigationService.CanGoBack();
                }

                return false;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Navigue to the menu.
        /// </summary>
        /// <param name="menuItem"></param>
        public void NavigateToMenu(MenuItemViewModel<INavigatable> menuItem)
        {
            if (_navigationService != null)
                _navigationService.NavigateToMenu(menuItem);
        }

        /// <summary>
        ///  Go Back.
        /// </summary>
        public void GoBack()
        {
            if (_navigationService != null)
            {
                _navigationService.GoBack();
            }
        }

        public async void Login()
        {
            UserDto userLogged = null;
            var defaultUsername = Thread.CurrentPrincipal.Identity.GetLogin();
            var defaultPassword = "";

            // Logging
            while (userLogged == null)
            {
                // Show dialog login
                var login = await ServiceLocator.DialogService.ShowLoginDialog(defaultUsername, defaultPassword);

                if (login.DialogResult)
                {
                    // Show splash screen
                    UnityFactory.Resolve<IWindowManager>().ShowWindow(SplashScreenViewModel.Instance);
                    SplashScreenViewModel.Instance.Message = MessageResources.UserConnection;

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
                    if (!Thread.CurrentPrincipal.Identity.IsAuthenticated)
                        System.Windows.Application.Current.Shutdown();
                    else
                    {
                        return;
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
        /// Update the active view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNavigationViewChanged(object sender, EventArgs e)
        {
            if (_navigationService != null)
            {
                ActivateItem(_navigationService.CurrentView);

                NotifyOfPropertyChange(() => SelectedFirstLevelItemMenu);
                NotifyOfPropertyChange(() => SelectedItemMenu);
                NotifyOfPropertyChange(() => CanGoBack);
            }
        }

        #endregion Methods
    }
}