using System;

namespace My.CoachManager.Presentation.Core.Models
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

        /// <summary>
        /// Calls when selection Changed.
        /// </summary>
        event EventHandler SelectedChanged;
    }
}