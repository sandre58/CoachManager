using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Cryptography;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Wpf.Constants;
using My.CoachManager.Presentation.Wpf.Core.Services;
using My.CoachManager.Presentation.Wpf.Properties;

namespace My.CoachManager.Presentation.Wpf.Services
{
    /// <inheritdoc />
    /// <summary>
    /// The implementation of the contract <see cref="T:My.CoachManager.Presentation.Core.Services.IAuthenticationService" />.
    /// this class has no need on its ownself, hence explicit implementation.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {

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
            var login = currentWindowsIdentity.Name;
            var username = login.Contains("\\") ? login.Split('\\')[1] : login;
            var user = GetAuthenticatedUser(username);

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
            return ApiHelper.GetData<UserDto>(ApiConstants.ApiUsers, login, hashPassword);
        }

        /// <summary>
        /// Gets Authenticated an user.
        /// </summary>
        /// <param name="login">The user name.</param>
        /// <returns></returns>
        private UserDto GetAuthenticatedUser(string login)
        {
            return ApiHelper.GetData<UserDto>(ApiConstants.ApiUsers, login);
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
                IList<Claim> claimCollection = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Login)
                    , new Claim(ClaimTypes.GivenName, user.Name)
                    , new Claim(ClaimTypes.Email, user.Mail)
                    , new Claim("RosterId", Settings.Default.RosterId.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claimCollection, "Admin");
                return new ClaimsPrincipal(claimsIdentity);
            }

            return null;
        }

        #endregion Private Methods
    }
}