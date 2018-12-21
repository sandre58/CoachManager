using System.Collections.Generic;
using CommonServiceLocator;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Services.PositionModule;
using My.CoachManager.Services.Wcf.Interfaces;

namespace My.CoachManager.Services.Wcf
{
    /// <summary>
    /// Player Service.
    /// </summary>
    public class PositionService : IPositionService
    {
        /// <inheritdoc />
        /// <summary>
        /// Get categories list.
        /// </summary>
        /// <returns></returns>
        public IList<PositionDto> GetPositions()
        {
            return ServiceLocator.Current.GetInstance<IPositionAppService>().GetPositions();
        }
        
    }
}