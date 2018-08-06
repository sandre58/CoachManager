using CommonServiceLocator;
using My.CoachManager.Application.Dtos.User;
using My.CoachManager.Application.Services.UserModule;
using My.CoachManager.Services.Wcf.Interfaces;

namespace My.CoachManager.Services.Wcf
{
    /// <summary>
    /// Player Service.
    /// </summary>
    public class UserService : IUserService
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public UserDto GetUserById(int id)
        {
            return ServiceLocator.Current.GetInstance<IUserAppService>().GetById(id);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public UserDto GetUserByLoginAndPassword(string login, string password)
        {
            return ServiceLocator.Current.GetInstance<IUserAppService>().GetByLoginAndPassword(login, password);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public UserDto GetUserByLogin(string login)
        {
            return ServiceLocator.Current.GetInstance<IUserAppService>().GetByLogin(login);
        }

    }
}