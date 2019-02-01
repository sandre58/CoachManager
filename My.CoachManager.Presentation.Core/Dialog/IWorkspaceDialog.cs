using My.CoachManager.Presentation.Core.Interfaces;
using Prism.Interactivity.InteractionRequest;

namespace My.CoachManager.Presentation.Core.Dialog
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
        new IFrameworkElement Content { get; }
    }
}