using System.Collections.Generic;
using System.ServiceModel;
using My.CoachManager.Application.Dtos.Position;

namespace My.CoachManager.Services.Wcf.Interfaces
{
    /// <summary>
    /// Administration Service Interface.
    /// </summary>
    [ServiceContract]
    public interface IPositionService
    {
        /// <summary>
        /// Get Positions list.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<PositionDto> GetList();

        /// <summary>
        /// Get a Position by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        PositionDto GetById(int id);

        /// <summary>
        /// Create a Position.
        /// </summary>
        /// <param name="dto">the player model.</param>
        /// <returns></returns>
        [OperationContract]
        PositionDto CreateOrUpdate(PositionDto dto);

        /// <summary>
        /// Remove a Position.
        /// </summary>
        /// <param name="dto">the player model.</param>
        /// <returns></returns>
        [OperationContract]
        void Remove(PositionDto dto);

        /// <summary>
        /// Update Positions Orders.
        /// </summary>
        /// <param name="entities"></param>
        [OperationContract]
        void UpdateOrders(IDictionary<int, int> entities);
    }
}