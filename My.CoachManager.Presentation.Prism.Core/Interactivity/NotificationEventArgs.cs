using System;
using My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest;

namespace My.CoachManager.Presentation.Prism.Core.Interactivity
{
    public class NotificationEventArgs : EventArgs
    {
        #region Members

        /// <summary>
        /// Gets or set the dialog.
        /// </summary>
        public INotificationPopup Notification { get; set; }

        /// <summary>
        /// Gets or sets the callback action.
        /// </summary>
        public Action<INotificationPopup> Callback { get; set; }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="NotificationEventArgs"/>.
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="callback"></param>
        public NotificationEventArgs(INotificationPopup notification, Action<INotificationPopup> callback = null)
        {
            Notification = notification;
            Callback = callback;
        }

        #endregion Constructors
    }
}