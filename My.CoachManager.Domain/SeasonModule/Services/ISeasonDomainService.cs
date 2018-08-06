using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.ReferenceModule.Services;

namespace My.CoachManager.Domain.SeasonModule.Services
{
    public interface ISeasonDomainService : IReferenceDomainService<Season>
    {
        /// <summary>
        /// Check if the item can be removed.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool CanBeRemoved(Season item);

        /// <summary>
        /// Check if the item is used by others properties.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsUsed(int id);
    }
}