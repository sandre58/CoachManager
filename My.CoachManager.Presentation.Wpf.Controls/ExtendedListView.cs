using My.CoachManager.Presentation.Wpf.Controls.GridViews.Columns;
using My.CoachManager.Presentation.Wpf.Core.Filters;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace My.CoachManager.Presentation.Wpf.Controls
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

        #region CustomSortCommand

        public static readonly DependencyProperty CustomSortCommandProperty = DependencyProperty.Register(
            "CustomSortCommand",
            typeof(ICommand),
            typeof(ExtendedListView));

        public ICommand CustomSortCommand
        {
            get => (ICommand)GetValue(CustomSortCommandProperty);
            set => SetValue(CustomSortCommandProperty, value);
        }

        #endregion CustomSortCommand

        #region SortDirection

        public static readonly DependencyProperty SortDirectionProperty = DependencyProperty.Register(
            "SortDirection",
            typeof(ListSortDirection),
            typeof(ExtendedListView));

        public ListSortDirection SortDirection
        {
            get => (ListSortDirection)GetValue(SortDirectionProperty);
            set => SetValue(SortDirectionProperty, value);
        }

        #endregion SortDirection

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
                if (!string.IsNullOrEmpty(column.PropertyName) && column.CanSort)
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

            if (CustomSortCommand != null)
            {
                CustomSortCommand.Execute(new SortDescription(propertyName, direction));
            }
            else
            {
                ICollectionView view;

                if (ItemsSource is IFilteredCollection items)
                    view = items;
                else
                    view = Items;

                using (view.DeferRefresh())
                {
                    view.SortDescriptions.Clear();
                    view.SortDescriptions.Add(new SortDescription(propertyName, direction));
                }
            }

            _columnHeader = clickedColumnHeader;

            if (currentSortedColumnHeader != null && !Equals(currentSortedColumnHeader, clickedColumnHeader))
                ExtendedGridViewColumn.SetIsSorting(currentSortedColumnHeader, false);

            ExtendedGridViewColumn.SetSortDirection(clickedColumnHeader, direction);
            ExtendedGridViewColumn.SetIsSorting(clickedColumnHeader, true);
        }

        private ListSortDirection GetSortDirection(GridViewColumnHeader currentSortedColumnHeader, GridViewColumnHeader clickedColumnHeader, string propertyName)
        {
            if (!Equals(currentSortedColumnHeader, clickedColumnHeader))
                return ListSortDirection.Ascending;

            if (CustomSortCommand != null)
            {
                return SortDirection == ListSortDirection.Ascending
                    ? ListSortDirection.Descending
                    : ListSortDirection.Ascending;
            }

            ICollectionView view;

            if (ItemsSource is IFilteredCollection items)
                view = items;
            else
                view = Items;

            if (view.SortDescriptions.Count == 0)
                return ListSortDirection.Ascending;

            var currentSort = view.SortDescriptions[0];
            if (currentSort.PropertyName == propertyName)
            {
                return currentSort.Direction == ListSortDirection.Ascending
                    ? ListSortDirection.Descending
                    : ListSortDirection.Ascending;
            }
            return ListSortDirection.Ascending;
        }

        #endregion CanSortProperty

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);

            if (CustomSortCommand == null)
            {
                ICollectionView view;

                if (ItemsSource is IFilteredCollection items)
                    view = items;
                else
                    view = Items;

                if (view.SortDescriptions.Any()) view.SortDescriptions.Clear();

                if (_columnHeader != null) ExtendedGridViewColumn.SetIsSorting(_columnHeader, false);
            }
        }
    }
}