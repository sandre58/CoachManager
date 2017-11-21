using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using My.CoachManager.Application.Dtos.Users;
using My.CoachManager.CrossCutting.Core.Cryptography;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Core.Security;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.ServiceAgent.UserServiceReference;

namespace My.CoachManager.Presentation.Prism.Wpf.Services
{
    /// <summary>
    /// The implementation of the contract <see cref="IAuthenticationService"/>.
    /// this class has no need on its ownself, hence explicit implementation.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly IUserService _userService;

        #endregion Fields

        #region Constructor

        public AuthenticationService(IUserService userService, ILogger logger)
        {
            _userService = userService;
            _logger = logger;
        }

        #endregion Constructor

        #region Public Methods

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
        protected UserDto GetAuthenticatedUser(string login, string password)
        {
            var hashPassword = TripleDesEncryptor.Encrypt(password, login);
            return _userService.GetUserByLoginAndPassword(login, hashPassword);
        }

        /// <summary>
        /// Gets Authenticated an user.
        /// </summary>
        /// <param name="login">The user name.</param>
        /// <returns></returns>
        protected UserDto GetAuthenticatedUser(string login)
        {
            return _userService.GetUserByLogin(login);
        }

        protected IPrincipal GetPrincipalFromUser(UserDto user)
        {
            if (user != null)
            {
                return new Principal()
                {
                    Identity = new Identity(user.Login, user.Name, user.Mail,
                        user.Roles.SelectMany(r => r.Permissions).Select(p => p.Code))
                };
            }

            return null;
        }

        #endregion Private Methods
    }
}