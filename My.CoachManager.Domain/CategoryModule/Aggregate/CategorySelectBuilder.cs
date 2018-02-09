using System;
using System.Linq.Expressions;
using My.CoachManager.Application.Dtos.Administration;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.CategoryModule.Aggregate
{
    public static class CategorySelectBuilder
    {
        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Category, CategoryDto>> SelectCategoryForList()
        {
            return x => new CategoryDto()
            {
                Id = x.Id,
                Label = x.Label,
                Order = x.Order,
                Year = x.Year
            };
        }

        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Category, CategoryDto>> SelectCategoryLabel()
        {
            return x => new CategoryDto()
            {
                Id = x.Id,
                Label = x.Label
            };
        }
    }
}