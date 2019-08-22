using My.CoachManager.Application.Dtos;
using System.Collections.Generic;

namespace My.CoachManager.Application.Services.RosterModule
{
    /// <summary>
    /// Interface defining the roster application services.
    /// </summary>
    public interface IRosterAppService
    {
        /// <summary>
        /// Get all dtos list.
        /// </summary>
        /// <returns></returns>
        IList<RosterDto> GetRosters();

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        int SaveRoster(RosterDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>
        void RemoveRoster(int id);

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        RosterDto GetRosterById(int id);

        /// <summary>
        /// Gets players.
        /// </summary>
        /// <returns></returns>
        IList<RosterPlayerDto> GetPlayers(int rosterId);

        /// <summary>
        /// Add players in rosters.
        /// </summary>
        /// <returns></returns>
        void AddPlayers(int squadId, IEnumerable<int> playerIds);

        /// <summary>
        /// Add players in squad.
        /// </summary>
        /// <returns></returns>
        void MovePlayersInSquad(int squadId, IEnumerable<int> playerIds);

        /// <summary>
        /// Remove players in rosters.
        /// </summary>
        /// <returns></returns>
        void RemovePlayers(int rosterId, IEnumerable<int> playerIds);

        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        RosterPlayerDto GetRosterPlayerById(int id);

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        int UpdatePlayer(RosterPlayerDto dto);
    }
}