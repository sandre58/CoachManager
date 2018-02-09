using System.Collections.Generic;
using System.ServiceModel;
using My.CoachManager.Application.Dtos.Persons;
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
        /// Get a squad.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SquadDto GetSquad(int squadId);

        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PlayerDetailDto GetPlayer(int playerId);
    }
}