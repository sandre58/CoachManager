using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.Admin.Service
{
    public interface IPositionDomainService : IDomainService
    {
        /// <summary>
        /// Check if Position is unique.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool CheckPositionIsUnique(Position item);
    }
}