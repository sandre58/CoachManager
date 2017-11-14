using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace My.CoachManager.Presentation.Prism.Controls.Behaviours
{
    public static class ReloadBehavior
    {
        public static readonly DependencyProperty OnDataContextChangedProperty =
            DependencyProperty.RegisterAttached("OnDataContextChanged", typeof(bool), typeof(ReloadBehavior), new PropertyMetadata(OnDataContextChanged));

        [Category(Constants.ParameterCategory)]
        public static bool GetOnDataContextChanged(MetroContentControl element)
        {
            var value = element.GetValue(OnDataContextChangedProperty);
            return value != null && (bool)value;
        }

        public static void SetOnDataContextChanged(MetroContentControl element, bool value)
        {
            element.SetValue(OnDataContextChangedProperty, value);
        }

        private static void OnDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MetroContentControl)d).DataContextChanged += ReloadDataContextChanged;
        }

        private static void ReloadDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((MetroContentControl)sender).Reload();
        }

        public static DependencyProperty OnSelectedTabChangedProperty =
            DependencyProperty.RegisterAttached("OnSelectedTabChanged", typeof(bool), typeof(ReloadBehavior), new PropertyMetadata(OnSelectedTabChanged));

        [Category(Constants.ParameterCategory)]
        public static bool GetOnSelectedTabChanged(ContentControl element)
        {
            var value = element.GetValue(OnDataContextChangedProperty);
            return value != null && (bool)value;
        }

        public static void SetOnSelectedTabChanged(ContentControl element, bool value)
        {
            element.SetValue(OnDataContextChangedProperty, value);
        }

        private static void OnSelectedTabChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ContentControl)d).Loaded += ReloadLoaded;
        }

        private static void ReloadLoaded(object sender, RoutedEventArgs e)
        {
            var metroContentControl = ((ContentControl)sender);
            var tab = Ancestors(metroContentControl)
                .OfType<TabControl>()
                .FirstOrDefault();

            if (tab == null) return;

            SetMetroContentControl(tab, metroContentControl);
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

            var contentControl = GetMetroContentControl((TabControl)sender);
            var metroContentControl = contentControl as MetroContentControl;
            if (metroContentControl != null)
            {
                metroContentControl.Reload();
            }

            var transitioningContentControl = contentControl as TransitioningContentControl;
            if (transitioningContentControl != null)
            {
                transitioningContentControl.ReloadTransition();
            }
        }

        public static readonly DependencyProperty MetroContentControlProperty =
            DependencyProperty.RegisterAttached("MetroContentControl", typeof(ContentControl), typeof(ReloadBehavior), new PropertyMetadata(default(ContentControl)));

        public static void SetMetroContentControl(UIElement element, ContentControl value)
        {
            element.SetValue(MetroContentControlProperty, value);
        }

        [Category(Constants.ParameterCategory)]
        public static ContentControl GetMetroContentControl(UIElement element)
        {
            return (ContentControl)element.GetValue(MetroContentControlProperty);
        }
    }
}