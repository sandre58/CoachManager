using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Wpf.Core.ViewModels.Base
{
    public abstract class WorkspaceViewModel : DataViewModel, IWorkspaceViewModel
    {
        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the title screen.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets show header.
        /// </summary>
        public bool ShowHeader { get; set; } = true;

        #endregion Members
    }
}