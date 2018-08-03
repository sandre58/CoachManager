using System.Windows;
using Prism.Interactivity.InteractionRequest;

namespace My.CoachManager.Presentation.Prism.Core.Dialog
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
        new FrameworkElement Content { get; }
    }
}