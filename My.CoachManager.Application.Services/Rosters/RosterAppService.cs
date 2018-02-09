using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Core;
using My.CoachManager.Application.Dtos.Rosters;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.RosterModule.Aggregate;

namespace My.CoachManager.Application.Services.Rosters
{
    /// <summary>
    /// Implementation of the IRosterAppService class.
    /// </summary>
    public class RosterAppService : AppService, IRosterAppService
    {
        #region ---- Fields ----

        private readonly ISquadRepository _squadRepository;
        private readonly IRosterRepository _rosterRepository;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="RosterAppService"/> class.
        /// </summary>
        public RosterAppService(ILogger logger, ISquadRepository squadRepository, IRosterRepository rosterRepository)
            : base(logger)
        {
            _squadRepository = squadRepository;
            _rosterRepository = rosterRepository;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SquadDto> GetSquads(int rosterId)
        {
            return _squadRepository.GetByFilter(RosterSelectBuilder.SelectSquad(), x => x.RosterId == rosterId).ToArray();
        }

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