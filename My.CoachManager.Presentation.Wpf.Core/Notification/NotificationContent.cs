namespace My.CoachManager.Presentation.Wpf.Core.Notification
{
    /// <summary>
    /// Provides properties to fill a toast notification.
    /// </summary>
    public class NotificationContent
    {
        /// <summary>
        /// Gets or set title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets mesage.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets type.
        /// </summary>
        public NotificationType Type { get; set; }
    }
}
