using System.Windows.Input;
using My.CoachManager.Presentation.Core.Commands;
using My.CoachManager.Presentation.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces;

namespace My.CoachManager.Presentation.ViewModels.Shell
{
    /// <summary>
    /// ViewModel for the login window.
    /// </summary>
    public class LoginViewModel : DialogViewModel, ILoginViewModel
    {
        #region Fields

        private static LoginViewModel _instance;
        private string _userName;
        private string _password;
        private string _errorMessage;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Get the instance.
        /// </summary>
        public static LoginViewModel Instance
        {
            get
            {
                return _instance ?? (_instance = new LoginViewModel());
            }
        }

        /// <summary>
        /// Get the display Name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return Resources.Strings.Screens.ShellResources.Login;
            }
        }

        /// <summary>
        /// Get or Set Login Command.
        /// </summary>
        public ICommand LoginCommand { get; set; }

        /// <summary>
        /// Get or Set Cancel Command.
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username
        {
            get { return _userName; }
            set
            {
                if (_userName == value) return;
                _userName = value;
                NotifyOfPropertyChange(() => Username);
                NotifyOfPropertyChange(() => LoginCommand);
            }
        }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value)
                    return;
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => LoginCommand);
            }
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (_errorMessage == value)
                    return;
                _errorMessage = value;
                NotifyOfPropertyChange();
            }
        }

        #endregion Properties

        #region Constructors

        protected LoginViewModel()
        {
            LoginCommand = new DelegateCommand(Login, CanLogin);
            CancelCommand = new DelegateCommand(Cancel, CanCancel);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Logg in.
        /// </summary>
        public void Login()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)) return;
            TryClose(true);
        }

        /// <summary>
        /// Can Cancel ?
        /// </summary>
        /// <returns></returns>
        public bool CanLogin()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        /// <summary>
        /// Cancel.
        /// </summary>
        public void Cancel()
        {
            TryClose(false);
        }

        /// <summary>
        /// Can Cancel ?
        /// </summary>
        /// <returns></returns>
        public bool CanCancel()
        {
            return true;
        }

        #endregion Methods
    }
}