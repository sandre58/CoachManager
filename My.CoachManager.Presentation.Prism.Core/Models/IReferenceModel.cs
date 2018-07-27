using System;

namespace My.CoachManager.Presentation.Prism.Core.Models
{
    /// <summary>
    /// Services to allow changes to an entity to be tracked.
    /// </summary>
    public interface IReferenceModel : IEntityModel, IOrderable, IComparable
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