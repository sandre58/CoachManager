using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.CategoryModule.Aggregate;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.PersonModule.Aggregate;
using My.CoachManager.Domain.SeasonModule.Aggregate;

namespace My.CoachManager.Domain.RosterModule.Aggregate
{
    /// <summary>
    /// The category factory.
    /// </summary>
    public static class RosterFactory
    {
        /// <summary>
        /// Create the entity from the DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The entity.</returns>
        public static Roster CreateEntity(RosterDto item)
        {
            if (item == null) return null;

            return new Roster
            {
                Id = item.Id,
                CategoryId = item.CategoryId,
                SeasonId = item.SeasonId,
                Name = item.Name
            };
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="entity">The entity.</param>
        public static bool UpdateEntity(RosterDto item, Roster entity)
        {
            entity.Id = item.Id;
            entity.CategoryId = item.CategoryId;
            entity.SeasonId = item.SeasonId;
            entity.Name = item.Name;

            return true;
        }

        /// <summary>
        /// Convert the entity to DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Result of the convert to DTO.</returns>
        public static RosterDto Get(Roster item)
        {
            if (item == null) return null;

            return new RosterDto
            {
                Id = item.Id,
                Name = item.Name,
                SeasonId = item.SeasonId,
                CategoryId = item.CategoryId,
                Category = CategoryFactory.Get(item.Category),
                Season = SeasonFactory.Get(item.Season),
                CreatedDate = item.CreatedDate,
                CreatedBy = item.CreatedBy,
                ModifiedDate = item.ModifiedDate,
                ModifiedBy = item.ModifiedBy
            };
        }

        /// <summary>
        /// Create the entity from the DTO.
        /// </summary>
        /// <returns>The entity.</returns>
        public static RosterPlayer CreatePlayer(int rosterId, int playerId)
        {
            if (rosterId == 0) return null;
            if (playerId == 0) return null;

            return new RosterPlayer
            {
                PlayerId = playerId,
                RosterId = rosterId
            };
        }

        /// <summary>
        /// Convert the entity to DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Result of the convert to DTO.</returns>
        public static RosterPlayerDto GetPlayer(RosterPlayer item)
        {
            if (item == null) return null;

            return new RosterPlayerDto
            {
                Id = item.Id,
                PlayerId = item.PlayerId,
                IsMutation = item.IsMutation,
                LicenseState = item.LicenseState,
                Number = item.Number,
                Player = PlayerFactory.Get(item.Player),
                RosterId = item.RosterId,
                CreatedDate = item.CreatedDate,
                CreatedBy = item.CreatedBy,
                ModifiedDate = item.ModifiedDate,
                ModifiedBy = item.ModifiedBy
            };
        }
    }
}