using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.AppModule.Services;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.InjuryModule.Aggregates;
using My.CoachManager.Domain.InjuryModule.Services;

namespace My.CoachManager.Application.Services.InjuryModule
{
    /// <summary>
    /// Implementation of the IPlayerAppService class.
    /// </summary>
    public class InjuryAppService : IInjuryAppService
    {
        #region ---- Fields ----

        private readonly IRepository<Injury> _injuryRepository;
        private readonly IInjuryDomainService _injuryDomainService;
        private readonly ICrudDomainService<Injury, InjuryDto> _crudDomainService;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="InjuryAppService"/> class.
        /// </summary>
        public InjuryAppService(
            IRepository<Injury> injuryRepository, ICrudDomainService<Injury, InjuryDto> crudDomainService,
            IInjuryDomainService injuryDomainService)
        {
            _injuryRepository = injuryRepository;
            _crudDomainService = crudDomainService;
            _injuryDomainService = injuryDomainService;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        public InjuryDto GetInjuryById(int id)
        {
            return InjuryFactory.Get(_injuryRepository.GetEntity(id));
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public int SaveInjury(int playerId, InjuryDto dto)
        {
            dto.PlayerId = playerId;
            return _crudDomainService.Save(dto, InjuryFactory.CreateEntity, InjuryFactory.UpdateEntity, x => _injuryDomainService.Validate(x));
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void RemoveInjury(int id)
        {
            _crudDomainService.Remove(id);
        }

        #endregion Methods
    }
}
