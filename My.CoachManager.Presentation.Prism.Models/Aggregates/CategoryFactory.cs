using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Presentation.Prism.Models.Aggregates
{
    /// <summary>
    /// The category factory.
    /// </summary>
    public static class CategoryFactory
    {
        /// <summary>
        /// Convert the model to DTO.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="crudStatus">The crud status.</param>
        /// <returns>The DTO from the model.</returns>
        public static CategoryDto Get(CategoryModel model, CrudStatus crudStatus)
        {
            return new CategoryDto
            {
                CrudStatus = crudStatus,
                Id = model.Id,
                Code = model.Code,
                Label = model.Label,
                Description = model.Description,
                Year = model.Year,
                Order = model.Order
            };
        }

        /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>The model.</returns>
        public static CategoryModel Get(CategoryDto dto)
        {
            if (dto == null) return null;

            return new CategoryModel
            {
                Id = dto.Id,
                Code = dto.Code,
                Label = dto.Label,
                Description = dto.Description,
                Year = dto.Year,
                Order = dto.Order,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                ModifiedBy = dto.ModifiedBy,
                ModifiedDate = dto.ModifiedDate
            };
        }
    }
}