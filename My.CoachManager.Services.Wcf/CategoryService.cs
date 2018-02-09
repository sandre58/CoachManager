using System.Collections.Generic;
using My.CoachManager.Application.Dtos.Categories;
using My.CoachManager.Application.Services.Categories;
using My.CoachManager.CrossCutting.Unity;
using My.CoachManager.Services.Wcf.Interfaces;

namespace My.CoachManager.Services.Wcf
{
    /// <summary>
    /// Player Service.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        /// <summary>
        /// Get categories list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryDto> GetList()
        {
            return UnityFactory.Resolve<ICategoryAppService>().GetList();
        }

        /// <summary>
        /// Get categories list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryDto> GetLabels()
        {
            return UnityFactory.Resolve<ICategoryAppService>().GetList();
        }

        /// <summary>
        /// Get category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CategoryDto GetById(int id)
        {
            return UnityFactory.Resolve<ICategoryAppService>().GetById(id);
        }

        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public CategoryDto CreateOrUpdate(CategoryDto categoryDto)
        {
            return UnityFactory.Resolve<ICategoryAppService>().CreateOrUpdate(categoryDto);
        }

        /// <summary>
        /// Remove category.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public void Remove(CategoryDto categoryDto)
        {
            UnityFactory.Resolve<ICategoryAppService>().Remove(categoryDto);
        }

        /// <summary>
        /// Update Categories Orders.
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateOrders(IDictionary<int, int> entities)
        {
            UnityFactory.Resolve<ICategoryAppService>().UpdateOrders(entities);
        }
    }
}