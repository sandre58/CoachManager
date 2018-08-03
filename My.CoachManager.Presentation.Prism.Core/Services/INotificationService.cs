using System;
using My.CoachManager.Presentation.Prism.Core.Notification;

namespace My.CoachManager.Presentation.Prism.Core.Services
{
    /// <summary>
    /// Interface abstracting the interaction with toast notification.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        void Show(NotificationContent content, TimeSpan? duration = null, Action onClick = null, Action onClose = null);
    }
}