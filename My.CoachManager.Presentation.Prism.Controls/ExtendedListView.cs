using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using My.CoachManager.Presentation.Prism.Controls.GridViews.Columns;
using My.CoachManager.Presentation.Prism.Core.Commands;

namespace My.CoachManager.Presentation.Prism.Controls
{
    public class ExtendedListView : ListView
    {
        #region Fields

        private GridViewColumnHeader _columnHeader;

        #endregion Fields

        #region DoubleClickCommandProperty

        public static readonly DependencyProperty DoubleClickCommandProperty = DependencyProperty.Register(
            "DoubleClickCommand",
            typeof(ICommand),
            typeof(ExtendedListView));

        public ICommand DoubleClickCommand
        {
            get => (ICommand)GetValue(DoubleClickCommandProperty);
            set => SetValue(DoubleClickCommandProperty, value);
        }

        #endregion DoubleClickCommandProperty

        #region CustomSelectionModeProperty

        public static readonly DependencyProperty CustomSelectionModeProperty = DependencyProperty.Register(
            "CustomSelectionMode",
            typeof(Core.Enums.SelectionMode),
            typeof(ExtendedListView),
            new PropertyMetadata(Core.Enums.SelectionMode.Single));

        public Core.Enums.SelectionMode CustomSelectionMode
        {
            get => (Core.Enums.SelectionMode)GetValue(CustomSelectionModeProperty);
            set => SetValue(CustomSelectionModeProperty, value);
        }

        #endregion CustomSelectionModeProperty

        #region CanOrderProperty

        public static readonly DependencyProperty CanOrderProperty = DependencyProperty.Register(
            "CanOrder",
            typeof(bool),
            typeof(ExtendedListView),
            new PropertyMetadata(false));

        public bool CanOrder
        {
            get => (bool)GetValue(CanOrderProperty);
            set => SetValue(CanOrderProperty, value);
        }

        #endregion CanOrderProperty

        #region CanSortProperty

        public static readonly DependencyProperty CanSortProperty = DependencyProperty.Register(
            "CanSort",
            typeof(bool),
            typeof(ExtendedListView),
            new PropertyMetadata(false, OnCanSortPropertyChanged));

        public bool CanSort
        {
            get => (bool)GetValue(CanSortProperty);
            set => SetValue(CanSortProperty, value);
        }

        private static void OnCanSortPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o is ExtendedListView listView)
            {
                listView.OnCanSortPropertyChanged(e);
            }
        }

        private void OnCanSortPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            bool oldValue = (bool)e.OldValue;
            bool newValue = (bool)e.NewValue;
            if (oldValue && !newValue)
                RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
            if (!oldValue && newValue)
                AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
        }

        private void ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is GridViewColumnHeader headerClicked && headerClicked.Column is ExtendedGridViewColumn column)
            {
                if (!string.IsNullOrEmpty(column.PropertyName))
                {
                    if (CanSort)
                    {
                        ApplySort(headerClicked, column.PropertyName);
                    }
                }
            }
        }

        private void ApplySort(GridViewColumnHeader clickedColumnHeader, string propertyName)
        {
            GridViewColumnHeader currentSortedColumnHeader = _columnHeader;
            ListSortDirection direction = GetSortDirection(currentSortedColumnHeader, clickedColumnHeader, propertyName);

            var view = Items;
            using (view.DeferRefresh())
            {
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription(propertyName, direction));
            }
            _columnHeader = clickedColumnHeader;

            if (currentSortedColumnHeader != null && !Equals(currentSortedColumnHeader, clickedColumnHeader))
                SetIsSorting(currentSortedColumnHeader, false);

            SetSortDirection(clickedColumnHeader, direction);
            SetIsSorting(clickedColumnHeader, true);
        }

        private ListSortDirection GetSortDirection(GridViewColumnHeader currentSortedColumnHeader, GridViewColumnHeader clickedColumnHeader, string propertyName)
        {
            if (!Equals(currentSortedColumnHeader, clickedColumnHeader))
                return ListSortDirection.Ascending;

            ICollectionView view = Items;
            if (view.SortDescriptions.Count == 0)
                return ListSortDirection.Ascending;

            var currentSort = view.SortDescriptions[0];
            if (currentSort.PropertyName == propertyName)
            {
                return currentSort.Direction == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            }
            return ListSortDirection.Ascending;
        }

        #endregion CanSortProperty

        #region SortDirection

        public static readonly DependencyProperty SortDirectionProperty = DependencyProperty.RegisterAttached("SortDirection", typeof(ListSortDirection), typeof(ExtendedListView),
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

        public static readonly DependencyProperty IsSortingProperty = DependencyProperty.RegisterAttached("IsSorting", typeof(bool), typeof(ExtendedListView),
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

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ExtendedListViewItem();
        }
    }
}