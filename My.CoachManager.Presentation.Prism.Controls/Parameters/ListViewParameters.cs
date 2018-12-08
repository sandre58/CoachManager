using System.ComponentModel;
using System.Windows.Controls.Primitives;
using My.CoachManager.Presentation.Prism.Controls.Helpers;

namespace My.CoachManager.Presentation.Prism.Controls.Parameters
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// <see cref="ListView"/> attached properties.
    /// </summary>
    public static class ListViewParameters
    {
        #region CanSort

        public static readonly DependencyProperty CanSortProperty = DependencyProperty.RegisterAttached(
            "CanSort",
            typeof(bool),
            typeof(ListViewParameters),
            new FrameworkPropertyMetadata(false, OnCanSortPropertyChanged));

        /// <summary>
        /// Gets whether the user can hide columns.
        /// </summary>
        /// <param name="dataGrid">The data grid.</param>
        /// <returns><c>true</c> if the user can hide columns, otherwise <c>false</c>.</returns>
        public static bool GetCanSort(ListView dataGrid)
        {
            var value = dataGrid.GetValue(CanSortProperty);
            return value != null && (bool)value;
        }

        /// <summary>
        /// Sets whether the user can hide columns.
        /// </summary>
        /// <param name="dataGrid">The data grid.</param>
        /// <param name="value">if set to <c>true</c> the user can hide columns.</param>
        public static void SetCanSort(ListView dataGrid, bool value)
        {
            dataGrid.SetValue(CanSortProperty, value);
        }

        private static void OnCanSortPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o is ListView listView)
            {

                    bool oldValue = (bool)e.OldValue;
                    bool newValue = (bool)e.NewValue;
                    if (oldValue && !newValue)
                        listView.RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                    if (!oldValue && newValue)
                        listView.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
            }
        }

        private static void ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is GridViewColumnHeader headerClicked && headerClicked.Column != null)
            {
                string propertyName = GetPropertyName(headerClicked.Column);
                if (!string.IsNullOrEmpty(propertyName))
                {
                    var listView = headerClicked.FindVisualParent<ListView>();
                    if (listView != null)
                    {
                        if (GetCanSort(listView))
                        {
                            ApplySort(listView, headerClicked, propertyName);
                        }
                    }
                }
            }
        }

        static void ApplySort(ListView listView, GridViewColumnHeader clickedColumnHeader, string propertyName)
        {
            GridViewColumnHeader currentSortedColumnHeader = GetSortedColumnHeader(listView);
            ListSortDirection direction = GetSortDirection(listView, currentSortedColumnHeader, clickedColumnHeader, propertyName);

            var view = listView.Items;
            using (view.DeferRefresh())
            {
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription(propertyName, direction));
            }
            SetSortedColumnHeader(listView, clickedColumnHeader);

            if (currentSortedColumnHeader != null && !Equals(currentSortedColumnHeader, clickedColumnHeader))
                SetIsSorting(currentSortedColumnHeader, false);

            SetSortDirection(clickedColumnHeader, direction);
            SetIsSorting(clickedColumnHeader, true);
        }

        static ListSortDirection GetSortDirection(ListView listView, GridViewColumnHeader currentSortedColumnHeader, GridViewColumnHeader clickedColumnHeader, string propertyName)
        {
            if (!Equals(currentSortedColumnHeader, clickedColumnHeader))
                return ListSortDirection.Ascending;

            ICollectionView view = listView.Items;
            if (view.SortDescriptions.Count == 0)
                return ListSortDirection.Ascending;

            var currentSort = view.SortDescriptions[0];
            if (currentSort.PropertyName == propertyName)
            {
                return currentSort.Direction == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            }
            return ListSortDirection.Ascending;
        }

        #endregion

        #region SortOrder

        private static readonly DependencyProperty SortDirectionProperty = DependencyProperty.RegisterAttached("SortDirection", typeof(ListSortDirection), typeof(ListViewParameters),
            new FrameworkPropertyMetadata(ListSortDirection.Ascending));

        public static ListSortDirection GetSortDirection(GridViewColumnHeader element)
        {
            return (ListSortDirection)element.GetValue(SortDirectionProperty);
        }

        private static void SetSortDirection(GridViewColumnHeader element, ListSortDirection value)
        {
            element.SetValue(SortDirectionProperty, value);
        }

        #endregion

        #region IsSorting

        private static readonly DependencyProperty IsSortingProperty = DependencyProperty.RegisterAttached("IsSorting", typeof(bool), typeof(ListViewParameters),
            new FrameworkPropertyMetadata(false));

        public static bool GetIsSorting(GridViewColumnHeader element)
        {
            return (bool)element.GetValue(IsSortingProperty);
        }

        private static void SetIsSorting(GridViewColumnHeader element, bool value)
        {
            element.SetValue(IsSortingProperty, value);
        }

        #endregion

        #region PropertyName

        private static readonly DependencyProperty PropertyNameProperty = DependencyProperty.RegisterAttached("PropertyName", typeof(string), typeof(ListViewParameters));

        public static string GetPropertyName(GridViewColumn element)
        {
            return (string)element.GetValue(PropertyNameProperty);
        }

        public static void SetPropertyName(GridViewColumn element, string value)
        {
            element.SetValue(PropertyNameProperty, value);
        }

        #endregion

        #region SortedColumn

        private static readonly DependencyProperty SortedColumnHeaderProperty = DependencyProperty.RegisterAttached("SortedColumnHeader", typeof(GridViewColumnHeader), typeof(ListViewParameters),
            new FrameworkPropertyMetadata(null));

        public static GridViewColumnHeader GetSortedColumnHeader(ListView element)
        {
            return (GridViewColumnHeader)element.GetValue(SortedColumnHeaderProperty);
        }

        private static void SetSortedColumnHeader(ListView element, GridViewColumnHeader value)
        {
            element.SetValue(SortedColumnHeaderProperty, value);
        }

        #endregion
    }
}