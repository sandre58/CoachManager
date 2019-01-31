using System.Windows;
using System.Windows.Controls;

namespace My.CoachManager.Presentation.Controls
{
    public class SelectorItem : ContentControl
    {
        #region Constructors

        static SelectorItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectorItem), new FrameworkPropertyMetadata(typeof(SelectorItem)));
        }

        #endregion Constructors

        #region Properties

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(SelectorItem), new UIPropertyMetadata(false, OnIsSelectedChanged));

        public bool IsSelected
        {
            get
            {
                return (bool)GetValue(IsSelectedProperty);
            }
            set
            {
                SetValue(IsSelectedProperty, value);
            }
        }

        private static void OnIsSelectedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            SelectorItem selectorItem = o as SelectorItem;
            if (selectorItem != null)
                selectorItem.OnIsSelectedChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnIsSelectedChanged(bool oldValue, bool newValue)
        {
            if (newValue)
                RaiseEvent(new RoutedEventArgs(SelectorControl.SelectedEvent, this));
            else
                RaiseEvent(new RoutedEventArgs(SelectorControl.UnSelectedEvent, this));
        }

        internal SelectorControl ParentSelector
        {
            get
            {
                return ItemsControl.ItemsControlFromItemContainer(this) as SelectorControl;
            }
        }

        #endregion Properties

        #region Events

        public static readonly RoutedEvent SelectedEvent = SelectorControl.SelectedEvent.AddOwner(typeof(SelectorItem));
        public static readonly RoutedEvent UnselectedEvent = SelectorControl.UnSelectedEvent.AddOwner(typeof(SelectorItem));

        #endregion Events
    }
}