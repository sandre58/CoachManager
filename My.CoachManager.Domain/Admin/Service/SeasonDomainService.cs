using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.Admin.Aggregate;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.Admin.Service
{
    public class SeasonDomainService : DomainService, ISeasonDomainService
    {
        #region Fields

        private readonly ISeasonRepository _seasonRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SeasonDomainService"/>.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="seasonRepository"></param>
        public SeasonDomainService(ILogger logger, ISeasonRepository seasonRepository)
            : base(logger)
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
            return !_seasonRepository.Any(DataSpecification.IsUnique(item));
        }

        #endregion Methods
    }
}