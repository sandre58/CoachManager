using System;
using System.Linq.Expressions;
using My.CoachManager.Application.Dtos.Category;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.CategoryModule.Aggregate
{
    public static class CategorySelectBuilder
    {
        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Category, CategoryDto>> SelectCategories()
        {
            return x => new CategoryDto
            {
                Id = x.Id,
                Code = x.Code,
                Label = x.Label,
                Description = x.Description,
                Order = x.Order,
                Year = x.Year
            };
        }
    }
}