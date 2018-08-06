using System.Collections.Generic;
using My.CoachManager.Application.Dtos.Roster;

namespace My.CoachManager.Application.Services.RosterModule
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

        /// <summary>
        /// Get all dtos list.
        /// </summary>
        /// <returns></returns>
        IList<RosterDto> GetRosters();

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        RosterDto SaveRoster(RosterDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>
        void RemoveRoster(RosterDto dto);

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        RosterDto GetRosterById(int id);
    }
}