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

        #endregion

        #region Constructor

        public AuthenticationService(IUserService userService, ILogger logger)
        {
            _userService = userService;
            _logger = logger;
        }
#endregion
        #region Public Methods

        /// <summary>
        /// Authenticate an user.
        /// </summary>
        /// <param name="login">The user name.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public bool Authenticate(string login, string password)
        {
            var user = GetAuthenticatedUser(login, password);

            SetIdentity(user);
            return user != null;
        }

        /// <summary>
        /// Authenticate an user.
        /// </summary>
        /// <returns></returns>
        public bool AuthenticateByWindowsCredentials()
        {
            var currentWindowsIdentity = WindowsIdentity.GetCurrent();
            var login = currentWindowsIdentity.GetLogin();
            var user = GetAuthenticatedUser(login);
            
            SetIdentity(user);
            return user != null;
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

        protected void SetIdentity(UserDto user)
        {
            if (user != null)
            {
                var principal = new Principal()
                {
                    Identity = new Identity(user.Login, user.Name, user.Mail,
                        user.Roles.SelectMany(r => r.Permissions).Select(p => p.Code))
                };

                AppDomain.CurrentDomain.SetThreadPrincipal(principal);
                Thread.CurrentPrincipal = principal;
            }
        }

        #endregion Private Methods
    }
}