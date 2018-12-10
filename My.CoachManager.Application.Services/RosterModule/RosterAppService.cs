using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos;
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
        private readonly IRepository<RosterPlayer> _playerRosterRepository;

        private readonly ICrudDomainService<Roster, RosterDto> _crudDomainService;
        private readonly IRosterDomainService _rosterDomainService;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="RosterAppService"/> class.
        /// </summary>
        /// <param name="rosterRepository"></param>
        /// <param name="playerRosterRepository"></param>
        /// <param name="crudDomainService"></param>
        /// <param name="rosterDomainService"></param>
        public RosterAppService(IRepository<Roster> rosterRepository, IRepository<RosterPlayer> playerRosterRepository, ICrudDomainService<Roster, RosterDto> crudDomainService, IRosterDomainService rosterDomainService)
        {
            _rosterRepository = rosterRepository;
            _playerRosterRepository = playerRosterRepository;
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

        /// <inheritdoc />
        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IList<RosterPlayerDto> GetPlayers(int rosterId)
        {
            return _playerRosterRepository.GetAll(RosterSelectBuilder.SelectRosterPlayers(), x => x.Player.Contacts, x => x.Player.Category, x => x.Player.Address, x => x.Player.Country).ToList();
        }

        /// <summary>
        /// Add players in rosters.
        /// </summary>
        /// <returns></returns>
        public void AddPlayers(int rosterId, IEnumerable<int> playerIds)
        {
            foreach (var id in playerIds)
            {
                _playerRosterRepository.Add(RosterFactory.CreatePlayer(rosterId, id));
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
            var player = _playerRosterRepository.GetEntity(id,
                x => x.Player,
                x => x.Player.Category,
                x => x.Player.Address,
                x => x.Player.Contacts,
                x => x.Player.Country);

            return RosterFactory.GetPlayer(player);
        }

        #endregion Methods
    }
}