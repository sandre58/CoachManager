using My.CoachManager.Application.Core;
using My.CoachManager.Application.Dtos.Users;

namespace My.CoachManager.Application.Services.Users
{
    /// <summary>
    /// Interface defining the category application services.
    /// </summary>
    public interface IUserAppService : IAppService
    {
        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        UserDto GetById(int id);

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        UserDto GetByLoginAndPassword(string login, string password);

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        UserDto GetByLogin(string login);

    }
}