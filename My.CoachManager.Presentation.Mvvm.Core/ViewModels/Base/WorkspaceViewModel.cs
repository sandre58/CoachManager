using My.CoachManager.Presentation.Mvvm.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Mvvm.Core.ViewModels.Base
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