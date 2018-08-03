using System;

namespace My.CoachManager.Presentation.Prism.Controls.Notifications
{
    /// <summary>
    /// Provide methods to display a toast notification.
    /// </summary>
    public interface IToastManager
    {
        /// <summary>
        /// Show a notification.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="areaName"></param>
        /// <param name="expirationTime"></param>
        /// <param name="onClick"></param>
        /// <param name="onClose"></param>
        void Show(object content, string areaName = "", TimeSpan? expirationTime = null, Action onClick = null, Action onClose = null);
    }
}