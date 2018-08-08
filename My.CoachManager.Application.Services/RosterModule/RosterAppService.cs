using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos.Roster;
using My.CoachManager.Domain.AppModule.Services;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.RosterModule.Aggregate;
using My.CoachManager.Domain.RosterModule.Services;

namespace My.CoachManager.Application.Services.RosterModule
{
    /// <summary>
    /// Implementation of the IRosterAppService class.
    /// </summary>
    public class RosterAppService : IRosterAppService
    {
        #region ---- Fields ----
        
        private readonly IRepository<Roster> _rosterRepository;

        private readonly ICrudDomainService<Roster, RosterDto> _crudDomainService;
        private readonly IRosterDomainService _rosterDomainService;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="RosterAppService"/> class.
        /// </summary>
        /// <param name="rosterRepository"></param>
        /// <param name="crudDomainService"></param>
        /// <param name="rosterDomainService"></param>
        public RosterAppService(IRepository<Roster> rosterRepository, ICrudDomainService<Roster, RosterDto> crudDomainService, IRosterDomainService rosterDomainService)
        {
            _rosterRepository = rosterRepository;
            _crudDomainService = crudDomainService;
            _rosterDomainService = rosterDomainService;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Save a dto.
        /// </summary>
        /// <returns></returns>
        public RosterDto SaveRoster(RosterDto dto)
        {
            return _crudDomainService.Save(dto, RosterFactory.CreateEntity, RosterFactory.UpdateEntity, x => _rosterDomainService.Validate(x));
        }

        /// <inheritdoc />
        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void RemoveRoster(RosterDto dto)
        {
            _crudDomainService.Remove(dto);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public RosterDto GetRosterById(int id)
        {
            var entity = _rosterRepository.GetEntity(id);
            return entity != null ? RosterFactory.Get(entity) : null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IList<RosterDto> GetRosters()
        {
            return _rosterRepository.GetAll(RosterSelectBuilder.SelectRosters(), x => x.Category, x => x.Season).ToList();
        }

        #endregion Methods
    }
}