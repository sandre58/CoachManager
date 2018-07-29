using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using My.CoachManager.Application.Dtos.Category;
using My.CoachManager.Application.Services.CategoryModule;
using My.CoachManager.Services.Wcf.Interfaces;

namespace My.CoachManager.Services.Wcf
{
    /// <summary>
    /// Player Service.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        /// <inheritdoc />
        /// <summary>
        /// Get categories list.
        /// </summary>
        /// <returns></returns>
        public IList<CategoryDto> GetCategories()
        {
            return ServiceLocator.Current.TryResolve<ICategoryAppService>().GetCategories();
        }

        /// <inheritdoc />
        /// <summary>
        /// Get category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CategoryDto GetCategoryById(int id)
        {
            return ServiceLocator.Current.TryResolve<ICategoryAppService>().GetCategoryById(id);
        }

        /// <inheritdoc />
        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public CategoryDto SaveCategory(CategoryDto categoryDto)
        {
            return ServiceLocator.Current.TryResolve<ICategoryAppService>().SaveCategory(categoryDto);
        }

        /// <inheritdoc />
        /// <summary>
        /// Remove category.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public void RemoveCategory(CategoryDto categoryDto)
        {
            ServiceLocator.Current.TryResolve<ICategoryAppService>().RemoveCategory(categoryDto);
        }

        /// <inheritdoc />
        /// <summary>
        /// Update Categories Orders.
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateOrders(IDictionary<int, int> entities)
        {
            ServiceLocator.Current.TryResolve<ICategoryAppService>().UpdateOrders(entities);
        }
    }
}