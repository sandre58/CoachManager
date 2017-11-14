using System.Collections.Generic;
using System.ServiceModel;
using My.CoachManager.Application.Dtos.Admin;
using My.CoachManager.Application.Dtos.Persons;

namespace My.CoachManager.Services.Wcf.Interfaces
{
    /// <summary>
    /// Administration Service Interface.
    /// </summary>
    [ServiceContract]
    public interface IAdminService
    {
        #region Category

        /// <summary>
        /// Get categories list.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<CategoryDto> GetCategoriesList();

        /// <summary>
        /// Get a category by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        CategoryDto GetCategoryById(int id);

        /// <summary>
        /// Create a category.
        /// </summary>
        /// <param name="playerDto">the player model.</param>
        /// <returns></returns>
        [OperationContract]
        CategoryDto CreateOrUpdateCategory(CategoryDto playerDto);

        /// <summary>
        /// Remove a category.
        /// </summary>
        /// <param name="playerDto">the player model.</param>
        /// <returns></returns>
        [OperationContract]
        void RemoveCategory(CategoryDto playerDto);

        /// <summary>
        /// Update Categories Orders.
        /// </summary>
        /// <param name="entities"></param>
        [OperationContract]
        void UpdateCategoriesOrders(IDictionary<int, int> entities);

        #endregion Category

        #region Season

        /// <summary>
        /// Get Seasons list.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<SeasonDto> GetSeasonsList();

        /// <summary>
        /// Get a Season by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        SeasonDto GetSeasonById(int id);

        /// <summary>
        /// Create a Season.
        /// </summary>
        /// <param name="dto">the player model.</param>
        /// <returns></returns>
        [OperationContract]
        SeasonDto CreateOrUpdateSeason(SeasonDto dto);

        /// <summary>
        /// Remove a Season.
        /// </summary>
        /// <param name="dto">the player model.</param>
        /// <returns></returns>
        [OperationContract]
        void RemoveSeason(SeasonDto dto);

        /// <summary>
        /// Update Seasons Orders.
        /// </summary>
        /// <param name="entities"></param>
        [OperationContract]
        void UpdateSeasonsOrders(IDictionary<int, int> entities);

        #endregion Season

        #region Position

        /// <summary>
        /// Get Positions list.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<PositionDto> GetPositionsList();

        /// <summary>
        /// Get a Position by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        PositionDto GetPositionById(int id);

        /// <summary>
        /// Create a Position.
        /// </summary>
        /// <param name="dto">the player model.</param>
        /// <returns></returns>
        [OperationContract]
        PositionDto CreateOrUpdatePosition(PositionDto dto);

        /// <summary>
        /// Remove a Position.
        /// </summary>
        /// <param name="dto">the player model.</param>
        /// <returns></returns>
        [OperationContract]
        void RemovePosition(PositionDto dto);

        /// <summary>
        /// Update Positions Orders.
        /// </summary>
        /// <param name="entities"></param>
        [OperationContract]
        void UpdatePositionsOrders(IDictionary<int, int> entities);

        #endregion Position

        #region Position

        /// <summary>
        /// Get Positions list.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<PlayerDto> GetPlayersList();

        /// <summary>
        /// Get a Position by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        PlayerDto GetPlayerById(int id);

        /// <summary>
        /// Create a Position.
        /// </summary>
        /// <param name="dto">the player model.</param>
        /// <returns></returns>
        [OperationContract]
        PlayerDto CreateOrUpdatePlayer(PlayerDto dto);

        /// <summary>
        /// Remove a Position.
        /// </summary>
        /// <param name="dto">the player model.</param>
        /// <returns></returns>
        [OperationContract]
        void RemovePlayer(PlayerDto dto);

        /// <summary>
        /// Load all countries.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<CountryDto> GetCountriesForPlayer();

        /// <summary>
        /// Load all cities.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<CityDto> GetCitiesForPlayer();

        /// <summary>
        /// Load all categories.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<CategoryDto> GetCategoriesForPlayer();

        #endregion Position
    }
}