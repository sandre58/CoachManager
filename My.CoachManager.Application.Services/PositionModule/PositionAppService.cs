using System.Collections.Generic;
using System.Linq;

using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.PositionModule.Aggregate;
using My.CoachManager.Domain.ReferenceModule.Aggregate;

namespace My.CoachManager.Application.Services.PositionModule
{
    /// <summary>
    /// Implementation of the IPositionAppService class.
    /// </summary>
    public class PositionAppService : IPositionAppService
    {
        #region ---- Fields ----

        private readonly IRepository<Position> _positionRepository;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionAppService"/> class.
        /// </summary>
        /// <param name="positionRepository"></param>
        public PositionAppService(IRepository<Position> positionRepository)
        {
            _positionRepository = positionRepository;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IList<PositionDto> GetPositions()
        {
            return _positionRepository.GetAll(PositionSelectBuilder.SelectPositions(), ReferenceOrderBuilder.OrderByOrder<Position>()).ToList();
        }

        #endregion Methods
    }
}
