namespace My.CoachManager.Domain.Core
{
    /// <summary>
    /// Provides properties for an entity which can be sorted.
    /// </summary>
    public interface IOrderable
    {
        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        int Order { get; set; }
    }
}