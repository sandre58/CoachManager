using System.Collections.Generic;
using My.CoachManager.Application.Core;
using My.CoachManager.Application.Dtos.Rosters;

namespace My.CoachManager.Application.Services.Rosters
{
    /// <summary>
    /// Interface defining the roster application services.
    /// </summary>
    public interface IRosterAppService : IAppService
    {
        /// <summary>
        /// Get all roster's squads.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SquadDto> GetSquads(int rosterId);

        /// <summary>
        /// Get all roster's players.
        /// </summary>
        /// <returns></returns>
        IEnumerable<PlayerDetailDto> GetPlayers(int rosterId);
    }
}