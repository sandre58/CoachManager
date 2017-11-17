using System;
using System.Collections.Generic;
using My.CoachManager.Application.Core;
using My.CoachManager.Application.Dtos.Admin;
using My.CoachManager.Application.Dtos.Persons;

namespace My.CoachManager.Application.Services.Admin
{
    /// <summary>
    /// Interface defining the Player application services.
    /// </summary>
    public interface IPlayerAppService : IAppService
    {
        /// <summary>
        /// Get all dtos list.
        /// </summary>
        /// <returns></returns>
        IEnumerable<PlayerDto> GetList();

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
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        PlayerDto GetById(int id);

        /// <summary>
        /// Load all countries.
        /// </summary>
        /// <returns></returns>
        IEnumerable<CountryDto> GetCountries();

        /// <summary>
        /// Load all cities.
        /// </summary>
        /// <returns></returns>
        IEnumerable<CityDto> GetCities();

        /// <summary>
        /// Load all categories.
        /// </summary>
        /// <returns></returns>
        IEnumerable<CategoryDto> GetCategories();

        /// <summary>
        /// Get category from birthdate.
        /// </summary>
        /// <returns></returns>
        CategoryDto GetCategoryFromBirthdate(DateTime date);
    }
}