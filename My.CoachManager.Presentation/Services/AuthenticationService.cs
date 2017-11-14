using System.Security.Principal;
using My.CoachManager.Application.Dtos.Users;
using My.CoachManager.CrossCutting.Core.Cryptography;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Core.Services.Interfaces;
using My.CoachManager.Presentation.ServiceAgent;
using My.CoachManager.Presentation.ServiceAgent.UserServiceReference;

namespace My.CoachManager.Presentation.Services
{
    /// <summary>
    /// The implementation of the contract <see cref="IAuthenticationService"/>.
    /// this class has no need on its ownself, hence explicit implementation.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        #region Public Methods

        /// <summary>
        /// Authenticate an user.
        /// </summary>
        /// <param name="login">The user name.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public UserDto Authenticate(string login, string password)
        {
            return GetAuthenticatedUser(login, password);
        }

        /// <summary>
        /// Authenticate an user.
        /// </summary>
        /// <returns></returns>
        public UserDto AuthenticateByWindowsCredentials()
        {
            var currentWindowsIdentity = WindowsIdentity.GetCurrent();
            var login = currentWindowsIdentity.GetLogin();
            return GetAuthenticatedUser(login);
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
            using (var client = ServiceClientFactory.Create<UserServiceClient, IUserService>())
            {
                var hashPassword = TripleDesEncryptor.Encrypt(password, login);
                return client.GetByLoginAndPassword(login, hashPassword);
            }
        }

        /// <summary>
        /// Gets Authenticated an user.
        /// </summary>
        /// <param name="login">The user name.</param>
        /// <returns></returns>
        protected UserDto GetAuthenticatedUser(string login)
        {
            using (var client = ServiceClientFactory.Create<UserServiceClient, IUserService>())
            {
                return client.GetByLogin(login);
            }
        }

        #endregion Private Methods
    }
}