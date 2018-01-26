using System;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Entities
{
    /// <summary>
    /// Services to allow changes to an entity to be tracked.
    /// </summary>
    public interface ILabelableViewModel : IEntityViewModel, IComparable
    {
        #region Properties

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        string Label { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        string Code { get; set; }

        #endregion Properties
    }
}