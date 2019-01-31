using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace My.CoachManager.Presentation.Controls
{
    public enum BadgePlacementMode
    {
        TopLeft,
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left
    }

    [TemplatePart(Name = BadgeContainerPartName, Type = typeof(UIElement))]
    public class BadgedBase : ContentControl
    {
        public const string BadgeContainerPartName = "PART_BadgeContainer";
        protected FrameworkElement BadgeContainer;

        public static readonly DependencyProperty BadgeProperty = DependencyProperty.Register(
            "Badge", typeof(object), typeof(BadgedBase), new FrameworkPropertyMetadata(default(object), FrameworkPropertyMetadataOptions.AffectsArrange, OnBadgeChanged));

        public object Badge
        {
            get { return GetValue(BadgeProperty); }
            set { SetValue(BadgeProperty, value); }
        }

        public static readonly DependencyProperty BadgeBackgroundProperty = DependencyProperty.Register(
            "BadgeBackground", typeof(Brush), typeof(BadgedBase), new PropertyMetadata(default(Brush)));

        public Brush BadgeBackground
        {
            get { return (Brush)GetValue(BadgeBackgroundProperty); }
            set { SetValue(BadgeBackgroundProperty, value); }
        }

        public static readonly DependencyProperty BadgeForegroundProperty = DependencyProperty.Register(
            "BadgeForeground", typeof(Brush), typeof(BadgedBase), new PropertyMetadata(default(Brush)));

        public Brush BadgeForeground
        {
            get { return (Brush)GetValue(BadgeForegroundProperty); }
            set { SetValue(BadgeForegroundProperty, value); }
        }

        public static readonly DependencyProperty BadgePlacementModeProperty = DependencyProperty.Register(
            "BadgePlacementMode", typeof(BadgePlacementMode), typeof(BadgedBase), new PropertyMetadata(default(BadgePlacementMode)));

        public BadgePlacementMode BadgePlacementMode
        {
            get { return (BadgePlacementMode)GetValue(BadgePlacementModeProperty); }
            set { SetValue(BadgePlacementModeProperty, value); }
        }

        public static readonly RoutedEvent BadgeChangedEvent =
            EventManager.RegisterRoutedEvent(
                "BadgeChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<object>),
                typeof(BadgedBase));

        public event RoutedPropertyChangedEventHandler<object> BadgeChanged
        {
            add { AddHandler(BadgeChangedEvent, value); }
            remove { RemoveHandler(BadgeChangedEvent, value); }
        }

        private static readonly DependencyPropertyKey IsBadgeSetPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsBadgeSet", typeof(bool), typeof(BadgedBase),
                new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty IsBadgeSetProperty =
            IsBadgeSetPropertyKey.DependencyProperty;

        public bool IsBadgeSet
        {
            get { return (bool)GetValue(IsBadgeSetProperty); }
            private set { SetValue(IsBadgeSetPropertyKey, value); }
        }

        private static void OnBadgeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (BadgedBase)d;

            instance.IsBadgeSet = !string.IsNullOrWhiteSpace(e.NewValue as string) || (e.NewValue != null && !(e.NewValue is string));

            var args = new RoutedPropertyChangedEventArgs<object>(
                e.OldValue,
                e.NewValue)
            { RoutedEvent = BadgeChangedEvent };
            instance.RaiseEvent(args);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            BadgeContainer = GetTemplateChild(BadgeContainerPartName) as FrameworkElement;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var result = base.ArrangeOverride(arrangeBounds);

            if (BadgeContainer == null) return result;

            var containerDesiredSize = BadgeContainer.DesiredSize;
            if ((containerDesiredSize.Width <= 0.0 || containerDesiredSize.Height <= 0.0)
                && !double.IsNaN(BadgeContainer.ActualWidth) && !double.IsInfinity(BadgeContainer.ActualWidth)
                && !double.IsNaN(BadgeContainer.ActualHeight) && !double.IsInfinity(BadgeContainer.ActualHeight))
            {
                containerDesiredSize = new Size(BadgeContainer.ActualWidth, BadgeContainer.ActualHeight);
            }

            var h = 0 - containerDesiredSize.Width / 2;
            var v = 0 - containerDesiredSize.Height / 2;
            BadgeContainer.Margin = new Thickness(0);
            BadgeContainer.Margin = new Thickness(h, v, h, v);

            return result;
        }
    }
}