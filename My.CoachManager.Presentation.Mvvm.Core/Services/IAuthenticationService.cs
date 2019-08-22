using System.Security.Principal;

namespace My.CoachManager.Presentation.Mvvm.Core.Services
{
    /// <summary>
    /// The appearance service interface.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticate an user.
        /// </summary>
        /// <param name="username">The user name.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        IPrincipal Authenticate(string username, string password);

        /// <summary>
        /// Authenticate an user.
        /// </summary>
        /// <returns></returns>
        IPrincipal AuthenticateByWindowsCredentials();
    }
}