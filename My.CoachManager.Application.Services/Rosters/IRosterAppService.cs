using System.Collections.Generic;
using My.CoachManager.Application.Dtos.Rosters;

namespace My.CoachManager.Application.Services.Rosters
{
    /// <summary>
    /// Interface defining the roster application services.
    /// </summary>
    public interface IRosterAppService
    {
        /// <summary>
        /// Get all roster's squads.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SquadDto> GetSquads(int rosterId);

        /// <summary>
        /// Get a squad.
        /// </summary>
        /// <returns></returns>
        SquadDto GetSquad(int squadId);
    }
}