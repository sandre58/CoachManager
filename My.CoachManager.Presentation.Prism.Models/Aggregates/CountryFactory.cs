using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Presentation.Prism.Models.Aggregates
{
    /// <summary>
    /// The Country factory.
    /// </summary>
    public static class CountryFactory
    {
        /// <summary>
        /// Convert the model to DTO.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="crudStatus">The crud status.</param>
        /// <returns>The DTO from the model.</returns>
        public static CountryDto Get(CountryModel model, CrudStatus crudStatus)
        {
            return new CountryDto
            {
                CrudStatus = crudStatus,
                Id = model.Id,
                Code = model.Code,
                Label = model.Label,
                Description = model.Description,
                Flag = model.Flag,
                Order = model.Order
            };
        }

        /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>The model.</returns>
        public static CountryModel Get(CountryDto dto)
        {
            if (dto == null) return null;

            var result = new CountryModel
            {
                Id = dto.Id,
                Code = dto.Code,
                Label = dto.Label,
                Description = dto.Description,
                Flag = dto.Flag,
                Order = dto.Order,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                ModifiedBy = dto.ModifiedBy,
                ModifiedDate = dto.ModifiedDate
            };
            result.ResetModified();

            return result;
        }
    }
}