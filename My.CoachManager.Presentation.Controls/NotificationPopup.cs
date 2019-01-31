// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interactivity;
using System.Windows.Interop;
using My.CoachManager.Presentation.Controls.Behaviours;
using My.CoachManager.Presentation.Controls.Windows;

namespace My.CoachManager.Presentation.Controls
{
    public class NotificationPopup : Window
    {
        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty DelayProperty = DependencyProperty.Register("Delay", typeof(double), typeof(NotificationPopup), new PropertyMetadata((double)4));

        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("Position", typeof(NotificationPosition), typeof(NotificationPopup), new PropertyMetadata(NotificationPosition.TopCenter));

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
        /// Shows the specified window as a notification.
        /// </summary>
        public void ShowPopup()
        {
            BehaviorCollection behaviors = Interaction.GetBehaviors(this);
            behaviors.Add(new FadeBehavior());
            behaviors.Add(new SlideBehavior());

            SetPopupDirection(this, Position);

            var windowInfo = new WindowInfo()
            {
                DisplayDuration = TimeSpan.FromSeconds(Delay),
                Window = this
            };

            Show();

            Observable
                .Timer(TimeSpan.FromSeconds(Delay))
                .ObserveOnDispatcher()
                .Subscribe(x => OnTimerElapsed(windowInfo));
        }

        /// <summary>
        /// Called when the timer has elapsed. Removes any stale notifications.
        /// </summary>
        /// <param name="windowInfo"></param>
        private static void OnTimerElapsed(WindowInfo windowInfo)
        {
            if (windowInfo.Window.IsMouseOver)
            {
                Observable
                    .Timer(windowInfo.DisplayDuration)
                    .ObserveOnDispatcher()
                    .Subscribe(x => OnTimerElapsed(windowInfo));
            }
            else
            {
                var behaviors = Interaction.GetBehaviors(windowInfo.Window);
                var fadeBehavior = behaviors.OfType<FadeBehavior>().First();
                var slideBehavior = behaviors.OfType<SlideBehavior>().First();

                fadeBehavior.FadeOut();
                slideBehavior.SlideOut();

                EventHandler eventHandler = null;
                eventHandler = (sender2, e2) =>
                {
                    fadeBehavior.FadeOutCompleted -= eventHandler;
                    windowInfo.Window.Close();
                };
                fadeBehavior.FadeOutCompleted += eventHandler;
            }
        }

        /// <summary>
        /// Display the notification window in specified direction of the screen
        /// </summary>
        /// <param name="window"> The window object</param>
        /// <param name="notificationFlowDirection"> Direction in which new notifications will appear.</param>
        private void SetPopupDirection(Window window, NotificationPosition notificationFlowDirection)
        {
            if (Owner != null)
            {
                var workingArea = Screen.FromHandle(new WindowInteropHelper(Owner).Handle).WorkingArea;
                var presentationSource = PresentationSource.FromVisual(Owner);
                if (presentationSource != null)
                {
                    if (presentationSource.CompositionTarget != null)
                    {
                        var transform = presentationSource.CompositionTarget.TransformFromDevice;
                        var corner = transform.Transform(new Point(workingArea.Left, workingArea.Top));

                        switch (notificationFlowDirection)
                        {
                            case NotificationPosition.TopCenter:
                                window.Left = corner.X + (Owner.Width - window.Width) / 2 + window.Margin.Left;
                                window.Top = corner.Y + window.Margin.Top;
                                break;

                            case NotificationPosition.BottomCenter:
                                window.Left = corner.X + (Owner.Width - window.Width) / 2 + window.Margin.Left;
                                window.Top = corner.Y + Owner.Height - window.Height - window.Margin.Bottom;
                                break;

                            case NotificationPosition.BottomRight:
                                window.Left = corner.X + Owner.Width - window.Width - window.Margin.Right;
                                window.Top = corner.Y + Owner.Height - window.Height - window.Margin.Bottom;
                                break;

                            case NotificationPosition.BottomLeft:
                                window.Left = corner.X + window.Margin.Left;
                                window.Top = corner.Y + Owner.Height - window.Height - window.Margin.Bottom;
                                break;

                            case NotificationPosition.TopLeft:
                                window.Left = corner.X + (Owner.Width - window.Width) / 2;
                                window.Top = corner.Y + window.Margin.Left;
                                break;

                            case NotificationPosition.TopRight:
                                window.Left = corner.X + Owner.Width - window.Width - window.Margin.Right;
                                window.Top = corner.Y + window.Margin.Top;
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Window metadata.
        /// </summary>
        private sealed class WindowInfo
        {
            /// <summary>
            /// Gets or sets the display duration.
            /// </summary>
            /// <value>
            /// The display duration.
            /// </value>
            public TimeSpan DisplayDuration { get; set; }

            /// <summary>
            /// Gets or sets the window.
            /// </summary>
            /// <value>
            /// The window.
            /// </value>
            public Window Window { get; set; }
        }
    }
}