using System.Collections.Generic;
using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Application.Services.CategoryModule
{
    /// <summary>
    /// Interface defining the category application services.
    /// </summary>
    public interface ICategoryAppService
    {
        /// <summary>
        /// Get all dtos list.
        /// </summary>
        /// <returns></returns>
        IList<CategoryDto> GetCategories();

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        int SaveCategory(CategoryDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>
        void RemoveCategory(CategoryDto dto);

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        CategoryDto GetCategoryById(int id);

        /// <summary>
        /// Update Orders.
        /// </summary>
        /// <param name="values"></param>
        void UpdateOrders(IDictionary<int, int> values);
    }
}