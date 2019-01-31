using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace My.CoachManager.Presentation.Controls
{
    /// <summary>
    /// The MetroThumbContentControl control can be used for titles or something else and enables basic drag movement functionality.
    /// </summary>
    public class ThumbContentControl : ContentControl, IThumb
    {
        private TouchDevice _currentDevice;
        private Point _startDragPoint;
        private Point _startDragScreenPoint;
        private Point _oldDragScreenPoint;

        static ThumbContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ThumbContentControl), new FrameworkPropertyMetadata(typeof(ThumbContentControl)));
            FocusableProperty.OverrideMetadata(typeof(ThumbContentControl), new FrameworkPropertyMetadata(default(bool)));
            EventManager.RegisterClassHandler(typeof(ThumbContentControl), Mouse.LostMouseCaptureEvent, new MouseEventHandler(OnLostMouseCapture));
        }

        public static readonly RoutedEvent DragStartedEvent
            = EventManager.RegisterRoutedEvent("DragStarted",
                                               RoutingStrategy.Bubble,
                                               typeof(DragStartedEventHandler),
                                               typeof(ThumbContentControl));

        public static readonly RoutedEvent DragDeltaEvent
            = EventManager.RegisterRoutedEvent("DragDelta",
                                               RoutingStrategy.Bubble,
                                               typeof(DragDeltaEventHandler),
                                               typeof(ThumbContentControl));

        public static readonly RoutedEvent DragCompletedEvent
            = EventManager.RegisterRoutedEvent("DragCompleted",
                                               RoutingStrategy.Bubble,
                                               typeof(DragCompletedEventHandler),
                                               typeof(ThumbContentControl));

        /// <summary>
        /// Adds or remove a DragStartedEvent handler
        /// </summary>
        public event DragStartedEventHandler DragStarted
        {
            add { AddHandler(DragStartedEvent, value); }
            remove { RemoveHandler(DragStartedEvent, value); }
        }

        /// <summary>
        /// Adds or remove a DragDeltaEvent handler
        /// </summary>
        public event DragDeltaEventHandler DragDelta
        {
            add { AddHandler(DragDeltaEvent, value); }
            remove { RemoveHandler(DragDeltaEvent, value); }
        }

        /// <summary>
        /// Adds or remove a DragCompletedEvent handler
        /// </summary>
        public event DragCompletedEventHandler DragCompleted
        {
            add { AddHandler(DragCompletedEvent, value); }
            remove { RemoveHandler(DragCompletedEvent, value); }
        }

        public static readonly DependencyPropertyKey IsDraggingPropertyKey
            = DependencyProperty.RegisterReadOnly("IsDragging",
                                                  typeof(bool),
                                                  typeof(ThumbContentControl),
                                                  new FrameworkPropertyMetadata(default(bool)));

        /// <summary>
        /// DependencyProperty for the IsDragging property.
        /// </summary>
        public static readonly DependencyProperty IsDraggingProperty = IsDraggingPropertyKey.DependencyProperty;

        /// <summary>
        /// Indicates that the left mouse button is pressed and is over the MetroThumbContentControl.
        /// </summary>
        public bool IsDragging
        {
            get { return (bool)GetValue(IsDraggingProperty); }
            protected set { SetValue(IsDraggingPropertyKey, value); }
        }

        public void CancelDragAction()
        {
            if (!IsDragging)
            {
                return;
            }
            if (IsMouseCaptured)
            {
                ReleaseMouseCapture();
            }
            ClearValue(IsDraggingPropertyKey);
            var horizontalChange = _oldDragScreenPoint.X - _startDragScreenPoint.X;
            var verticalChange = _oldDragScreenPoint.Y - _startDragScreenPoint.Y;
            RaiseEvent(new MetroThumbContentControlDragCompletedEventArgs(horizontalChange, verticalChange, true));
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (!IsDragging)
            {
                e.Handled = true;
                // focus me
                Focus();
                // now capture the mouse for the drag action
                CaptureMouse();
                // so now we are in dragging mode
                SetValue(IsDraggingPropertyKey, true);
                // get the mouse points
                _startDragPoint = e.GetPosition(this);
                _oldDragScreenPoint = _startDragScreenPoint = PointToScreen(_startDragPoint);
                try
                {
                    RaiseEvent(new MetroThumbContentControlDragStartedEventArgs(_startDragPoint.X, _startDragPoint.Y));
                }
                catch
                {
                    CancelDragAction();
                }
            }

            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (IsMouseCaptured && IsDragging)
            {
                e.Handled = true;
                // now we are in normal mode
                ClearValue(IsDraggingPropertyKey);
                // release the captured mouse
                ReleaseMouseCapture();
                // get the current mouse position and call the completed event with the horizontal/vertical change
                Point currentMouseScreenPoint = PointToScreen(e.MouseDevice.GetPosition(this));
                var horizontalChange = currentMouseScreenPoint.X - _startDragScreenPoint.X;
                var verticalChange = currentMouseScreenPoint.Y - _startDragScreenPoint.Y;
                RaiseEvent(new MetroThumbContentControlDragCompletedEventArgs(horizontalChange, verticalChange, false));
            }

            base.OnMouseLeftButtonUp(e);
        }

        private static void OnLostMouseCapture(object sender, MouseEventArgs e)
        {
            // Cancel the drag action if we lost capture
            ThumbContentControl thumb = (ThumbContentControl)sender;
            if (!Equals(Mouse.Captured, thumb))
            {
                thumb.CancelDragAction();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!IsDragging)
            {
                return;
            }
            if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            {
                Point currentDragPoint = e.GetPosition(this);
                // Get client point and convert it to screen point
                Point currentDragScreenPoint = PointToScreen(currentDragPoint);
                if (currentDragScreenPoint != _oldDragScreenPoint)
                {
                    _oldDragScreenPoint = currentDragScreenPoint;
                    e.Handled = true;
                    var horizontalChange = currentDragPoint.X - _startDragPoint.X;
                    var verticalChange = currentDragPoint.Y - _startDragPoint.Y;
                    RaiseEvent(new DragDeltaEventArgs(horizontalChange, verticalChange) { RoutedEvent = DragDeltaEvent });
                }
            }
            else
            {
                // clear some saved stuff
                if (Equals(e.MouseDevice.Captured, this))
                {
                    ReleaseMouseCapture();
                }
                ClearValue(IsDraggingPropertyKey);
                _startDragPoint.X = 0;
                _startDragPoint.Y = 0;
            }
        }

        protected override void OnPreviewTouchDown(TouchEventArgs e)
        {
            // Release any previous capture
            ReleaseCurrentDevice();
            // Capture the new touch
            CaptureCurrentDevice(e);
        }

        protected override void OnPreviewTouchUp(TouchEventArgs e)
        {
            ReleaseCurrentDevice();
        }

        protected override void OnLostTouchCapture(TouchEventArgs e)
        {
            // Only re-capture if the reference is not null
            // This way we avoid re-capturing after calling ReleaseCurrentDevice()
            if (_currentDevice != null)
            {
                CaptureCurrentDevice(e);
            }
        }

        private void ReleaseCurrentDevice()
        {
            if (_currentDevice != null)
            {
                // Set the reference to null so that we don't re-capture in the OnLostTouchCapture() method
                var temp = _currentDevice;
                _currentDevice = null;
                ReleaseTouchCapture(temp);
            }
        }

        private void CaptureCurrentDevice(TouchEventArgs e)
        {
            bool gotTouch = CaptureTouch(e.TouchDevice);
            if (gotTouch)
            {
                _currentDevice = e.TouchDevice;
            }
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new MetroThumbContentControlAutomationPeer(this);
        }
    }

    public class MetroThumbContentControlDragStartedEventArgs : DragStartedEventArgs
    {
        public MetroThumbContentControlDragStartedEventArgs(double horizontalOffset, double verticalOffset)
            : base(horizontalOffset, verticalOffset)
        {
            RoutedEvent = ThumbContentControl.DragStartedEvent;
        }
    }

    public class MetroThumbContentControlDragCompletedEventArgs : DragCompletedEventArgs
    {
        public MetroThumbContentControlDragCompletedEventArgs(double horizontalOffset, double verticalOffset, bool canceled)
            : base(horizontalOffset, verticalOffset, canceled)
        {
            RoutedEvent = ThumbContentControl.DragCompletedEvent;
        }
    }

    public class MetroThumbContentControlAutomationPeer : FrameworkElementAutomationPeer
    {
        public MetroThumbContentControlAutomationPeer(FrameworkElement owner) : base(owner)
        { }

        protected override string GetClassNameCore()
        {
            return "MetroThumbContentControl";
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Custom;
        }
    }
}