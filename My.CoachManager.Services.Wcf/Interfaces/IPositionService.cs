using System.Collections.Generic;
using System.ServiceModel;
using My.CoachManager.Application.Dtos;

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
        IList<PositionDto> GetPositions();

    }
}