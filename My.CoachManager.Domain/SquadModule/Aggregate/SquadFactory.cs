using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.SquadModule.Aggregate
{
    /// <summary>
    /// The category factory.
    /// </summary>
    public static class SquadFactory
    {
        /// <summary>
        /// Create the entity from the DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The entity.</returns>
        public static Squad CreateEntity(SquadDto item)
        {
            if (item == null) return null;

            return new Squad
            {
                Id = item.Id,
                RosterId = item.RosterId,
                Name = item.Name
            };
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="entity">The entity.</param>
        public static bool UpdateEntity(SquadDto item, Squad entity)
        {
            entity.Id = item.Id;
            entity.RosterId = item.RosterId;
            entity.Name = item.Name;

            return true;
        }

        /// <summary>
        /// Convert the entity to DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Result of the convert to DTO.</returns>
        public static SquadDto Get(Squad item)
        {
            if (item == null) return null;

            return new SquadDto
            {
                Id = item.Id,
                Name = item.Name,
                RosterId = item.RosterId,
                CreatedDate = item.CreatedDate,
                CreatedBy = item.CreatedBy,
                ModifiedDate = item.ModifiedDate,
                ModifiedBy = item.ModifiedBy
            };
        }

    }
}