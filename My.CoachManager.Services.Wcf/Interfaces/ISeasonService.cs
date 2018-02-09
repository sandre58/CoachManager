using System.Collections.Generic;
using System.ServiceModel;
using My.CoachManager.Application.Dtos.Seasons;

namespace My.CoachManager.Services.Wcf.Interfaces
{
    /// <summary>
    /// Administration Service Interface.
    /// </summary>
    [ServiceContract]
    public interface ISeasonService
    {
        /// <summary>
        /// Get Seasons list.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<SeasonDto> GetList();

        /// <summary>
        /// Get a Season by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        SeasonDto GetById(int id);

        /// <summary>
        /// Create a Season.
        /// </summary>
        /// <param name="dto">the player model.</param>
        /// <returns></returns>
        [OperationContract]
        SeasonDto CreateOrUpdate(SeasonDto dto);

        /// <summary>
        /// Remove a Season.
        /// </summary>
        /// <param name="dto">the player model.</param>
        /// <returns></returns>
        [OperationContract]
        void Remove(SeasonDto dto);

        /// <summary>
        /// Update Seasons Orders.
        /// </summary>
        /// <param name="entities"></param>
        [OperationContract]
        void UpdateOrders(IDictionary<int, int> entities);
    }
}