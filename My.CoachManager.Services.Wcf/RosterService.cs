﻿using System.Collections.Generic;
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