using My.CoachManager.Presentation.Mvvm.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Mvvm.Core.Dialog
{
    public interface IWorkspaceDialog : INotification
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        DialogResult Result { get; }

        /// <summary>
        /// Gets content.
        /// </summary>
        new IDialogViewModel Content { get; }
    }
}