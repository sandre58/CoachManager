using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.RosterModule.Aggregate;
using My.CoachManager.Infrastructure.Data.Core;

namespace My.CoachManager.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Class representing a SquadRepository.
    /// </summary>
    public class SquadRepository : GenericRepository<Squad>, ISquadRepository
    {
        #region ----- Constructor -----

        /// <summary>
        /// Initializes a new instance of the <see cref="SquadRepository"/> class.
        /// </summary>
        /// <param name="context">Context EF.</param>
        /// <param name="logger">The logger.</param>
        public SquadRepository(IQueryableUnitOfWork context, ILogger logger)
            : base(context, logger)
        {
        }

        #endregion ----- Constructor -----
    }
}