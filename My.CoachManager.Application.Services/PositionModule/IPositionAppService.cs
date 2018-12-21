using System.Collections.Generic;
using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Application.Services.PositionModule
{
    /// <summary>
    /// Interface defining the position application services.
    /// </summary>
    public interface IPositionAppService
    {
        /// <summary>
        /// Get all dtos list.
        /// </summary>
        /// <returns></returns>
        IList<PositionDto> GetPositions();
    }
}