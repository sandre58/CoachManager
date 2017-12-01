using System.Collections.Generic;
using My.CoachManager.Application.Dtos.Rosters;
using My.CoachManager.Application.Services.Rosters;
using My.CoachManager.CrossCutting.Unity;
using My.CoachManager.Services.Wcf.Interfaces;

namespace My.CoachManager.Services.Wcf
{
    /// <summary>
    /// Roster Service.
    /// </summary>
    public class RosterService : IRosterService
    {
        /// <summary>
        /// Gets squads list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SquadDto> GetSquads(int rosterId)
        {
            return UnityFactory.Resolve<IRosterAppService>().GetSquads(rosterId);
        }

        /// <summary>
        /// Get all roster's players.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PlayerDetailDto> GetPlayers(int rosterId)
        {
            return UnityFactory.Resolve<IRosterAppService>().GetPlayers(rosterId);
        }
    }
}