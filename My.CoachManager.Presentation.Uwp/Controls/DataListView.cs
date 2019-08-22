using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace My.CoachManager.Presentation.Uwp.Controls
{
    public class DataListView : ListView
    {

        private bool _initialized;
        private Grid _titleRowControl;

        /// <summary>Initializes a new instance of the <see cref="DataListView"/> class. </summary>
        public DataListView()
        {
           Columns = new DataListViewColumnsCollection(); // Initialize collection so that columns can be defined in XAML
            
        }

        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.RegisterAttached("Columns",
            typeof(ObservableCollection<DataListViewColumn>), typeof(DataListView), new PropertyMetadata(null, OnColumnsPropertyChanged));

        /// <summary>Gets the column description of the <see cref="DataListView"/>. </summary>
        public DataListViewColumnsCollection Columns
        {
            get => (DataListViewColumnsCollection)GetValue(ColumnsProperty);
            set => SetValue(ColumnsProperty, value);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _titleRowControl = (Grid)GetTemplateChild("ColumnHeaders");

            Initialize();
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DataListViewItem();
        }

        private static void OnColumnsPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var dataGrid = (DataListView)obj;

            var oldList = args.OldValue as INotifyCollectionChanged;
            var newList = args.NewValue as INotifyCollectionChanged;

            if (oldList != null)
                oldList.CollectionChanged -= dataGrid.OnColumnsChanged;
            if (newList != null)
                newList.CollectionChanged += dataGrid.OnColumnsChanged;

            dataGrid.OnColumnsChanged(null, null);
        }

        private void OnColumnsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (_initialized)
            {
                UpdateColumnHeaders();
            }
        }

        private void UpdateColumnHeaders()
        {
            var columnIndex = 0;
            var hasStar = false;

            _titleRowControl.Children.Clear();
            _titleRowControl.ColumnDefinitions.Clear();

            foreach (var column in Columns)
            {
                // Create header element
                var title = new ContentPresenter
                {
                    Content = column,
                    ContentTemplate = HeaderTemplate
                };

                Grid.SetColumn(title, columnIndex++);
                _titleRowControl.Children.Add(title);

                // Create grid column definition
                var columnDefinition = column.CreateGridColumnDefinition();
                hasStar = hasStar || columnDefinition.Width.IsStar;
                _titleRowControl.ColumnDefinitions.Add(columnDefinition);
            }

            if (!hasStar)
                _titleRowControl.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        }

        private void Initialize()
        {
            if (!_initialized)
            {
                if (_titleRowControl == null)
                    return;

                _initialized = true;

                UpdateColumnHeaders();

            }
        }

    }
}
