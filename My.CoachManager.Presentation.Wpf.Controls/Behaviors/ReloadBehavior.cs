using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using My.CoachManager.Presentation.Wpf.Controls.Helpers;

namespace My.CoachManager.Presentation.Wpf.Controls.Behaviors
{
    public static class ReloadBehavior
    {
        public static readonly DependencyProperty OnDataContextChangedProperty =
            DependencyProperty.RegisterAttached("OnDataContextChanged", typeof(bool), typeof(ReloadBehavior), new PropertyMetadata(OnDataContextChanged));

        [Category(Constants.ParameterCategory)]
        public static bool GetOnDataContextChanged(ExtendedContentControl element)
        {
            var value = element.GetValue(OnDataContextChangedProperty);
            return value != null && (bool)value;
        }

        public static void SetOnDataContextChanged(ExtendedContentControl element, bool value)
        {
            element.SetValue(OnDataContextChangedProperty, value);
        }

        private static void OnDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ExtendedContentControl)d).DataContextChanged += ReloadDataContextChanged;
        }

        private static void ReloadDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((ExtendedContentControl)sender).Reload();
        }

        public static DependencyProperty OnSelectedTabChangedProperty =
            DependencyProperty.RegisterAttached("OnSelectedTabChanged", typeof(bool), typeof(ReloadBehavior), new PropertyMetadata(OnSelectedTabChanged));

        [Category(Constants.ParameterCategory)]
        public static bool GetOnSelectedTabChanged(System.Windows.Controls.ContentControl element)
        {
            var value = element.GetValue(OnDataContextChangedProperty);
            return value != null && (bool)value;
        }

        public static void SetOnSelectedTabChanged(System.Windows.Controls.ContentControl element, bool value)
        {
            element.SetValue(OnDataContextChangedProperty, value);
        }

        private static void OnSelectedTabChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((System.Windows.Controls.ContentControl)d).Loaded += ReloadLoaded;
        }

        private static void ReloadLoaded(object sender, RoutedEventArgs e)
        {
            var ExtendedContentControl = ((ContentControl)sender);
            var tab = Ancestors(ExtendedContentControl)
                .OfType<TabControl>()
                .FirstOrDefault();

            if (tab == null) return;

            SetExtendedContentControl(tab, ExtendedContentControl);
            tab.SelectionChanged -= ReloadSelectionChanged;
            tab.SelectionChanged += ReloadSelectionChanged;
        }

        private static IEnumerable<DependencyObject> Ancestors(DependencyObject obj)
        {
            var parent = VisualTreeHelper.GetParent(obj);
            while (parent != null)
            {
                yield return parent;
                obj = parent;
                parent = VisualTreeHelper.GetParent(obj);
            }
        }

        private static void ReloadSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource != sender)
                return;

            var contentControl = GetExtendedContentControl((TabControl)sender);
            var ExtendedContentControl = contentControl as ExtendedContentControl;
            if (ExtendedContentControl != null)
            {
                ExtendedContentControl.Reload();
            }

            var transitioningContentControl = contentControl as TransitioningContentControl;
            if (transitioningContentControl != null)
            {
                transitioningContentControl.ReloadTransition();
            }
        }

        public static readonly DependencyProperty ExtendedContentControlProperty =
            DependencyProperty.RegisterAttached("ExtendedContentControl", typeof(ContentControl), typeof(ReloadBehavior), new PropertyMetadata(default(ContentControl)));

        public static void SetExtendedContentControl(UIElement element, ContentControl value)
        {
            element.SetValue(ExtendedContentControlProperty, value);
        }

        [Category(Constants.ParameterCategory)]
        public static ContentControl GetExtendedContentControl(UIElement element)
        {
            return (ContentControl)element.GetValue(ExtendedContentControlProperty);
        }
    }
}