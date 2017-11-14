using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Core.Helpers;
using My.CoachManager.Presentation.Core.Commands;
using System.ComponentModel;

namespace My.CoachManager.Presentation.ViewModels.Core
{
    public abstract class ReadOnlyListViewModel<TEntityViewModel, TFiltersViewModel> : ReadOnlyListViewModel<TEntityViewModel>
        where TFiltersViewModel : FiltersViewModel
        where TEntityViewModel : INotifyPropertyChanged
    {
        #region Fields

        private TFiltersViewModel _filters;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected ReadOnlyListViewModel()
            : this(Activator.CreateInstance<TFiltersViewModel>())
        {
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="filters"></param>
        protected ReadOnlyListViewModel(TFiltersViewModel filters)
        {
            Filters = filters;
            ActiveFilters = ConvertHelper.ChangeType<TFiltersViewModel>(Filters.Clone());
            DefaultFilters = ConvertHelper.ChangeType<TFiltersViewModel>(ActiveFilters.Clone());
            FilterCommand = new DelegateCommand(Filter, CanFilter);
        }

        #endregion Constructors and Destructors

        #region Public Properties

        /// <summary>
        /// Get or Set Filter Command.
        /// </summary>
        public ICommand FilterCommand { get; set; }

        /// <summary>
        /// Get or set search parameter.
        /// </summary>
        public TFiltersViewModel Filters
        {
            get
            {
                return _filters;
            }

            set
            {
                if (_filters == value)
                {
                    return;
                }

                _filters = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Get or set actives search parameters.
        /// </summary>
        protected TFiltersViewModel ActiveFilters { get; set; }

        /// <summary>
        /// Get or set actives search parameters.
        /// </summary>
        protected TFiltersViewModel DefaultFilters { get; set; }

        #endregion Public Properties

        #region Public Methods and Operators

        /// <summary>
        /// Search.
        /// </summary>
        public virtual void Filter()
        {
            ActiveFilters = ConvertHelper.ChangeType<TFiltersViewModel>(Filters.Clone());

            RefreshData();
        }

        /// <summary>
        /// Refresh items.
        /// </summary>
        public override void RefreshItems()
        {
            Filters = ConvertHelper.ChangeType<TFiltersViewModel>(ActiveFilters.Clone());

            RefreshData();
        }

        /// <summary>
        /// Reset items.
        /// </summary>
        public override void ResetItems()
        {
            Filters = ConvertHelper.ChangeType<TFiltersViewModel>(DefaultFilters.Clone());
            ActiveFilters = ConvertHelper.ChangeType<TFiltersViewModel>(DefaultFilters.Clone());

            RefreshData();
        }

        /// <summary>
        /// Can Filter.
        /// </summary>
        public virtual bool CanFilter()
        {
            return Filters != null;
        }

        #endregion Public Methods and Operators
    }

    /// <summary>
    /// A base class for all viewmodels that cater to workspace views.
    /// </summary>
    public abstract class ReadOnlyListViewModel<TEntityViewModel> : NavigatableViewModel where TEntityViewModel : INotifyPropertyChanged
    {
        #region Fields

        private int _count;

        private ObservableCollection<TEntityViewModel> _entities;

        private TEntityViewModel _selectedItem;

        private bool _limitResults;

        #endregion Fields

        #region Constructors and Destructors

        protected ReadOnlyListViewModel()
        {
            _limitResults = true;

            RefreshCommand = new DelegateCommand(RefreshItems, CanRefresh);
            ResetCommand = new DelegateCommand(ResetItems, CanRefresh);
            OpenItemCommand = new ParameterizedDelegateCommand<TEntityViewModel>(OpenItem, CanOpenItem);
        }

        #endregion Constructors and Destructors

        #region Public Properties

        /// <summary>
        /// Get or set the count results.
        /// </summary>
        public int Count
        {
            get
            {
                return _count;
            }

            protected set
            {
                if (_count == value)
                {
                    return;
                }

                _count = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// <value>True</value> if the number of results are limited
        /// </summary>
        public bool LimitResults
        {
            get
            {
                return _limitResults;
            }

            set
            {
                if (value.Equals(_limitResults))
                {
                    return;
                }

                _limitResults = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Get the entities.
        /// </summary>
        public ObservableCollection<TEntityViewModel> Entities
        {
            get
            {
                return _entities;
            }

            protected set
            {
                if (value == _entities)
                {
                    return;
                }

                _entities = value;
                NotifyOfPropertyChange(() => Entities);
                NotifyOfPropertyChange(() => RefreshCommand);
                NotifyOfPropertyChange(() => ResetCommand);
            }
        }

        /// <summary>
        /// Get or set the selected item.
        /// </summary>
        public TEntityViewModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                if (Equals(value, _selectedItem))
                {
                    return;
                }

                _selectedItem = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Get or Set refresh Command.
        /// </summary>
        public ICommand RefreshCommand { get; set; }

        /// <summary>
        /// Get or Set reset Command.
        /// </summary>
        public ICommand ResetCommand { get; set; }

        /// <summary>
        /// Get or Set open item Command.
        /// </summary>
        public ICommand OpenItemCommand { get; set; }

        #endregion Public Properties

        #region Methods

        /// <summary>
        /// Refresh Items.
        /// </summary>
        public virtual void RefreshItems()
        {
            RefreshData();
        }

        /// <summary>
        /// Can Remove item.
        /// </summary>
        public virtual bool CanRefresh()
        {
            return true;
        }

        /// <summary>
        /// Reset Items.
        /// </summary>
        public virtual void ResetItems()
        {
            RefreshData();
        }

        /// <summary>
        /// Can Reset item.
        /// </summary>
        public virtual bool CanReset()
        {
            return true;
        }

        public void OpenItem(TEntityViewModel entity)
        {
            if (CanOpenItem(entity))
            {
                OpenItemCore(entity);
            }
        }

        protected abstract void OpenItemCore(TEntityViewModel entity);

        public virtual bool CanOpenItem(TEntityViewModel entity)
        {
            return entity != null;
        }

        /// <summary>
        /// Create keyboard shortcut on item.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="entity"></param>
        public virtual void KeyboardActionOnItem(KeyEventArgs args, TEntityViewModel entity)
        {
            switch (args.Key)
            {
                case Key.Enter:
                    OpenItem(entity);
                    args.Handled = true;
                    break;
            }
        }

        #endregion Methods
    }
}