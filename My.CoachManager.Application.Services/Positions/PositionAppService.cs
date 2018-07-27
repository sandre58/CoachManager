using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos.Positions;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.PositionModule.Aggregate;
using My.CoachManager.Domain.PositionModule.Service;

namespace My.CoachManager.Application.Services.Positions
{
    /// <summary>
    /// Implementation of the IPositionAppService class.
    /// </summary>
    public class PositionAppService : IPositionAppService
    {
        #region ---- Fields ----

        private readonly IRepository<Position> _positionRepository;

        private readonly IPositionDomainService _positionDomainService;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionAppService"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="positionRepository"></param>
        /// <param name="positionDomainService"></param>
        public PositionAppService(ILogger logger, IRepository<Position> positionRepository, IPositionDomainService positionDomainService)
        {
            _positionRepository = positionRepository;
            _positionDomainService = positionDomainService;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public PositionDto CreateOrUpdate(PositionDto dto)
        {
            return null;
            //var entity = dto.ToEntity<Position>();
            //if (!_positionDomainService.IsUnique(entity))
            //{
            //    throw new BusinessException(string.Format(ValidationMessageResources.AlreadyExistMessage, entity.Code));
            //}

            ////_positionRepository.AddOrModify(entity);

            //_positionRepository.UnitOfWork.Commit();

            //return entity.ToDto<PositionDto>();
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void Remove(PositionDto dto)
        {
            //_positionRepository.Remove(dto.ToEntity<Position>());

            //_positionRepository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public PositionDto GetById(int id)
        {
            return null;
            //return _positionRepository.GetEntity(id).ToDto<PositionDto>();
        }

        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PositionDto> GetList()
        {
            return _positionRepository.GetAll(PositionSelectBuilder.SelectPositionForList()).ToArray();
        }

        /// <summary>
        /// Update items Orders.
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateOrders(IDictionary<int, int> entities)
        {
            foreach (var entity in entities)
            {
                _positionRepository.GetEntity(entity.Key).Order = entity.Value;
            }

            _positionRepository.UnitOfWork.Commit();
        }

        #endregion Methods
    }
}