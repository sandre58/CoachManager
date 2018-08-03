using System;
using My.CoachManager.Presentation.Prism.Core.Notification;
using My.CoachManager.Presentation.Prism.Core.Resources;
using My.CoachManager.Presentation.Prism.Core.Services;
using Microsoft.Practices.ServiceLocation;

namespace My.CoachManager.Presentation.Prism.Core.Manager
{
    /// <summary>
    /// Provides methods and properties to display toast notification.
    /// </summary>
    public static class NotificationManager
    {
        #region Fields

        private static INotificationService _notificationService;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets Notification Service.
        /// </summary>
        private static INotificationService NotificationService => _notificationService ??
                                                              (_notificationService = ServiceLocator.Current.GetInstance<INotificationService>());

        #endregion Members

        #region Methods

        /// <summary>
        /// Show a toast notification.
        /// </summary>
        public static void ShowError(string message, TimeSpan? duration = null, Action onClick = null, Action onClose = null)
        {
            Show(new NotificationContent
            {
                Title = DialogResources.Error,
                Message = message,
                Type = NotificationType.Error,
            }, duration, onClick, onClose);
        }

        /// <summary>
        /// Show a toast notification.
        /// </summary>
        public static void ShowInformation(string message, TimeSpan? duration = null, Action onClick = null, Action onClose = null)
        {
            Show(new NotificationContent
            {
                Title = DialogResources.Information,
                Message = message,
                Type = NotificationType.Information,
            }, duration, onClick, onClose);
        }

        /// <summary>
        /// Show a toast notification.
        /// </summary>
        public static void ShowWarning(string message, TimeSpan? duration = null, Action onClick = null, Action onClose = null)
        {
            Show(new NotificationContent
            {
                Title = DialogResources.Warning,
                Message = message,
                Type = NotificationType.Warning,
            }, duration, onClick, onClose);
        }

        /// <summary>
        /// Show a toast notification.
        /// </summary>
        public static void ShowSuccess(string message, TimeSpan? duration = null, Action onClick = null, Action onClose = null)
        {
            Show(new NotificationContent
            {
                Title = DialogResources.Success,
                Message = message,
                Type = NotificationType.Success,
            }, duration, onClick, onClose);
        }

        /// <summary>
        /// Show a toast notification.
        /// </summary>
        public static void Show(NotificationContent content, TimeSpan? duration = null, Action onClick = null, Action onClose = null)
        {
            NotificationService.Show(content, duration, onClick, onClose);
        }

        #endregion Methods
    }
}