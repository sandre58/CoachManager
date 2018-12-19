using System.Collections.Generic;
using CommonServiceLocator;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Services.RosterModule;
using My.CoachManager.Services.Wcf.Interfaces;

namespace My.CoachManager.Services.Wcf
{
    /// <summary>
    /// Roster Service.
    /// </summary>
    public class RosterService : IRosterService
    {
        /// <inheritdoc />
        /// <summary>
        /// Save a dto.
        /// </summary>
        /// <returns></returns>
        public int SaveRoster(RosterDto dto)
        {
            return ServiceLocator.Current.GetInstance<IRosterAppService>().SaveRoster(dto);
        }

        /// <inheritdoc />
        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void RemoveRoster(RosterDto dto)
        {
            ServiceLocator.Current.GetInstance<IRosterAppService>().RemoveRoster(dto);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public RosterDto GetRosterById(int id)
        {
            return ServiceLocator.Current.GetInstance<IRosterAppService>().GetRosterById(id);
        }

        /// <inheritdoc />
        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IList<RosterDto> GetRosters()
        {
            return ServiceLocator.Current.GetInstance<IRosterAppService>().GetRosters();
        }

        /// <summary>
        /// Gets players.
        /// </summary>
        /// <returns></returns>
        public IList<RosterPlayerDto> GetPlayers(int rosterId)
        {
            return ServiceLocator.Current.GetInstance<IRosterAppService>().GetPlayers(rosterId);
        }

        /// <summary>
        /// Add players in rosters.
        /// </summary>
        /// <returns></returns>
        public void AddPlayers(int rosterId, IEnumerable<int> playerIds)
        {
            ServiceLocator.Current.GetInstance<IRosterAppService>().AddPlayers(rosterId, playerIds);
        }

        /// <summary>
        /// Remove players in rosters.
        /// </summary>
        /// <returns></returns>
        public void RemovePlayers(int rosterId, IEnumerable<int> playerIds)
        {
            ServiceLocator.Current.GetInstance<IRosterAppService>().RemovePlayers(rosterId, playerIds);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get player.
        /// </summary>
        /// <returns></returns>
        public RosterPlayerDto GetRosterPlayerById(int id)
        {
            return ServiceLocator.Current.GetInstance<IRosterAppService>().GetRosterPlayerById(id);
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public int UpdatePlayer(RosterPlayerDto dto)
        {
            return ServiceLocator.Current.GetInstance<IRosterAppService>().UpdatePlayer(dto);
        }
    }
}