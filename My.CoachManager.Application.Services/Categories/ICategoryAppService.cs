﻿using System.Collections.Generic;
using My.CoachManager.Application.Core;
using My.CoachManager.Application.Dtos.Categories;

namespace My.CoachManager.Application.Services.Categories
{
    /// <summary>
    /// Interface defining the category application services.
    /// </summary>
    public interface ICategoryAppService : IAppService
    {
        /// <summary>
        /// Get all dtos list.
        /// </summary>
        /// <returns></returns>
        IEnumerable<CategoryDto> GetList();

        /// <summary>
        /// Load all labels items.
        /// </summary>
        /// <returns></returns>
        IEnumerable<CategoryDto> GetLabels();

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        CategoryDto CreateOrUpdate(CategoryDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>
        void Remove(CategoryDto dto);

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        CategoryDto GetById(int id);

        /// <summary>
        /// Update Orders.
        /// </summary>
        /// <param name="values"></param>
        void UpdateOrders(IDictionary<int, int> values);
    }
}