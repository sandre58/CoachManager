using System;
using My.CoachManager.Application.Dtos.Category;
using My.CoachManager.Application.Dtos.Persons;

namespace My.CoachManager.Application.Services.Persons
{
    /// <summary>
    /// Interface defining the player application services.
    /// </summary>
    public interface IPlayerAppService
    {
        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        PlayerDetailsDto GetPlayerDetails(int playerId);

        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        PlayerDto GetPlayer(int playerId);

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        PlayerDto CreateOrUpdate(PlayerDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>
        void Remove(PlayerDto dto);

        /// <summary>
        /// Get category from birthdate.
        /// </summary>
        /// <returns></returns>
        CategoryDto GetCategoryFromBirthdate(DateTime date);
    }
}