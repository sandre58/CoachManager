using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.Person.Aggregate;
using My.CoachManager.Infrastructure.Data.Core;

namespace My.CoachManager.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Class representing a BeneficiaryRepository.
    /// </summary>
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        #region ----- Constructor -----

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryRepository"/> class.
        /// </summary>
        /// <param name="context">Context EF.</param>
        /// <param name="logger">The logger.</param>
        public CountryRepository(IQueryableUnitOfWork context, ILogger logger)
            : base(context, logger)
        {
        }

        #endregion ----- Constructor -----
    }
}