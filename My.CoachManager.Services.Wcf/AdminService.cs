using System;
using System.Collections.Generic;
using My.CoachManager.Application.Dtos.Admin;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.Application.Services.Admin;
using My.CoachManager.CrossCutting.Unity;
using My.CoachManager.Services.Wcf.Interfaces;

namespace My.CoachManager.Services.Wcf
{
    /// <summary>
    /// Player Service.
    /// </summary>
    public class AdminService : IAdminService
    {
        #region Category

        /// <summary>
        /// Get categories list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryDto> GetCategoriesList()
        {
            return UnityFactory.Resolve<ICategoryAppService>().GetList();
        }

        /// <summary>
        /// Get category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CategoryDto GetCategoryById(int id)
        {
            return UnityFactory.Resolve<ICategoryAppService>().GetById(id);
        }

        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public CategoryDto CreateOrUpdateCategory(CategoryDto categoryDto)
        {
            return UnityFactory.Resolve<ICategoryAppService>().CreateOrUpdate(categoryDto);
        }

        /// <summary>
        /// Remove category.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public void RemoveCategory(CategoryDto categoryDto)
        {
            UnityFactory.Resolve<ICategoryAppService>().Remove(categoryDto);
        }

        /// <summary>
        /// Update Categories Orders.
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateCategoriesOrders(IDictionary<int, int> entities)
        {
            UnityFactory.Resolve<ICategoryAppService>().UpdateOrders(entities);
        }

        #endregion Category

        #region Season

        /// <summary>
        /// Get Seasons list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SeasonDto> GetSeasonsList()
        {
            return UnityFactory.Resolve<ISeasonAppService>().GetList();
        }

        /// <summary>
        /// Get Season.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SeasonDto GetSeasonById(int id)
        {
            return UnityFactory.Resolve<ISeasonAppService>().GetById(id);
        }

        /// <summary>
        /// Create Season.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public SeasonDto CreateOrUpdateSeason(SeasonDto categoryDto)
        {
            return UnityFactory.Resolve<ISeasonAppService>().CreateOrUpdate(categoryDto);
        }

        /// <summary>
        /// Remove Season.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public void RemoveSeason(SeasonDto categoryDto)
        {
            UnityFactory.Resolve<ISeasonAppService>().Remove(categoryDto);
        }

        /// <summary>
        /// Update Seasons Orders.
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateSeasonsOrders(IDictionary<int, int> entities)
        {
            UnityFactory.Resolve<ISeasonAppService>().UpdateOrders(entities);
        }

        #endregion Season

        #region Position

        /// <summary>
        /// Get Positions list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PositionDto> GetPositionsList()
        {
            return UnityFactory.Resolve<IPositionAppService>().GetList();
        }

        /// <summary>
        /// Get Position.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PositionDto GetPositionById(int id)
        {
            return UnityFactory.Resolve<IPositionAppService>().GetById(id);
        }

        /// <summary>
        /// Create Position.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public PositionDto CreateOrUpdatePosition(PositionDto categoryDto)
        {
            return UnityFactory.Resolve<IPositionAppService>().CreateOrUpdate(categoryDto);
        }

        /// <summary>
        /// Remove Position.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public void RemovePosition(PositionDto categoryDto)
        {
            UnityFactory.Resolve<IPositionAppService>().Remove(categoryDto);
        }

        /// <summary>
        /// Update Positions Orders.
        /// </summary>
        /// <param name="entities"></param>
        public void UpdatePositionsOrders(IDictionary<int, int> entities)
        {
            UnityFactory.Resolve<IPositionAppService>().UpdateOrders(entities);
        }

        #endregion Position

        #region Player

        /// <summary>
        /// Get Players list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PlayerDto> GetPlayersList()
        {
            return UnityFactory.Resolve<IPlayerAppService>().GetList();
        }

        /// <summary>
        /// Get Player.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlayerDto GetPlayerById(int id)
        {
            return UnityFactory.Resolve<IPlayerAppService>().GetById(id);
        }

        /// <summary>
        /// Create Player.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public PlayerDto CreateOrUpdatePlayer(PlayerDto dto)
        {
            return UnityFactory.Resolve<IPlayerAppService>().CreateOrUpdate(dto);
        }

        /// <summary>
        /// Remove Player.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public void RemovePlayer(PlayerDto dto)
        {
            UnityFactory.Resolve<IPlayerAppService>().Remove(dto);
        }

        /// <summary>
        /// Load all countries.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CountryDto> GetCountriesForPlayer()
        {
            return UnityFactory.Resolve<IPlayerAppService>().GetCountries();
        }

        /// <summary>
        /// Load all cities.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CityDto> GetCitiesForPlayer()
        {
            return UnityFactory.Resolve<IPlayerAppService>().GetCities();
        }

        /// <summary>
        /// Load all categories.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryDto> GetCategoriesForPlayer()
        {
            return UnityFactory.Resolve<IPlayerAppService>().GetCategories();
        }

        /// <summary>
        /// Get category from birthdate.
        /// </summary>
        /// <returns></returns>
        public CategoryDto GetCategoryFromBirthdate(DateTime date)
        {
            return UnityFactory.Resolve<IPlayerAppService>().GetCategoryFromBirthdate(date);
        }

        #endregion Player
    }
}