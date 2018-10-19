using System.Collections.Generic;
using System.ServiceModel;
using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Services.Wcf.Interfaces
{
    /// <summary>
    /// Roster Service Interface.
    /// </summary>
    [ServiceContract]
    public interface IRosterService
    {
        /// <summary>
        /// Get all dtos list.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<RosterDto> GetRosters();

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        RosterDto SaveRoster(RosterDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void RemoveRoster(RosterDto dto);

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        RosterDto GetRosterById(int id);

        /// <summary>
        /// Gets players.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<RosterPlayerDto> GetPlayers(int rosterId);
    }
}