using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos.Roster;
using My.CoachManager.Domain.AppModule.Services;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.RosterModule.Aggregate;

namespace My.CoachManager.Application.Services.RosterModule
{
    /// <summary>
    /// Implementation of the IRosterAppService class.
    /// </summary>
    public class RosterAppService : IRosterAppService
    {
        #region ---- Fields ----

        private readonly IRepository<Squad> _squadRepository;
        private readonly IRepository<Roster> _rosterRepository;

        private readonly ICrudDomainService<Roster, RosterDto> _crudDomainService;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="RosterAppService"/> class.
        /// </summary>
        /// <param name="rosterRepository"></param>
        /// <param name="crudDomainService"></param>
        /// <param name="squadRepository"></param>
        public RosterAppService(IRepository<Roster> rosterRepository, ICrudDomainService<Roster, RosterDto> crudDomainService, IRepository<Squad> squadRepository)
        {
            _squadRepository = squadRepository;
            _rosterRepository = rosterRepository;
            _crudDomainService = crudDomainService;
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
            return _crudDomainService.Save(dto, RosterFactory.CreateEntity, RosterFactory.UpdateEntity);
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

        /// <inheritdoc />
        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SquadDto> GetSquads(int rosterId)
        {
            return _squadRepository.GetByFilter(RosterSelectBuilder.SelectSquad(), x => x.RosterId == rosterId).ToArray();
        }

        /// <inheritdoc />
        /// <summary>
        /// Get a squad.
        /// </summary>
        /// <returns></returns>
        public SquadDto GetSquad(int squadId)
        {
            var squad = _squadRepository.GetEntity(squadId, x => x.Players.Select(p => p.Player),
                x => x.Players.Select(p => p.Player.Category),
                x => x.Players.Select(p => p.Player.Address),
                x => x.Players.Select(p => p.Player.Country),
                x => x.Players.Select(p => p.Player.Contacts));

            return new SquadDto()
            {
                Id = squad.Id,
                Name = squad.Name,
                Players = squad.Players.Select(RosterSelectBuilder.SelectSquadPlayer()).ToArray()
            };
        }

        #endregion Methods
    }
}