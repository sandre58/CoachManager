using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.Admin.Service
{
    public interface ICategoryDomainService : IDomainService
    {
        /// <summary>
        /// Check if category is unique.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        bool IsUnique(Category category);

        /// <summary>
        /// Check if the category can be removed.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        bool CanBeRemoved(Category category);

        /// <summary>
        /// Check if the category is used by others properties.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        bool IsUsed(Category category);
    }
}