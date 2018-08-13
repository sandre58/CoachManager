using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.Domain.AppModule.Services;
using My.CoachManager.Domain.SeasonModule.Aggregate;
using My.CoachManager.Domain.SeasonModule.Services;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.ReferenceModule.Aggregate;

namespace My.CoachManager.Application.Services.SeasonModule
{
    /// <summary>
    /// Implementation of the ISeasonAppService class.
    /// </summary>
    public class SeasonAppService : ISeasonAppService
    {
        #region ---- Fields ----

        private readonly IRepository<Season> _seasonRepository;

        private readonly ICrudDomainService<Season, SeasonDto> _crudDomainService;

        private readonly ISeasonDomainService _seasonDomainService;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="SeasonAppService"/> class.
        /// </summary>
        /// <param name="seasonRepository"></param>
        /// <param name="crudDomainService"></param>
        /// <param name="seasonDomainService"></param>
        public SeasonAppService(IRepository<Season> seasonRepository, ICrudDomainService<Season, SeasonDto> crudDomainService, ISeasonDomainService seasonDomainService)
        {
            _seasonRepository = seasonRepository;
            _seasonDomainService = seasonDomainService;
            _crudDomainService = crudDomainService;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Save a dto.
        /// </summary>
        /// <returns></returns>
        public SeasonDto SaveSeason(SeasonDto dto)
        {
            return _crudDomainService.Save(dto, SeasonFactory.CreateEntity, SeasonFactory.UpdateEntity, x => _seasonDomainService.Validate(x));
        }

        /// <inheritdoc />
        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void RemoveSeason(SeasonDto dto)
        {
            if (_seasonDomainService.IsUsed(dto.Id))
            {
                throw new IsUsedException(dto.Label);
            }

            _crudDomainService.Remove(dto);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public SeasonDto GetSeasonById(int id)
        {
            var entity = _seasonRepository.GetEntity(id);
            return entity != null ? SeasonFactory.Get(entity) : null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IList<SeasonDto> GetSeasons()
        {
            return _seasonRepository.GetAll(SeasonSelectBuilder.SelectSeasons(), ReferenceOrderBuilder.OrderByOrder<Season>()).ToList();
        }

        /// <inheritdoc />
        /// <summary>
        /// Update items Orders.
        /// </summary>
        /// <param name="values"></param>
        public void UpdateOrders(IDictionary<int, int> values)
        {
            var entities = _seasonRepository.GetByFilter(x => values.Keys.Contains(x.Id), ReferenceOrderBuilder.OrderByOrder<Season>()).ToList();

            _seasonDomainService.UpdateOrders(entities.ToDictionary(x => x, x => values[x.Id]));
        }

        #endregion Methods
    }
}