using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Core;
using My.CoachManager.Application.Dtos.Administration;
using My.CoachManager.Application.Dtos.Mapping;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.SeasonModule.Aggregate;
using My.CoachManager.Domain.SeasonModule.Service;

namespace My.CoachManager.Application.Services.Seasons
{
    /// <summary>
    /// Implementation of the ISeasonAppService class.
    /// </summary>
    public class SeasonAppService : AppService, ISeasonAppService
    {
        #region ---- Fields ----

        private readonly ISeasonRepository _seasonRepository;

        private readonly ISeasonDomainService _seasonDomainService;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="SeasonAppService"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="seasonRepository"></param>
        /// <param name="seasonDomainService"></param>
        public SeasonAppService(ILogger logger, ISeasonRepository seasonRepository, ISeasonDomainService seasonDomainService)
            : base(logger)
        {
            _seasonRepository = seasonRepository;
            _seasonDomainService = seasonDomainService;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public SeasonDto CreateOrUpdate(SeasonDto dto)
        {
            var entity = dto.ToEntity<Season>();
            if (!_seasonDomainService.IsUnique(entity))
            {
                throw new BusinessException(string.Format(ValidationMessageResources.AlreadyExistMessage, entity.Code));
            }

            _seasonRepository.AddOrModify(entity);

            _seasonRepository.UnitOfWork.Commit();

            return entity.ToDto<SeasonDto>();
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void Remove(SeasonDto dto)
        {
            _seasonRepository.Remove(dto.ToEntity<Season>());

            _seasonRepository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public SeasonDto GetById(int id)
        {
            return _seasonRepository.GetEntity(id).ToDto<SeasonDto>();
        }

        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SeasonDto> GetList()
        {
            return _seasonRepository.GetAll(SeasonSelectBuilder.SelectSeasonForList()).ToArray();
        }

        /// <summary>
        /// Update items Orders.
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateOrders(IDictionary<int, int> entities)
        {
            foreach (var entity in entities)
            {
                _seasonRepository.GetEntity(entity.Key).Order = entity.Value;
            }

            _seasonRepository.UnitOfWork.Commit();
        }

        #endregion Methods
    }
}