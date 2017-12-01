using System.Collections.Generic;
using System.ServiceModel;
using My.CoachManager.Application.Dtos.Rosters;

namespace My.CoachManager.Services.Wcf.Interfaces
{
    /// <summary>
    /// Roster Service Interface.
    /// </summary>
    [ServiceContract]
    public interface IRosterService
    {
        /// <summary>
        /// Get squads list.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<SquadDto> GetSquads(int rosterId);

        /// <summary>
        /// Get all roster's players.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<PlayerDetailDto> GetPlayers(int rosterId);
    }
}