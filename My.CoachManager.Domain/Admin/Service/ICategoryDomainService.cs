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
        bool CheckCategoryIsUnique(Category category);
    }
}