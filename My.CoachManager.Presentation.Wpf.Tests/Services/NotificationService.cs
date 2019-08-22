using System;
using My.CoachManager.Presentation.Wpf.Controls.Notifications;
using My.CoachManager.Presentation.Wpf.Core.Notification;
using My.CoachManager.Presentation.Wpf.Core.Services;

namespace My.CoachManager.Presentation.Wpf.Tests.Services
{
    /// <summary>
    /// Provides methods to display toast notifications.
    /// </summary>
    public class NotificationService : INotificationService
    {
        #region Fields
        
        private readonly ToastManager _toastManager;

        #endregion

        #region  Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="NotificationService"/>.
        /// </summary>
        public NotificationService()
        {
            _toastManager = new ToastManager();
        }

        #endregion

        #region INotificationService

        /// <inheritdoc />
        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="T:System.Boolean" /> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void Show(NotificationContent content, TimeSpan? duration = null, Action onClick = null, Action onClose = null)
        {
            _toastManager.Show(content, "", duration, onClick, onClose);
        }

        #endregion

    }
}