using System.Collections.Generic;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.Application.Dtos.Rosters;
using My.CoachManager.Application.Services.Persons;
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
        /// Get squad.
        /// </summary>
        /// <returns></returns>
        public SquadDto GetSquad(int squadId)
        {
            return UnityFactory.Resolve<IRosterAppService>().GetSquad(squadId);
        }

        /// <summary>
        /// Get squad.
        /// </summary>
        /// <returns></returns>
        public PlayerDetailDto GetPlayer(int playerId)
        {
            return UnityFactory.Resolve<IPlayerAppService>().GetPlayer(playerId);
        }
    }
}