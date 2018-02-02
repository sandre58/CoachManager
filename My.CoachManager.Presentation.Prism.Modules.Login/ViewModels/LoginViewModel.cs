using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.Login.Core;
using Prism.Commands;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.Modules.Login.ViewModels
{
    /// <summary>
    /// ViewModel for the login window.
    /// </summary>
    public class LoginViewModel : DialogViewModel, ILoginViewModel
    {
        #region Fields

        private string _userName;
        private string _password;
        private string _error;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Get or Set Login Command.
        /// </summary>
        public DelegateCommand LoginCommand { get; set; }

        /// <summary>
        /// Get or Set Cancel Command.
        /// </summary>
        public DelegateCommand CancelCommand { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set
            {
                SetProperty(ref _userName, value, () =>
          {
              LoginCommand.RaiseCanExecuteChanged();
          });
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
                SetProperty(ref _password, value, () =>
                {
                    LoginCommand.RaiseCanExecuteChanged();
                });
            }
        }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        public string Error
        {
            get { return _error; }
            set
            {
                SetProperty(ref _error, value);
            }
        }

        #endregion Properties

        #region Constructors

        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand(Login, CanLogin);
            CancelCommand = new DelegateCommand(Cancel, CanCancel);

            Locator.GetInstance<IEventAggregator>().GetEvent<LoginErrorRequestEvent>().Subscribe(error => Error = error);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Logg in.
        /// </summary>
        public void Login()
        {
            Close(DialogResult.Ok);
        }

        /// <summary>
        /// Can Cancel ?
        /// </summary>
        /// <returns></returns>
        public bool CanLogin()
        {
            return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
        }

        /// <summary>
        /// Cancel.
        /// </summary>
        public void Cancel()
        {
            Close(DialogResult.Cancel);
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