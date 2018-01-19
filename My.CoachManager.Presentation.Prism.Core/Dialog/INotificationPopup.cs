using Prism.Interactivity.InteractionRequest;

namespace My.CoachManager.Presentation.Prism.Core.Dialog
{
    public interface INotificationPopup : INotification
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        MessageDialogStyle Style { get; set; }
    }
}