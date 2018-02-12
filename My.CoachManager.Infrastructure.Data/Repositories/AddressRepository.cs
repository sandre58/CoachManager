using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.AddressModule.Aggregate;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Infrastructure.Data.Core;

namespace My.CoachManager.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Class representing a AddressRepository.
    /// </summary>
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        #region ----- Constructor -----

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressRepository"/> class.
        /// </summary>
        /// <param name="context">Context EF.</param>
        /// <param name="logger">The logger.</param>
        public AddressRepository(IQueryableUnitOfWork context, ILogger logger)
            : base(context, logger)
        {
        }

        #endregion ----- Constructor -----
    }
}