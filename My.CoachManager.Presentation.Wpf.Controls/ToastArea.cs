using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using My.CoachManager.Presentation.Wpf.Controls.Notifications;

namespace My.CoachManager.Presentation.Wpf.Controls
{
    public class ToastArea : Control
    {
        
        public ToastPosition Position
        {
            get => (ToastPosition)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(ToastPosition), typeof(ToastArea), new PropertyMetadata(ToastPosition.BottomRight));


        public int MaxItems
        {
            get => (int)GetValue(MaxItemsProperty);
            set => SetValue(MaxItemsProperty, value);
        }
        
        public static readonly DependencyProperty MaxItemsProperty =
            DependencyProperty.Register("MaxItems", typeof(int), typeof(ToastArea), new PropertyMetadata(int.MaxValue));

        private IList _items;

        public ToastArea()
        {
            ToastManager.AddArea(this);
        }

        static ToastArea()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToastArea),
                new FrameworkPropertyMetadata(typeof(ToastArea)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var itemsControl = GetTemplateChild("PART_Items") as Panel;
            _items = itemsControl?.Children;
        }


        public async void Show(object content, TimeSpan expirationTime, Action onClick, Action onClose)
        {
            var notification = new Toast
            {
                Content = content
            };
            
            notification.MouseLeftButtonDown += (sender, args) =>
            {
                if (onClick != null)
                {
                    onClick.Invoke();
                    (sender as Toast)?.Close();
                }
            };
            notification.NotificationClosed += (sender, args) => onClose?.Invoke();
            notification.NotificationClosed += OnNotificationClosed;

            if (!IsLoaded)
            {
                return;
            }

            var w = Window.GetWindow(this);
            if (w != null)
            {
                var x = PresentationSource.FromVisual(w);
                if (x == null)
                {
                    return;
                }
            }

            lock (_items)
            {
                _items.Add(notification);

                if (_items.OfType<Toast>().Count(i => !i.IsClosing) > MaxItems)
                {
                    _items.OfType<Toast>().First(i => !i.IsClosing).Close();
                }
            }

            if (expirationTime == TimeSpan.MaxValue)
            {
                return;
            }
            await Task.Delay(expirationTime);
                notification.Close();
        }

        private void OnNotificationClosed(object sender, RoutedEventArgs routedEventArgs)
        {
            var notification = sender as Toast;
            _items.Remove(notification);
        }
    }

}