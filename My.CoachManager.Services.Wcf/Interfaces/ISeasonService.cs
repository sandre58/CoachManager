using System.Collections.Generic;
using System.ServiceModel;
using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Services.Wcf.Interfaces
{
    /// <summary>
    /// Administration Service Interface.
    /// </summary>
    [ServiceContract]
    public interface ISeasonService
    {
        /// <summary>
        /// Get categories list.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<SeasonDto> GetSeasons();

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
        /// <param name="playerDto">the player model.</param>
        /// <returns></returns>
        [OperationContract]
        SeasonDto SaveSeason(SeasonDto playerDto);

        /// <summary>
        /// Remove a Season.
        /// </summary>
        /// <param name="playerDto">the player model.</param>
        /// <returns></returns>
        [OperationContract]
        void RemoveSeason(SeasonDto playerDto);

        /// <summary>
        /// Update Categories Orders.
        /// </summary>
        /// <param name="entities"></param>
        [OperationContract]
        void UpdateOrders(IDictionary<int, int> entities);
    }
}