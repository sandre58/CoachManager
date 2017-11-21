using System;
using System.Windows;
using System.Windows.Interactivity;
using My.CoachManager.Presentation.Prism.Controls.Enums;
using My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest;
using Prism.Interactivity.InteractionRequest;
using NotificationPopup = My.CoachManager.Presentation.Prism.Controls.NotificationPopup;

namespace My.CoachManager.Presentation.Prism.Wpf.Interactivity
{
    /// <summary>
    /// Shows a popup window in response to an <see cref="Core.Interactivity.InteractionRequest"/> being raised.
    /// </summary>
    public class NotificationPopupAction : TriggerAction<FrameworkElement>
    {
        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty DelayProperty = DependencyProperty.Register("Delay", typeof(double), typeof(NotificationPopupAction), new PropertyMetadata((double)4));

        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty PopupStyleProperty = DependencyProperty.Register("PopupStyle", typeof(Style), typeof(NotificationPopupAction));

        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("Position", typeof(NotificationPosition), typeof(NotificationPopupAction), new PropertyMetadata(NotificationPosition.TopCenter));

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public double Delay
        {
            get
            {
                var value = GetValue(DelayProperty);
                if (value != null) return (double)value;
                return 0;
            }
            set { SetValue(DelayProperty, value); }
        }

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public NotificationPosition Position
        {
            get
            {
                var value = GetValue(PositionProperty);
                if (value != null) return (NotificationPosition)value;
                return default(NotificationPosition);
            }
            set { SetValue(PositionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public Style PopupStyle
        {
            get
            {
                return (Style)GetValue(PopupStyleProperty);
            }
            set { SetValue(PopupStyleProperty, value); }
        }

        /// <summary>
        /// Displays the child window and collects results for <see cref="IInteractionRequest"/>.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestedEventArgs;
            if (args == null)
            {
                return;
            }

            var wrapper = GetPopup((INotificationPopup)args.Context);
            wrapper.Delay = Delay;
            wrapper.Position = Position;

            try
            {
                wrapper.Owner = System.Windows.Application.Current.MainWindow;
            }
            catch (Exception)
            {
                // ignored
            }
            if (PopupStyle != null) wrapper.Style = PopupStyle;

            // We invoke the callback when the interaction's window is closed.
            var callback = args.Callback;

            EventHandler handler = (o, e) =>
            {
                wrapper.Content = null;
                if (callback != null) callback();
            };
            wrapper.Closed += handler;

            wrapper.ShowPopup();
        }

        /// <summary>
        /// Returns the window to display as part of the trigger action.
        /// </summary>
        /// <param name="notification">The dialog to be set as a DataContext in the window.</param>
        /// <returns></returns>
        protected virtual NotificationPopup GetPopup(INotificationPopup notification)
        {
            var wrapper = new NotificationPopup();

            if (wrapper == null)
                throw new NullReferenceException("GetPopup cannot return null");

            // If the WindowContent does not have its own DataContext, it will inherit this one.
            wrapper.DataContext = notification;
            wrapper.Title = notification.Title;

            return wrapper;
        }
    }
}