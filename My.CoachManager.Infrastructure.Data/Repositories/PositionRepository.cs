using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.AdministrationModule.Aggregate;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Infrastructure.Data.Core;

namespace My.CoachManager.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Class representing a PositionRepository.
    /// </summary>
    public class PositionRepository : GenericRepository<Position>, IPositionRepository
    {
        #region ----- Constructor -----

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryRepository"/> class.
        /// </summary>
        /// <param name="context">Context EF.</param>
        /// <param name="logger">The logger.</param>
        public PositionRepository(IQueryableUnitOfWork context, ILogger logger)
            : base(context, logger)
        {
        }

        #endregion ----- Constructor -----
    }
}