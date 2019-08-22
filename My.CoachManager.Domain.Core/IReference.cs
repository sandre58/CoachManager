namespace My.CoachManager.Domain.Core
{
    /// <summary>
    /// Provides properties for an entity containing a label and a code.
    /// </summary>
    public interface IReference : IEntity, IOrderable
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        string Label { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        string Description { get; set; }
    }
}
