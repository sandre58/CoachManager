using System.Collections.Generic;
using My.CoachManager.Application.Dtos.Position;

namespace My.CoachManager.Application.Services.Positions
{
    /// <summary>
    /// Interface defining the Position application services.
    /// </summary>
    public interface IPositionAppService
    {
        /// <summary>
        /// Get all dtos list.
        /// </summary>
        /// <returns></returns>
        IEnumerable<PositionDto> GetList();

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        PositionDto CreateOrUpdate(PositionDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>
        void Remove(PositionDto dto);

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        PositionDto GetById(int id);

        /// <summary>
        /// Update Orders.
        /// </summary>
        /// <param name="values"></param>
        void UpdateOrders(IDictionary<int, int> values);
    }
}