using My.CoachManager.Application.Dtos.Users;

namespace My.CoachManager.Presentation.Core.Services.Interfaces
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
        UserDto Authenticate(string username, string password);

        /// <summary>
        /// Authenticate an user.
        /// </summary>
        /// <returns></returns>
        UserDto AuthenticateByWindowsCredentials();
    }
}