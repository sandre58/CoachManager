namespace My.CoachManager.Presentation.Prism.Core.Models
{
    /// <summary>
    /// Services to allow changes to an entity to be selectable.
    /// </summary>
    public interface ISelectable
    {
        /// <summary>
        /// Gets or sets the selectable value.
        /// </summary>
        bool IsSelectable { get; set; }

        /// <summary>
        /// Gets or sets the selected Value.
        /// </summary>
        bool IsSelected { get; set; }
    }
}