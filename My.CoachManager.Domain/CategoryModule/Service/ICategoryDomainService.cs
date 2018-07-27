using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.ReferenceModule.Service;

namespace My.CoachManager.Domain.CategoryModule.Service
{
    public interface ICategoryDomainService : IReferenceDomainService<Category>
    {
        /// <summary>
        /// Check if the category can be removed.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        bool CanBeRemoved(Category category);

        /// <summary>
        /// Check if the category is used by others properties.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsUsed(int id);
    }
}