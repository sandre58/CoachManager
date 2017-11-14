using Prism.Interactivity.InteractionRequest;

namespace My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest
{
    public interface INotificationPopup : INotification
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        MessageDialogStyle Style { get; set; }
    }
}