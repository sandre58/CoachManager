namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class WorkspaceViewModel : ScreenViewModel, IWorkspaceViewModel
    {

        #region Members

        /// <summary>
        /// Gets or sets the title screen.
        /// </summary>
        public string Title { get; set; }

        #endregion Members
    }
}