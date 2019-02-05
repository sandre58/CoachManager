using My.CoachManager.Application.Dtos;
using System;
using System.Collections.Generic;

namespace My.CoachManager.Application.Services.PersonModule
{
    /// <summary>
    /// Interface defining the player application services.
    /// </summary>
    public interface IPlayerAppService
    {
        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        IList<PlayerDto> GetPlayers();

        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        PlayerDto GetPlayerById(int playerId);

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        int SavePlayer(PlayerDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>
        void RemovePlayer(PlayerDto dto);

        /// <summary>
        /// Get category from birthdate.
        /// </summary>
        /// <returns></returns>
        CategoryDto GetCategoryFromDate(DateTime fromDate, DateTime toDate);
    }
}