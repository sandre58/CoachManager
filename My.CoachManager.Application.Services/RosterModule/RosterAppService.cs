using Microsoft.EntityFrameworkCore;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Services.PersonModule;
using My.CoachManager.Domain.AppModule.Services;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.RosterModule.Aggregate;
using My.CoachManager.Domain.RosterModule.Services;
using System.Collections.Generic;
using System.Linq;

namespace My.CoachManager.Application.Services.RosterModule
{
    /// <summary>
    /// Implementation of the IRosterAppService class.
    /// </summary>
    public class RosterAppService : IRosterAppService
    {
        #region ---- Fields ----

        private readonly IRepository<Roster> _rosterRepository;
        private readonly IRepository<RosterPlayer> _playerRosterRepository;

        private readonly ICrudDomainService<Roster, RosterDto> _crudDomainService;
        private readonly IRosterDomainService _rosterDomainService;

        private readonly IPlayerAppService _playerAppService;
        private readonly ISquadAppService _squadAppService;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="RosterAppService"/> class.
        /// </summary>
        /// <param name="rosterRepository"></param>
        /// <param name="playerRosterRepository"></param>
        /// <param name="crudDomainService"></param>
        /// <param name="rosterDomainService"></param>
        /// <param name="playerAppService"></param>
        /// <param name="squadAppService"></param>
        public RosterAppService(IRepository<Roster> rosterRepository,
            IRepository<RosterPlayer> playerRosterRepository,
            ICrudDomainService<Roster, RosterDto> crudDomainService,
            IRosterDomainService rosterDomainService,
            IPlayerAppService playerAppService,
            ISquadAppService squadAppService)
        {
            _rosterRepository = rosterRepository;
            _playerRosterRepository = playerRosterRepository;
            _crudDomainService = crudDomainService;
            _rosterDomainService = rosterDomainService;
            _playerAppService = playerAppService;
            _squadAppService = squadAppService;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Save a dto.
        /// </summary>
        /// <returns></returns>
        public int SaveRoster(RosterDto dto)
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
            var entity = _rosterRepository.GetEntity(id, x => x.Squads, x => x.Season);
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
        public IList<RosterPlayerDto> GetPlayers(int rosterId)
        {
            return _playerRosterRepository.Query
                .Include(x => x.Player)
                    .ThenInclude(x => x.Address)
                .Include(x => x.Player)
                    .ThenInclude(x => x.Contacts)
                .Include(x => x.Player)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Player)
                    .ThenInclude(x => x.Country)
                .Include(x => x.Player)
                    .ThenInclude(x => x.Positions)
                    .ThenInclude(x => x.Position)
                .Where(x => x.RosterId == rosterId)
                .Select(RosterSelectBuilder.SelectRosterPlayers()).ToList();
        }

        /// <summary>
        /// Add players in rosters.
        /// </summary>
        /// <returns></returns>
        public void AddPlayers(int squadId, IEnumerable<int> playerIds)
        {
            var squad = _squadAppService.GetSquadById(squadId);

            foreach (var id in playerIds)
            {
                    _playerRosterRepository.Add(RosterFactory.CreatePlayer(squad.RosterId, squadId, id));
            }

            _playerRosterRepository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Add players in squad.
        /// </summary>
        /// <returns></returns>
        public void MovePlayersInSquad(int squadId, IEnumerable<int> playerIds)
        {
            foreach (var id in playerIds)
            {
                var player = _playerRosterRepository.GetEntity(id);
                RosterFactory.UpdateSquadPlayer(squadId, player);
                _playerRosterRepository.Modify(player);
            }

            _playerRosterRepository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Remove players in rosters.
        /// </summary>
        /// <returns></returns>
        public void RemovePlayers(int rosterId, IEnumerable<int> playerIds)
        {
            var players = _playerRosterRepository.GetByFilter(x => playerIds.Contains(x.PlayerId) && x.RosterId == rosterId).ToList();
            foreach (var player in players)
            {
                _playerRosterRepository.Remove(player);
            }

            _playerRosterRepository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        public RosterPlayerDto GetRosterPlayerById(int id)
        {
            var player = _playerRosterRepository.Query
                .Include(x => x.Player)
                .ThenInclude(x => x.Address)
                .Include(x => x.Player)
                .ThenInclude(x => x.Contacts)
                .Include(x => x.Player)
                .ThenInclude(x => x.Category)
                .Include(x => x.Player)
                .ThenInclude(x => x.Country)
                .Include(x => x.Player)
                .ThenInclude(x => x.Positions)
                .ThenInclude(x => x.Position)
                .Include(x => x.Squad)
                .FirstOrDefault(x => x.Id == id);

            return RosterFactory.GetPlayer(player);
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public int UpdatePlayer(RosterPlayerDto dto)
        {
            var entity = _playerRosterRepository.GetEntity(dto.Id);

            dto.PlayerId = _playerAppService.SavePlayer(dto.Player);

            RosterFactory.UpdatePlayer(dto, entity);

            _playerRosterRepository.Modify(entity);
            _playerRosterRepository.UnitOfWork.Commit();

            dto.CrudStatus = CrudStatus.Unchanged;

            return entity.Id;
        }

        #endregion Methods
    }
}