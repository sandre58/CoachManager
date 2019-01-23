using System.Security.Principal;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Cryptography;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Core.Security;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.ServiceAgent.UserServiceReference;

namespace My.CoachManager.Presentation.Prism.Wpf.Services
{
    /// <inheritdoc />
    /// <summary>
    /// The implementation of the contract <see cref="T:My.CoachManager.Presentation.Prism.Core.Services.IAuthenticationService" />.
    /// this class has no need on its ownself, hence explicit implementation.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        
        private readonly IUserService _userService;

        #endregion Fields

        #region Constructor

        public AuthenticationService(IUserService userService)
        {
            _userService = userService;
        }

        #endregion Constructor

        #region Public Methods

        /// <inheritdoc />
        /// <summary>
        /// Authenticate an user.
        /// </summary>
        /// <param name="login">The user name.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public IPrincipal Authenticate(string login, string password)
        {
            var user = GetAuthenticatedUser(login, password);

            return GetPrincipalFromUser(user);
        }

        /// <inheritdoc />
        /// <summary>
        /// Authenticate an user.
        /// </summary>
        /// <returns></returns>
        public IPrincipal AuthenticateByWindowsCredentials()
        {
            var currentWindowsIdentity = WindowsIdentity.GetCurrent();
            var login = currentWindowsIdentity.GetLogin();
            var user = GetAuthenticatedUser(login);

            return GetPrincipalFromUser(user);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Gets Authenticated an user.
        /// </summary>
        /// <param name="login">The user name.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        private UserDto GetAuthenticatedUser(string login, string password)
        {
            var hashPassword = TripleDesEncryptor.Encrypt(password, login);
            return _userService.GetUserByLoginAndPassword(login, hashPassword);
        }

        /// <summary>
        /// Gets Authenticated an user.
        /// </summary>
        /// <param name="login">The user name.</param>
        /// <returns></returns>
        private UserDto GetAuthenticatedUser(string login)
        {
            return _userService.GetUserByLogin(login);
        }

        /// <summary>
        /// Get principal.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static IPrincipal GetPrincipalFromUser(UserDto user)
        {
            if (user != null)
            {
                return new Principal()
                {
                    Identity = new Identity(user.Login, user.Name, user.Mail, SettingsManager.GetRosterId())
                };
            }

            return null;
        }

        #endregion Private Methods
    }
}