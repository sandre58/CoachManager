using System.Windows;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;
using Prism.Interactivity.InteractionRequest;

namespace My.CoachManager.Presentation.Wpf.Core.Dialog
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