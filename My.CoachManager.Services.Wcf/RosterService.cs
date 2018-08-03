using System.Collections.Generic;
using CommonServiceLocator;
using My.CoachManager.Application.Dtos.Rosters;
using My.CoachManager.Application.Services.Rosters;
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
            return ServiceLocator.Current.GetInstance<IRosterAppService>().GetSquads(rosterId);
        }

        /// <summary>
        /// Get squad.
        /// </summary>
        /// <returns></returns>
        public SquadDto GetSquad(int squadId)
        {
            return ServiceLocator.Current.GetInstance<IRosterAppService>().GetSquad(squadId);
        }
    }
}