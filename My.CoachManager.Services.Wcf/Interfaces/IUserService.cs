using System.ServiceModel;

using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Services.Wcf.Interfaces
{
    /// <summary>
    /// Administration Service Interface.
    /// </summary>
    [ServiceContract]
    public interface IUserService
    {
        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        UserDto GetUserById(int id);

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        UserDto GetUserByLoginAndPassword(string login, string password);

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        UserDto GetUserByLogin(string login);
    }
}
