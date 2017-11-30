using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.CompetitionModule.Aggregate;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Infrastructure.Data.Core;

namespace My.CoachManager.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Class representing a Club Repository.
    /// </summary>
    public class ClubRepository : GenericRepository<Club>, IClubRepository
    {
        #region ----- Constructor -----

        /// <summary>
        /// Initializes a new instance of the <see cref="ClubRepository"/> class.
        /// </summary>
        /// <param name="context">Context EF.</param>
        /// <param name="logger">The logger.</param>
        public ClubRepository(IQueryableUnitOfWork context, ILogger logger)
            : base(context, logger)
        {
        }

        #endregion ----- Constructor -----
    }
}