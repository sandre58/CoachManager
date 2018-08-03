using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.ReferenceModule.Aggregates;

namespace My.CoachManager.Domain.PositionModule.Services
{
    public class PositionDomainService : IPositionDomainService
    {
        #region Fields

        private readonly IRepository<Position> _positionRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PositionDomainService"/>.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="positionRepository"></param>
        public PositionDomainService(ILogger logger, IRepository<Position> positionRepository)
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
        public bool IsUnique(Position position)
        {
            return !_positionRepository.Any(ReferenceSpecification.IsUnique(position));
        }

        #endregion Methods
    }
}