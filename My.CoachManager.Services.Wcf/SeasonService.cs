using System.Collections.Generic;
using My.CoachManager.Application.Dtos.Administration;
using My.CoachManager.Application.Services.Seasons;
using My.CoachManager.CrossCutting.Unity;
using My.CoachManager.Services.Wcf.Interfaces;

namespace My.CoachManager.Services.Wcf
{
    /// <summary>
    /// Player Service.
    /// </summary>
    public class SeasonService : ISeasonService
    {
        /// <summary>
        /// Get Seasons list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SeasonDto> GetList()
        {
            return UnityFactory.Resolve<ISeasonAppService>().GetList();
        }

        /// <summary>
        /// Get Season.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SeasonDto GetById(int id)
        {
            return UnityFactory.Resolve<ISeasonAppService>().GetById(id);
        }

        /// <summary>
        /// Create Season.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public SeasonDto CreateOrUpdate(SeasonDto categoryDto)
        {
            return UnityFactory.Resolve<ISeasonAppService>().CreateOrUpdate(categoryDto);
        }

        /// <summary>
        /// Remove Season.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public void Remove(SeasonDto categoryDto)
        {
            UnityFactory.Resolve<ISeasonAppService>().Remove(categoryDto);
        }

        /// <summary>
        /// Update Seasons Orders.
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateOrders(IDictionary<int, int> entities)
        {
            UnityFactory.Resolve<ISeasonAppService>().UpdateOrders(entities);
        }
    }
}