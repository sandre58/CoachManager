using System.Linq;
using My.CoachManager.Application.Core;
using My.CoachManager.Application.Dtos.Mapping;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.Application.Dtos.Rosters;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.PersonModule.Aggregate;
using My.CoachManager.Domain.RosterModule.Aggregate;

namespace My.CoachManager.Application.Services.Rosters
{
    /// <summary>
    /// Implementation of the IPlayerAppService class.
    /// </summary>
    public class PlayerAppService : AppService, IPlayerAppService
    {
        #region ---- Fields ----

        private readonly IPlayerRepository _playerRepository;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="RosterAppService"/> class.
        /// </summary>
        public PlayerAppService(ILogger logger, IPlayerRepository playerRepository)
            : base(logger)
        {
            _playerRepository = playerRepository;
        }

        #endregion ---- Constructors ----
    }
}