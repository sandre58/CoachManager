using System.Windows.Input;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class WorkspaceDialogViewModel : DialogViewModel, IWorkspaceDialogViewModel
    {
        #region Initialisation

        /// <inheritdoc />
        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void InitializeShortcuts()
        {
            base.InitializeShortcuts();

            KeyboardShortcuts.Add(new KeyBinding(RefreshCommand, Key.F5, ModifierKeys.None));
        }

        #endregion Initialisation
    }
}