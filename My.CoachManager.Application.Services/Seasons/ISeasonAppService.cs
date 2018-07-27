using System.Collections.Generic;
using My.CoachManager.Application.Dtos.Seasons;

namespace My.CoachManager.Application.Services.Seasons
{
    /// <summary>
    /// Interface defining the Season application services.
    /// </summary>
    public interface ISeasonAppService
    {
        /// <summary>
        /// Get all dtos list.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SeasonDto> GetList();

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        SeasonDto CreateOrUpdate(SeasonDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>
        void Remove(SeasonDto dto);

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        SeasonDto GetById(int id);

        /// <summary>
        /// Update Orders.
        /// </summary>
        /// <param name="values"></param>
        void UpdateOrders(IDictionary<int, int> values);
    }
}