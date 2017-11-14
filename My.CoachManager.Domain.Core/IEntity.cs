namespace My.CoachManager.Domain.Core
{
    public interface IEntity : IEntityBase
    {
        /// <summary>
        /// Get or set the base Id.
        /// </summary>
        int Id { get; set; }
    }
}