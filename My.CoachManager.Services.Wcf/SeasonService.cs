using System.Collections.Generic;

using CommonServiceLocator;

using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Services.SeasonModule;
using My.CoachManager.Services.Wcf.Interfaces;

namespace My.CoachManager.Services.Wcf
{
    /// <summary>
    /// Player Service.
    /// </summary>
    public class SeasonService : ISeasonService
    {
        /// <inheritdoc />
        /// <summary>
        /// Get categories list.
        /// </summary>
        /// <returns></returns>
        public IList<SeasonDto> GetSeasons()
        {
            return ServiceLocator.Current.GetInstance<ISeasonAppService>().GetSeasons();
        }

        /// <inheritdoc />
        /// <summary>
        /// Get Season.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SeasonDto GetSeasonById(int id)
        {
            return ServiceLocator.Current.GetInstance<ISeasonAppService>().GetSeasonById(id);
        }

        /// <inheritdoc />
        /// <summary>
        /// Create Season.
        /// </summary>
        /// <param name="seasonDto"></param>
        /// <returns></returns>
        public int SaveSeason(SeasonDto seasonDto)
        {
            return ServiceLocator.Current.GetInstance<ISeasonAppService>().SaveSeason(seasonDto);
        }

        /// <inheritdoc />
        /// <summary>
        /// Remove Season.
        /// </summary>
        /// <param name="seasonDto"></param>
        /// <returns></returns>
        public void RemoveSeason(SeasonDto seasonDto)
        {
            ServiceLocator.Current.GetInstance<ISeasonAppService>().RemoveSeason(seasonDto);
        }

        /// <inheritdoc />
        /// <summary>
        /// Update Categories Orders.
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateOrders(IDictionary<int, int> entities)
        {
            ServiceLocator.Current.GetInstance<ISeasonAppService>().UpdateOrders(entities);
        }
    }
}
