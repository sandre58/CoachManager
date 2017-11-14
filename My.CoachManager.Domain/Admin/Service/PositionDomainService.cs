using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.Admin.Aggregate;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.Admin.Service
{
    public class PositionDomainService : DomainService, IPositionDomainService
    {
        #region Fields

        private readonly IPositionRepository _positionRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PositionDomainService"/>.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="positionRepository"></param>
        public PositionDomainService(ILogger logger, IPositionRepository positionRepository)
            : base(logger)
        {
            _positionRepository = positionRepository;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Check if Position is unique.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool CheckPositionIsUnique(Position position)
        {
            return !_positionRepository.Any(DataSpecification.IsUnique(position));
        }

        #endregion Methods
    }
}