using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.InjuryModule.Aggregates
{
    /// <summary>
    /// The Injury factory.
    /// </summary>
    public static class InjuryFactory
    {

        /// <summary>
        /// Create the entity from the DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The entity.</returns>
        public static Injury CreateEntity(InjuryDto item)
        {
            if (item == null) return null;

            return new Injury
            {
                Id = item.Id,
                Type = item.Type,
                Date = item.Date,
                PlayerId = item.PlayerId,
                Description = item.Description,
                Condition = item.Condition,
                ExpectedReturn = item.ExpectedReturn
            };
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="entity">The entity.</param>
        public static bool UpdateEntity(InjuryDto item, Injury entity)
        {
            entity.Id = item.Id;
            entity.Type = item.Type;
            entity.Date = item.Date;
            entity.PlayerId = item.PlayerId;
            entity.Description = item.Description;
            entity.Condition = item.Condition;
            entity.ExpectedReturn = item.ExpectedReturn;

            return true;
        }


        /// <summary>
        /// Convert the entity to DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Result of the convert to DTO.</returns>
        public static InjuryDto Get(Injury item)
        {
            if (item == null) return null;

            return new InjuryDto
            {
                Id = item.Id,
                Type = item.Type,
                Date = item.Date,
                PlayerId = item.PlayerId,
                Description = item.Description,
                Condition = item.Condition,
                ExpectedReturn = item.ExpectedReturn,
                CreatedDate = item.CreatedDate,
                CreatedBy = item.CreatedBy,
                ModifiedDate = item.ModifiedDate,
                ModifiedBy = item.ModifiedBy
            };
        }
    }
}