namespace My.CoachManager.Presentation.Prism.Core.Models
{
    /// <summary>
    /// Services to allow changes to an entity to be tracked.
    /// </summary>
    public interface IOrderable
    {
        /// <summary>
        /// Get or Set the order.
        /// </summary>
        int Order { get; set; }
    }
}