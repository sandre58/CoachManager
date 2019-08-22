using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using My.CoachManager.Presentation.Wpf.Controls.Helpers;

namespace My.CoachManager.Presentation.Wpf.Controls
{
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
    public class Toast : ContentControl
    {
        private TimeSpan _closingAnimationTime = TimeSpan.Zero;

        public bool IsClosing { get; set; }

        public static readonly RoutedEvent NotificationCloseInvokedEvent = EventManager.RegisterRoutedEvent(
            "NotificationCloseInvoked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Toast));

        public static readonly RoutedEvent NotificationClosedEvent = EventManager.RegisterRoutedEvent(
            "NotificationClosed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Toast));

        static Toast()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Toast),
                new FrameworkPropertyMetadata(typeof(Toast)));
        }

        public event RoutedEventHandler NotificationCloseInvoked
        {
            add => AddHandler(NotificationCloseInvokedEvent, value);
            remove => RemoveHandler(NotificationCloseInvokedEvent, value);
        }

        public event RoutedEventHandler NotificationClosed
        {
            add => AddHandler(NotificationClosedEvent, value);
            remove => RemoveHandler(NotificationClosedEvent, value);
        }

        public static bool GetCloseOnClick(DependencyObject obj)
        {
            return (bool)obj.GetValue(CloseOnClickProperty);
        }

        public static void SetCloseOnClick(DependencyObject obj, bool value)
        {
            obj.SetValue(CloseOnClickProperty, value);
        }

        public static readonly DependencyProperty CloseOnClickProperty =
            DependencyProperty.RegisterAttached("CloseOnClick", typeof(bool), typeof(Toast), new FrameworkPropertyMetadata(false,CloseOnClickChanged));

        private static void CloseOnClickChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (!(dependencyObject is Button button))
            {
                return;
            }

            var value = (bool)dependencyPropertyChangedEventArgs.NewValue;

            if (value)
            {
                button.Click += (sender, args) =>
                {
                    var notification = button.FindVisualParent<Toast>();
                    notification?.Close();
                };
            }
        }      

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (GetTemplateChild("PART_CloseButton") is Button closeButton)
                closeButton.Click += OnCloseButtonOnClick;
            
            var storyboards = Template.Triggers.OfType<EventTrigger>().FirstOrDefault(t => t.RoutedEvent == NotificationCloseInvokedEvent)?.Actions.OfType<BeginStoryboard>().Select(a => a.Storyboard);
            _closingAnimationTime = new TimeSpan(storyboards?.Max(s => Math.Min((s.Duration.HasTimeSpan ? s.Duration.TimeSpan + (s.BeginTime ?? TimeSpan.Zero) : TimeSpan.MaxValue).Ticks, s.Children.Select(ch => ch.Duration.TimeSpan + (s.BeginTime ?? TimeSpan.Zero)).Max().Ticks)) ?? 0);
            
        }

        private void OnCloseButtonOnClick(object sender, RoutedEventArgs args)
        {
            if (!(sender is Button button)) return;
            
            button.Click -= OnCloseButtonOnClick;
            Close();
        }
        
        public async void Close()
        {
            if (IsClosing)
            {
                return;
            }
        
            IsClosing = true;
            
            RaiseEvent(new RoutedEventArgs(NotificationCloseInvokedEvent));
            await Task.Delay(_closingAnimationTime);
            RaiseEvent(new RoutedEventArgs(NotificationClosedEvent));
        } 
    }       
}
