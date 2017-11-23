using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.UserModule.Aggregate;
using My.CoachManager.Infrastructure.Data.Core;

namespace My.CoachManager.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Class representing a BeneficiaryRepository.
    /// </summary>
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        #region ----- Constructor -----

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">Context EF.</param>
        /// <param name="logger">The logger.</param>
        public UserRepository(IQueryableUnitOfWork context, ILogger logger)
            : base(context, logger)
        {
        }

        #endregion ----- Constructor -----
    }
}