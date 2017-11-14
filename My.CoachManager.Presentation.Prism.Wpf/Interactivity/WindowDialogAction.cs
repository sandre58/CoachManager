using System.Windows;
using My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest;
using Prism.Interactivity;
using Prism.Interactivity.InteractionRequest;
using MessageDialog = My.CoachManager.Presentation.Prism.Wpf.Views.MessageDialog;

namespace My.CoachManager.Presentation.Prism.Wpf.Interactivity
{
    /// <summary>
    /// Shows a popup window in response to an <see cref="My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest"/> being raised.
    /// </summary>
    public class WindowDialogAction : PopupWindowAction
    {
        /// <summary>
        /// Returns the window to display as part of the trigger action.
        /// </summary>
        /// <param name="notification">The notification to be set as a DataContext in the window.</param>
        /// <returns></returns>
        protected override Window GetWindow(INotification notification)
        {
            Window wrapperWindow = null;

            if (notification is IMessageDialog)
            {
                wrapperWindow = new MessageDialog();
            }

            if (wrapperWindow != null)
            {
                wrapperWindow.DataContext = notification;
                PrepareContentForWindow(notification, wrapperWindow);

                if (AssociatedObject != null)
                    wrapperWindow.Owner = Window.GetWindow(AssociatedObject);

                // If the user provided a Style for a Window we set it as the window's style.
                if (WindowStyle != null)
                    wrapperWindow.Style = WindowStyle;

                // If the user has provided a startup location for a Window we set it as the window's startup location.
                if (WindowStartupLocation.HasValue)
                    wrapperWindow.WindowStartupLocation = WindowStartupLocation.Value;

                return wrapperWindow;
            }

            return null;
        }
    }
}