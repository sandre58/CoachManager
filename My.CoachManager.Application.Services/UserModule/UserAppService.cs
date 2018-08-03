using System.Linq;
using My.CoachManager.Application.Dtos.Users;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.UserModule.Aggregate;

namespace My.CoachManager.Application.Services.UserModule
{
    /// <summary>
    /// Implementation of the ICategoryAppService class.
    /// </summary>
    public class UserAppService : IUserAppService
    {
        #region ---- Fields ----

        private readonly IRepository<User> _userRepository;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAppService"/> class.
        /// </summary>
        /// <param name="userRepository"></param>
        public UserAppService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public UserDto GetById(int id)
        {
            return UserFactory.Get(_userRepository.GetEntity(id));
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public UserDto GetByLoginAndPassword(string login, string password)
        {
            return UserFactory.Get(_userRepository.GetBySpec(UserSpecification.GetUserByCredentials(login, password), x => x.Roles.Select(r => r.Permissions)).FirstOrDefault());
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public UserDto GetByLogin(string login)
        {
            return UserFactory.Get(_userRepository.GetBySpec(UserSpecification.GetUserByLogin(login), x => x.Roles.Select(r => r.Permissions)).FirstOrDefault());
        }

        #endregion Methods
    }
}