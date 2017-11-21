using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Interactivity;
using My.CoachManager.Presentation.Prism.Core.Services;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class WorkspaceDialogViewModel : DialogViewModel, IWorkspaceDialogViewModel
    {
        #region Fields

        private string _title;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets the title screen.
        /// </summary>
        public virtual string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ScreenViewModel"/>.
        /// </summary>
        public WorkspaceDialogViewModel(IDialogService dialogService, ILogger logger)
            : base(dialogService, logger)
        {
            DialogResult = DialogResult.None;
        }

        #endregion Constructors
    }
}