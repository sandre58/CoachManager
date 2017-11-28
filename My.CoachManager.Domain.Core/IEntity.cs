namespace My.CoachManager.Domain.Core
{
    /// <summary>
    /// Provides properties for an entity.
    /// </summary>
    public interface IEntity : IEntityBase
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the Business identifier.
        /// </summary>
        string BusinessKey { get; }
    }
}