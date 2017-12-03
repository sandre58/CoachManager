using System.Windows;
using System.Windows.Controls;

namespace My.CoachManager.Presentation.Prism.Controls
{
    /// <summary>
    /// Originally from http://xamlcoder.com/blog/2010/11/04/creating-a-metro-ui-style-control/
    /// </summary>
    public class ExtendedContentControl : System.Windows.Controls.ContentControl
    {
        public static readonly DependencyProperty ReverseTransitionProperty = DependencyProperty.Register("ReverseTransition", typeof(bool), typeof(ExtendedContentControl), new FrameworkPropertyMetadata(false));

        public bool ReverseTransition
        {
            get
            {
                var value = GetValue(ReverseTransitionProperty);
                return value != null && (bool)value;
            }
            set { SetValue(ReverseTransitionProperty, value); }
        }

        public static readonly DependencyProperty TransitionsEnabledProperty = DependencyProperty.Register("TransitionsEnabled", typeof(bool), typeof(ExtendedContentControl), new FrameworkPropertyMetadata(true));

        public bool TransitionsEnabled
        {
            get
            {
                var value = GetValue(TransitionsEnabledProperty);
                return value != null && (bool)value;
            }
            set { SetValue(TransitionsEnabledProperty, value); }
        }

        public static readonly DependencyProperty OnlyLoadTransitionProperty = DependencyProperty.Register("OnlyLoadTransition", typeof(bool), typeof(ExtendedContentControl), new FrameworkPropertyMetadata(false));

        public bool OnlyLoadTransition
        {
            get
            {
                var value = GetValue(OnlyLoadTransitionProperty);
                return value != null && (bool)value;
            }
            set { SetValue(OnlyLoadTransitionProperty, value); }
        }

        private bool _transitionLoaded;

        public ExtendedContentControl()
        {
            DefaultStyleKey = typeof(ExtendedContentControl);

            Loaded += ExtendedContentControlLoaded;
            Unloaded += ExtendedContentControlUnloaded;
        }

        private void ExtendedContentControlIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (TransitionsEnabled && !_transitionLoaded)
            {
                if (!IsVisible)
                    VisualStateManager.GoToState(this, ReverseTransition ? "AfterUnLoadedReverse" : "AfterUnLoaded", false);
                else
                    VisualStateManager.GoToState(this, ReverseTransition ? "AfterLoadedReverse" : "AfterLoaded", true);
            }
        }

        private void ExtendedContentControlUnloaded(object sender, RoutedEventArgs e)
        {
            if (TransitionsEnabled)
            {
                if (_transitionLoaded)
                    VisualStateManager.GoToState(this, ReverseTransition ? "AfterUnLoadedReverse" : "AfterUnLoaded", false);
                IsVisibleChanged -= ExtendedContentControlIsVisibleChanged;
            }
        }

        private void ExtendedContentControlLoaded(object sender, RoutedEventArgs e)
        {
            if (TransitionsEnabled)
            {
                if (!_transitionLoaded)
                {
                    _transitionLoaded = OnlyLoadTransition;
                    VisualStateManager.GoToState(this, ReverseTransition ? "AfterLoadedReverse" : "AfterLoaded", true);
                }
                IsVisibleChanged -= ExtendedContentControlIsVisibleChanged;
                IsVisibleChanged += ExtendedContentControlIsVisibleChanged;
            }
            else
            {
                var root = (Grid)GetTemplateChild("root");
                if (root != null)
                {
                    root.Opacity = 1.0;
                    var transform = ((System.Windows.Media.TranslateTransform)root.RenderTransform);
                    if (transform.IsFrozen)
                    {
                        var modifiedTransform = transform.Clone();
                        modifiedTransform.X = 0;
                        root.RenderTransform = modifiedTransform;
                    }
                    else
                    {
                        transform.X = 0;
                    }
                }
            }
        }

        public void Reload()
        {
            if (!TransitionsEnabled || _transitionLoaded) return;

            if (ReverseTransition)
            {
                VisualStateManager.GoToState(this, "BeforeLoaded", true);
                VisualStateManager.GoToState(this, "AfterUnLoadedReverse", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "BeforeLoaded", true);
                VisualStateManager.GoToState(this, "AfterLoaded", true);
            }
        }
    }
}