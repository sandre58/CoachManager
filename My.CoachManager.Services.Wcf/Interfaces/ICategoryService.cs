using System;
using System.Collections.Generic;
using System.ServiceModel;
using My.CoachManager.Application.Dtos.Categories;

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
        IEnumerable<CategoryDto> GetList();

        /// <summary>
        /// Get categories list.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<CategoryDto> GetLabels();

        /// <summary>
        /// Get a category by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        CategoryDto GetById(int id);

        /// <summary>
        /// Create a category.
        /// </summary>
        /// <param name="playerDto">the player model.</param>
        /// <returns></returns>
        [OperationContract]
        CategoryDto CreateOrUpdate(CategoryDto playerDto);

        /// <summary>
        /// Remove a category.
        /// </summary>
        /// <param name="playerDto">the player model.</param>
        /// <returns></returns>
        [OperationContract]
        void Remove(CategoryDto playerDto);

        /// <summary>
        /// Update Categories Orders.
        /// </summary>
        /// <param name="entities"></param>
        [OperationContract]
        void UpdateOrders(IDictionary<int, int> entities);
    }
}