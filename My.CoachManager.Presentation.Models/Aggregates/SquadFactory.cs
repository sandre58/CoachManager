using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Presentation.Models.Aggregates
{
    /// <summary>
    /// The model factory.
    /// </summary>
    public static class SquadFactory
    {
        /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>The model.</returns>
        public static SquadModel Get(SquadDto dto)
        {
            if (dto == null) return null;

            var result = new SquadModel
            {
                Id = dto.Id,
                Name = dto.Name,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                ModifiedBy = dto.ModifiedBy,
                ModifiedDate = dto.ModifiedDate
            };
            result.ResetModified();

            return result;
        }

        /// <summary>
        /// Convert the model to DTO.
        /// </summary>
        /// <param name="item">The model.</param>
        /// <param name="crudStatus">The crud status.</param>
        /// <returns>The DTO from the model.</returns>
        public static SquadDto Get(SquadModel item, CrudStatus crudStatus)
        {
            if (item == null) return null;

            return new SquadDto
            {
                CrudStatus = crudStatus,
                Id = item.Id,
                Name = item.Name
            };
        }
    }
}