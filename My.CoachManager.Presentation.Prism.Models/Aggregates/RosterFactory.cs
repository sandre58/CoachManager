using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Presentation.Prism.Models.Aggregates
{
    /// <summary>
    /// The model factory.
    /// </summary>
    public static class RosterFactory
    {
        /// <summary>
        /// Convert the model to DTO.
        /// </summary>
        /// <param name="item">The model.</param>
        /// <param name="crudStatus">The crud status.</param>
        /// <returns>The DTO from the model.</returns>
        public static RosterDto Get(RosterModel item, CrudStatus crudStatus)
        {
            if (item == null) return null;

            return new RosterDto
            {
                CrudStatus = crudStatus,
                Id = item.Id,
                Name = item.Name,
                SeasonId = item.SeasonId,
                CategoryId = item.CategoryId
            };
        }

        /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>The model.</returns>
        public static RosterModel Get(RosterDto dto)
        {
            if (dto == null) return null;

            var result = new RosterModel
            {
                Id = dto.Id,
                Name = dto.Name,
                SeasonId = dto.SeasonId,
                CategoryId = dto.CategoryId,
                Category = CategoryFactory.Get(dto.Category),
                Season = SeasonFactory.Get(dto.Season),
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