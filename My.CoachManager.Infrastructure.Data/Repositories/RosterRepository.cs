using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.RosterModule.Aggregate;
using My.CoachManager.Infrastructure.Data.Core;

namespace My.CoachManager.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Class representing a AddressRepository.
    /// </summary>
    public class RosterRepository : GenericRepository<Roster>, IRosterRepository
    {
        #region ----- Constructor -----

        /// <summary>
        /// Initializes a new instance of the <see cref="RosterRepository"/> class.
        /// </summary>
        /// <param name="context">Context EF.</param>
        /// <param name="logger">The logger.</param>
        public RosterRepository(IQueryableUnitOfWork context, ILogger logger)
            : base(context, logger)
        {
        }

        #endregion ----- Constructor -----
    }
}