using System.Collections.Generic;

using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Application.Services.RosterModule
{
    /// <summary>
    /// Interface defining the squad application services.
    /// </summary>
    public interface ISquadAppService
    {
        /// <summary>
        /// Get all dtos list.
        /// </summary>
        /// <returns></returns>
        IList<SquadDto> GetSquads(int rosterId);

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        int SaveSquad(int rosterId, SquadDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>
        void RemoveSquad(int id);

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        SquadDto GetSquadById(int id);

        /// <summary>
        /// Gets Roster From Squad
        /// </summary>
        /// <param name="squadId"></param>
        /// <returns></returns>
        RosterDto GetRosterFromSquad(int squadId);
    }
}
