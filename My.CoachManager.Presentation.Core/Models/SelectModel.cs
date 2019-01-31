using System;

namespace My.CoachManager.Presentation.Core.Models
{
    public class SelectModel : ModelBase, ISelectable
    {
        /// <summary>
        /// Initialize a new instance of <see cref="SelectModel"/>
        /// </summary>
        public SelectModel()
        {
            IsSelectable = true;
        }

        /// <summary>
        /// Gets or sets the selectable value.
        /// </summary>
        public bool IsSelectable { get; set; }

        /// <summary>
        /// Gets or sets the selected Value.
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Calls when selection Changed.
        /// </summary>
        public event EventHandler SelectedChanged;

        /// <summary>
        /// Calls when selection Changed.
        /// </summary>
        protected void OnIsSelectedChanged()
        {
            SelectedChanged?.Invoke(this, new EventArgs());
        }
    }
}