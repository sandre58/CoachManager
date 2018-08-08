using My.CoachManager.Application.Dtos.Season;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.SeasonModule.Aggregate
{
    /// <summary>
    /// The season factory.
    /// </summary>
    public static class SeasonFactory
    {
        /// <summary>
        /// Create the entity from the DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The entity.</returns>
        public static Season CreateEntity(SeasonDto item)
        {
            if (item == null) return null;

            return new Season
            {
                Id = item.Id,
                Code = item.Code,
                Label = item.Label,
                Description = item.Description,
                Order = item.Order,
                StartDate = item.StartDate,
                EndDate = item.EndDate
            };
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="entity">The entity.</param>
        public static bool UpdateEntity(SeasonDto item, Season entity)
        {
            entity.Code = item.Code;
            entity.Label = item.Label;
            entity.Description = item.Description;
            entity.Order = item.Order;
            entity.StartDate = item.StartDate;
            entity.EndDate = item.EndDate;

            return true;
        }

        /// <summary>
        /// Convert the entity to DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Result of the convert to DTO.</returns>
        public static SeasonDto Get(Season item)
        {
            if (item == null) return null;

            return new SeasonDto
            {
                Id = item.Id,
                Code = item.Code,
                Label = item.Label,
                Description = item.Description,
                Order = item.Order,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                CreatedDate = item.CreatedDate,
                CreatedBy = item.CreatedBy,
                ModifiedDate = item.ModifiedDate,
                ModifiedBy = item.ModifiedBy
            };
        }
    }
}