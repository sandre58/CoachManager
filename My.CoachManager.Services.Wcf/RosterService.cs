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
        public void AddPlayers(int squadId, IEnumerable<int> playerIds)
        {
            ServiceLocator.Current.GetInstance<IRosterAppService>().AddPlayers(squadId, playerIds);
        }

        /// <summary>
        /// Add players in squad.
        /// </summary>
        /// <returns></returns>
         public void MovePlayersInSquad(int squadId, IEnumerable<int> playerIds)
        {
            ServiceLocator.Current.GetInstance<IRosterAppService>().MovePlayersInSquad(squadId, playerIds);
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

        /// <summary>
        /// Get all dtos list.
        /// </summary>
        /// <returns></returns>
        public IList<SquadDto> GetSquads(int rosterId)
        {
            return ServiceLocator.Current.GetInstance<ISquadAppService>().GetSquads(rosterId);
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public int SaveSquad(SquadDto dto)
        {
            return ServiceLocator.Current.GetInstance<ISquadAppService>().SaveSquad(dto);
        }

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>
        public void RemoveSquad(SquadDto dto)
        {
            ServiceLocator.Current.GetInstance<ISquadAppService>().RemoveSquad(dto);
        }

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public SquadDto GetSquadById(int id)
        {
            return ServiceLocator.Current.GetInstance<ISquadAppService>().GetSquadById(id);
        }

        /// <summary>
        /// Gets Roster From Squad
        /// </summary>
        /// <param name="squadId"></param>
        /// <returns></returns>
        public RosterDto GetRosterFromSquad(int squadId)
        {
            return ServiceLocator.Current.GetInstance<ISquadAppService>().GetRosterFromSquad(squadId);
        }
    }
}