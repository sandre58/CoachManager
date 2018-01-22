using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace My.CoachManager.Presentation.Prism.Controls
{
    [TemplatePart(Name = BadgeContainerPartName, Type = typeof(UIElement))]
    public class Badged : BadgedBase
    {
        public static readonly DependencyProperty BadgeChangedStoryboardProperty = DependencyProperty.Register(
            "BadgeChangedStoryboard", typeof(Storyboard), typeof(Badged), new PropertyMetadata(default(Storyboard)));

        public Storyboard BadgeChangedStoryboard
        {
            get { return (Storyboard)GetValue(BadgeChangedStoryboardProperty); }
            set { SetValue(BadgeChangedStoryboardProperty, value); }
        }

        static Badged()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Badged), new FrameworkPropertyMetadata(typeof(Badged)));
        }

        public override void OnApplyTemplate()
        {
            BadgeChanged -= OnBadgeChanged;

            base.OnApplyTemplate();

            BadgeChanged += OnBadgeChanged;
        }

        private void OnBadgeChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var sb = BadgeChangedStoryboard;
            if (BadgeContainer != null && sb != null)
            {
                try
                {
                    BadgeContainer.BeginStoryboard(sb);
                }
                catch (Exception exception)
                {
                    throw new InvalidOperationException("Uups, it seems like there is something wrong with the given Storyboard.", exception);
                }
            }
        }
    }
}