using System.Collections.Generic;
using CommonServiceLocator;
using My.CoachManager.Application.Dtos.Roster;
using My.CoachManager.Application.Services.RosterModule;
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

        /// <inheritdoc />
        /// <summary>
        /// Save a dto.
        /// </summary>
        /// <returns></returns>
        public RosterDto SaveRoster(RosterDto dto)
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
    }
}