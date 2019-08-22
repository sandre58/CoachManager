using My.CoachManager.Presentation.Mvvm.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Mvvm.Core.ViewModels.Base
{
    public abstract class WorkspaceDialogViewModel : DialogViewModel, IWorkspaceDialogViewModel
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets if we can refresh after initialization.
        /// </summary>
        public override bool RefreshOnInit => false;
    }
}