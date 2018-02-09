using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.SeasonModule.Service
{
    public interface ISeasonDomainService : IDomainService
    {
        /// <summary>
        /// Check if Season is unique.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool IsUnique(Season item);
    }
}