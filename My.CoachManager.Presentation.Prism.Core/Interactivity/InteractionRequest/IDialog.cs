using My.CoachManager.Presentation.Prism.Core.ViewModels;
using Prism.Interactivity.InteractionRequest;

namespace My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest
{
    public interface IDialog : INotification
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        DialogResult Result { get; }

        /// <summary>
        /// Gets the context.
        /// </summary>
        IDialogViewModel Context { get; }
    }
}