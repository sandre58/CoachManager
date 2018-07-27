using My.CoachManager.Application.Dtos.Category;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.CategoryModule.Aggregate
{

    /// <summary>
    /// The category factory.
    /// </summary>
    public static class CategoryFactory
    {
        /// <summary>
        /// Create the entity from the DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The entity.</returns>
        public static Category CreateEntity(CategoryDto item)
        {
            return new Category
            {
                Code = item.Code,
                Label = item.Label,
                Description = item.Description,
                Order = item.Order,
                Year = item.Year,
            };
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="entity">The entity.</param>
        public static bool UpdateEntity(CategoryDto item, Category entity)
        {
            entity.Code = item.Code;
            entity.Label = item.Label;
            entity.Description = item.Description;
            entity.Order = item.Order;
            entity.Year = item.Year;

            return true;
        }

        /// <summary>
        /// Convert the entity to DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Result of the convert to DTO.</returns>
        public static CategoryDto Get(Category item)
        {
            return new CategoryDto
            {
                Code = item.Code,
                Label = item.Label,
                Description = item.Description,
                Order = item.Order,
                Year = item.Year,
            };
        }
    }
}
