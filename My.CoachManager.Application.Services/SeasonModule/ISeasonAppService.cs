using System.Collections.Generic;
using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Application.Services.SeasonModule
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
        IList<SeasonDto> GetSeasons();

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        int SaveSeason(SeasonDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>
        void RemoveSeason(SeasonDto dto);

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        SeasonDto GetSeasonById(int id);

        /// <summary>
        /// Update Orders.
        /// </summary>
        /// <param name="values"></param>
        void UpdateOrders(IDictionary<int, int> values);
    }
}