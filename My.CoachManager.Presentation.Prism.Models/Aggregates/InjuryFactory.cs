using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Presentation.Prism.Models.Aggregates
{
    /// <summary>
    /// The category factory.
    /// </summary>
    public static class InjuryFactory
    {
        /// <summary>
        /// Convert the model to DTO.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="crudStatus">The crud status.</param>
        /// <returns>The DTO from the model.</returns>
        public static InjuryDto Get(InjuryModel model, CrudStatus crudStatus)
        {
            return new InjuryDto
            {
                CrudStatus = crudStatus,
                Id = model.Id,
                Description = model.Description,
                Date = model.Date,
                Type = model.Type,
                Condition = model.Condition,
                ExpectedReturn = model.ExpectedReturn,
                Severity = model.Severity
            };
        }

        /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>The model.</returns>
        public static InjuryModel Get(InjuryDto dto)
        {
            if (dto == null) return null;

            var result = new InjuryModel
            {
                Id = dto.Id,
                Description = dto.Description,
                Date = dto.Date,
                Type = dto.Type,
                Condition = dto.Condition,
                ExpectedReturn = dto.ExpectedReturn,
                Severity = dto.Severity,
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