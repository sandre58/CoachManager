using Prism.Interactivity.InteractionRequest;

namespace My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest
{
    public class NotificationPopup : Notification, INotificationPopup
    {
        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        public MessageDialogStyle Style { get; set; }
    }
}