using My.CoachManager.Presentation.Prism.Core.Dialog;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class WorkspaceDialogViewModel : DialogViewModel, IWorkspaceDialogViewModel
    {
        #region Fields

        private string _title;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ScreenViewModel"/>.
        /// </summary>
        public WorkspaceDialogViewModel()
        {
            DialogResult = DialogResult.None;
        }

        #endregion Constructors

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