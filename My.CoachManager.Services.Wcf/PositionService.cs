using System.Collections.Generic;
using My.CoachManager.Application.Dtos.Administration;
using My.CoachManager.Application.Services.Positions;
using My.CoachManager.CrossCutting.Unity;
using My.CoachManager.Services.Wcf.Interfaces;

namespace My.CoachManager.Services.Wcf
{
    /// <summary>
    /// Player Service.
    /// </summary>
    public class PositionService : IPositionService
    {
        /// <summary>
        /// Get Positions list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PositionDto> GetList()
        {
            return UnityFactory.Resolve<IPositionAppService>().GetList();
        }

        /// <summary>
        /// Get Position.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PositionDto GetById(int id)
        {
            return UnityFactory.Resolve<IPositionAppService>().GetById(id);
        }

        /// <summary>
        /// Create Position.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public PositionDto CreateOrUpdate(PositionDto categoryDto)
        {
            return UnityFactory.Resolve<IPositionAppService>().CreateOrUpdate(categoryDto);
        }

        /// <summary>
        /// Remove Position.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public void Remove(PositionDto categoryDto)
        {
            UnityFactory.Resolve<IPositionAppService>().Remove(categoryDto);
        }

        /// <summary>
        /// Update Positions Orders.
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateOrders(IDictionary<int, int> entities)
        {
            UnityFactory.Resolve<IPositionAppService>().UpdateOrders(entities);
        }
    }
}