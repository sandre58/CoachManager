using System.Collections.Generic;
using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Application.Services.CategoryModule
{
    public interface ICategoryAppService
    {
        #region Methods

        /// <summary>
        /// Save a dto.
        /// </summary>
        /// <returns></returns>
        int SaveCategory(CategoryDto dto);

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        void RemoveCategory(int id);
        
        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        CategoryDto GetCategoryById(int id);
        
        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        IList<CategoryDto> GetCategories();
        
        /// <summary>
        /// Update items Orders.
        /// </summary>
        /// <param name="values"></param>
        void UpdateOrders(IDictionary<int, int> values);

        #endregion Methods
    }
}
