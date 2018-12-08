namespace My.CoachManager.Presentation.Prism.Core.Models
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

    }
}