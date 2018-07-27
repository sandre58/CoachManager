using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.ReferenceModule.Aggregates;

namespace My.CoachManager.Domain.SeasonModule.Service
{
    public class SeasonDomainService : ISeasonDomainService
    {
        #region Fields

        private readonly IRepository<Season> _seasonRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SeasonDomainService"/>.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="seasonRepository"></param>
        public SeasonDomainService(ILogger logger, IRepository<Season> seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Check if category is unique.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool IsUnique(Season item)
        {
            return !_seasonRepository.Any(ReferenceSpecification.IsUnique(item));
        }

        #endregion Methods
    }
}