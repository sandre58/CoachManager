using System.Linq;
using My.CoachManager.Application.Core;
using My.CoachManager.Application.Dtos.Mapping;
using My.CoachManager.Application.Dtos.Users;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.User.Aggregate;

namespace My.CoachManager.Application.Services.Users
{
    /// <summary>
    /// Implementation of the ICategoryAppService class.
    /// </summary>
    public class UserAppService : AppService, IUserAppService
    {
        #region ---- Fields ----

        private readonly IUserRepository _userRepository;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAppService"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userRepository"></param>
        public UserAppService(ILogger logger, IUserRepository userRepository)
            : base(logger)
        {
            _userRepository = userRepository;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public UserDto GetById(int id)
        {
            return _userRepository.GetEntity(id).ToDto<UserDto>();
        }

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public UserDto GetByLoginAndPassword(string login, string password)
        {
            return _userRepository.GetBySpec(UserSpecification.GetUserByCredentials(login, password), x => x.Roles.Select(r => r.Permissions)).FirstOrDefault().ToDto<UserDto>();
        }

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public UserDto GetByLogin(string login)
        {
            return _userRepository.GetBySpec(UserSpecification.GetUserByLogin(login), x => x.Roles.Select(r => r.Permissions)).FirstOrDefault().ToDto<UserDto>();
        }


        #endregion Methods
    }
}