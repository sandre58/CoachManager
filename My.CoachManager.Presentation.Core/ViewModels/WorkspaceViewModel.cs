using My.CoachManager.Presentation.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Core.ViewModels
{
    public abstract class WorkspaceViewModel : ScreenViewModel, IWorkspaceViewModel
    {
        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the title screen.
        /// </summary>
        public string Title { get; set; }

        #endregion Members
    }
}