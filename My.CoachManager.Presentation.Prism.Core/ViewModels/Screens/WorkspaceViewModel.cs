namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class WorkspaceViewModel : ScreenViewModel, IWorkspaceViewModel
    {
        #region Fields

        private string _title;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets the title screen.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #endregion Members
    }
}