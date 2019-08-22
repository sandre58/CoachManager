using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace My.CoachManager.Presentation.Wpf.Controls.GridViews.Columns
{
    public class ExtendedGridViewColumn : GridViewColumn
    {
        #region Hidden Columns

        #region CanUserHideColumns

        public static readonly DependencyProperty CanUserHideColumnProperty = DependencyProperty.Register(
            "CanUserHideColumn",
            typeof(bool),
            typeof(ExtendedGridViewColumn),
            new PropertyMetadata(true));

        public bool CanUserHideColumn
        {
            get => (bool)GetValue(CanUserHideColumnProperty);
            set => SetValue(CanUserHideColumnProperty, value);
        }

        #endregion CanUserHideColumns

        #region CanSort

        public static readonly DependencyProperty CanSortProperty = DependencyProperty.Register(
            "CanSort",
            typeof(bool),
            typeof(ExtendedGridViewColumn),
            new PropertyMetadata(true));

        public bool CanSort
        {
            get => (bool)GetValue(CanSortProperty);
            set => SetValue(CanSortProperty, value);
        }

        #endregion CanSort

        #region Visibility

        /// <summary>
        ///     Dependency property for Visibility
        /// </summary>
        public static readonly DependencyProperty VisibilityProperty =
            DependencyProperty.Register(
                "Visibility",
                typeof(Visibility),
                typeof(ExtendedGridViewColumn),
                new FrameworkPropertyMetadata(Visibility.Visible, OnVisibilityPropertyChanged));

        /// <summary>
        ///     The property which determines if the column is visible or not.
        /// </summary>
        public Visibility Visibility
        {
            get => (Visibility)GetValue(VisibilityProperty);
            set => SetValue(VisibilityProperty, value);
        }

        /// <summary>
        ///     Property changed callback for Visibility property
        /// </summary>
        private static void OnVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs eventArgs)
        {
            var column = d as ExtendedGridViewColumn;
            Visibility oldVisibility = (Visibility)eventArgs.OldValue;
            Visibility newVisibility = (Visibility)eventArgs.NewValue;

            if (oldVisibility != Visibility.Visible && newVisibility != Visibility.Visible)
            {
                return;
            }

            if (newVisibility == Visibility.Visible)
            {
                if (column != null) column.Width = column._internalWidth;
            }
            else
            {
                if (column != null)
                {
                    column._internalWidth = column.Width;
                    column.Width = 0;
                }
            }
        }

        /// <summary>
        ///     Helper IsVisible property
        /// </summary>
        internal bool IsVisible => Visibility == Visibility.Visible;

        private double _internalWidth;

        #endregion Visibility

        #region PropertyName

        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register(
            "PropertyName",
            typeof(string),
            typeof(ExtendedGridViewColumn),
            new PropertyMetadata(string.Empty));

        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }

        #endregion PropertyName

        #endregion Hidden Columns

        #region SortDirection

        public static readonly DependencyProperty SortDirectionProperty = DependencyProperty.RegisterAttached("SortDirection", typeof(ListSortDirection), typeof(ExtendedGridViewColumn),
            new FrameworkPropertyMetadata(ListSortDirection.Ascending));

        public static ListSortDirection GetSortDirection(GridViewColumnHeader element)
        {
            return (ListSortDirection)element.GetValue(SortDirectionProperty);
        }

        public static void SetSortDirection(GridViewColumnHeader element, ListSortDirection value)
        {
            element.SetValue(SortDirectionProperty, value);
        }

        #endregion SortDirection

        #region IsSorting

        public static readonly DependencyProperty IsSortingProperty = DependencyProperty.RegisterAttached("IsSorting", typeof(bool), typeof(ExtendedGridViewColumn),
            new FrameworkPropertyMetadata(false));

        public static bool GetIsSorting(GridViewColumnHeader element)
        {
            return (bool)element.GetValue(IsSortingProperty);
        }

        public static void SetIsSorting(GridViewColumnHeader element, bool value)
        {
            element.SetValue(IsSortingProperty, value);
        }

        #endregion IsSorting
    }
}