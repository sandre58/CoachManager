﻿using System;
using My.CoachManager.Presentation.Wpf.Core.Notification;

namespace My.CoachManager.Presentation.Wpf.Core.Services
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