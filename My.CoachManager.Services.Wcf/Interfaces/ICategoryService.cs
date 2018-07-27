using System.Collections.Generic;
using System.ServiceModel;
using My.CoachManager.Application.Dtos.Category;

namespace My.CoachManager.Services.Wcf.Interfaces
{
    /// <summary>
    /// Administration Service Interface.
    /// </summary>
    [ServiceContract]
    public interface ICategoryService
    {
        /// <summary>
        /// Get categories list.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<CategoryDto> GetCategories();

        /// <summary>
        /// Get a category by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        CategoryDto GetCategoryById(int id);

        /// <summary>
        /// Create a category.
        /// </summary>
        /// <param name="playerDto">the player model.</param>
        /// <returns></returns>
        [OperationContract]
        CategoryDto SaveCategory(CategoryDto playerDto);

        /// <summary>
        /// Remove a category.
        /// </summary>
        /// <param name="playerDto">the player model.</param>
        /// <returns></returns>
        [OperationContract]
        void RemoveCategory(CategoryDto playerDto);

        /// <summary>
        /// Update Categories Orders.
        /// </summary>
        /// <param name="entities"></param>
        [OperationContract]
        void UpdateOrders(IDictionary<int, int> entities);
    }
}