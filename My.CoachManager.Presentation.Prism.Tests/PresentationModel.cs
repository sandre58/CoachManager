using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Data;
using System.Windows.Threading;

namespace My.CoachManager.Presentation.Prism.Tests
{
    internal class PresentationModel : INotifyPropertyChanged
    {
        private const uint _itemsCount = 10;
        private ObservableCollection<Data> _data = new ObservableCollection<Data>();
        private ICollectionView _collectionView;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private readonly DispatcherTimer _dataUpdateTimer;
        private readonly Dispatcher _dispatcher = Dispatcher.CurrentDispatcher;
        private readonly Random _random = new Random((int)DateTime.Now.Ticks);

        private readonly int _threadId;

        private StringFilter test;

        public StringFilter Test
        {
            get { return test; }
            set { test = value; }
        }

        private bool _test1;

        public bool Test1
        {
            get { return _test1; }
            set
            {
                _test1 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Test1"));

                var col = _collectionView as FilteredCollectionView;
                if (_test1)
                {
                    col.AddFilter(test);
                }
                else
                {
                    col.RemoveFilter(test);
                }
            }
        }

        public PresentationModel()
        {
            _collectionView = new FilteredCollectionView(_data);
            InitializeData();

            _dataUpdateTimer = new DispatcherTimer();
            _dataUpdateTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            _dataUpdateTimer.Tick += new EventHandler(OnUpdateTimerTick);

            _threadId = Thread.CurrentThread.ManagedThreadId;

            test = new StringFilter(typeof(Data).GetProperty("LastName"), StringFilterMode.Contains);
        }

        private void OnUpdateTimerTick(object sender, EventArgs e)
        {
            Debug.Assert(_threadId == Thread.CurrentThread.ManagedThreadId, "Cross thread call");
            int index = _random.Next(0, (int)_itemsCount);
            if (!_dispatcher.CheckAccess())
            {
                _dispatcher.BeginInvoke(new ThreadStart(() =>
                {
                    _data[index].Value = _random.NextDouble() * 10000 / DateTime.Now.Millisecond;
                }), null);
                return;
            }
            _data[index].Value = _random.NextDouble() * 10000 / DateTime.Now.Millisecond;
        }

        public bool IsFakeUpdateEnabled
        {
            get { return _dataUpdateTimer.IsEnabled; }
            set
            {
                if (value)
                {
                    _dataUpdateTimer.Start();
                }
                else
                {
                    _dataUpdateTimer.Stop();
                }
            }
        }

        private void InitializeData()
        {
            for (uint i = 0; i < _itemsCount; i++)
            {
                _data.Add(new Data()
                {
                    FirstName = "First" + i,
                    LastName = "Last" + i,
                    Age = i,
                    Birthday = DateTime.Today.AddYears(-((int)i)),
                });
            }
        }

        public ICollectionView Data
        {
            get { return _collectionView; }
        }
    }
}