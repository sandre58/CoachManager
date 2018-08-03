using System.Collections.Generic;
using CommonServiceLocator;
using My.CoachManager.Application.Dtos.Positions;
using My.CoachManager.Application.Services.Positions;
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
            return ServiceLocator.Current.GetInstance<IPositionAppService>().GetList();
        }

        /// <summary>
        /// Get Position.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PositionDto GetById(int id)
        {
            return ServiceLocator.Current.GetInstance<IPositionAppService>().GetById(id);
        }

        /// <summary>
        /// Create Position.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public PositionDto CreateOrUpdate(PositionDto categoryDto)
        {
            return ServiceLocator.Current.GetInstance<IPositionAppService>().CreateOrUpdate(categoryDto);
        }

        /// <summary>
        /// Remove Position.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public void Remove(PositionDto categoryDto)
        {
            ServiceLocator.Current.GetInstance<IPositionAppService>().Remove(categoryDto);
        }

        /// <summary>
        /// Update Positions Orders.
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateOrders(IDictionary<int, int> entities)
        {
            ServiceLocator.Current.GetInstance<IPositionAppService>().UpdateOrders(entities);
        }
    }
}