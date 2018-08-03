using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.SeasonModule.Services
{
    public interface ISeasonDomainService
    {
        /// <summary>
        /// Check if Season is unique.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool IsUnique(Season item);
    }
}