using System;
using My.CoachManager.Application.Core;
using My.CoachManager.Application.Dtos.Categories;
using My.CoachManager.Application.Dtos.Persons;

namespace My.CoachManager.Application.Services.Persons
{
    /// <summary>
    /// Interface defining the player application services.
    /// </summary>
    public interface IPlayerAppService : IAppService
    {
        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        PlayerDetailDto GetPlayer(int playerId);

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