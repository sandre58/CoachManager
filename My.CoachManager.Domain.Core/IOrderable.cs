namespace My.CoachManager.Domain.Core
{
    /// <summary>
    /// Services to allow changes to an entity to be tracked.
    /// </summary>
    public interface IOrderable
    {
        #region Properties

        /// <summary>
        /// Get or Set the order.
        /// </summary>
        int Order { get; set; }

        #endregion Properties
    }
}