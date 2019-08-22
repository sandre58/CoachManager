using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Application.Services.UserModule
{
    /// <summary>
    /// Interface defining the category application services.
    /// </summary>
    public interface IUserAppService 
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
