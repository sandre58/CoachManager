using System;
using System.Collections;
using System.Collections.Generic;
using My.CoachManager.Presentation.Mvvm.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Mvvm.Core.ViewModels.Base
{
    public abstract class FiltersViewModel<T> : ViewModelBase, IFiltersViewModel<T>
    {
        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        public IEnumerable<T> Items { get; protected set; }

        IEnumerable IFiltersViewModel.Items => Items;

        /// <summary>
        /// Gets or sets filtered items count.
        /// </summary>
        public abstract int FilteredItemsCount { get; }

        /// <summary>
        /// Gets or sets all items count.
        /// </summary>
        public abstract int AllItemsCount { get; }

        /// <summary>
        /// Gets is filtered.
        /// </summary>
        public bool IsFiltered => FilteredItemsCount != AllItemsCount;

        /// <summary>
        /// Gets the allowed filters.
        /// </summary>
        public IList<Tuple<Func<IFilter>, string>> AllowedFilters { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        public ObservableItemsCollection<IFilterViewModel> Filters { get; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        public ObservableItemsCollection<IFilterViewModel> DefaultFilters { get; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the  active filters.
        /// </summary>
        public int CountActiveFilters => Filters?.Count(x => x.IsEnabled) ?? 0;

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the filter visibility.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the filter visibility.
        /// </summary>
        public bool OperatorIsVisible { get; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        public IFilterViewModel SpeedFilter { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Command to reset filters.
        /// </summary>
        public DelegateCommand ResetFiltersCommand { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets Apply Filters Command.
        /// </summary>
        public DelegateCommand ApplyFiltersCommand { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets Show Filters Command.
        /// </summary>
        public DelegateCommand ShowFiltersCommand { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets Hide Filters Command.
        /// </summary>
        public DelegateCommand HideFiltersCommand { get; set; }

        /// <summary>
        /// Gets or sets the Add command.
        /// </summary>
        public DelegateCommand<Tuple<Func<IFilter>, string>> AddFilterCommand { get; set; }

        /// <summary>
        /// Gets or sets the Remove command.
        /// </summary>
        public DelegateCommand<IFilter> RemoveFilterCommand { get; set; }

        /// <summary>
        /// Gets or sets auto filter value.
        /// </summary>
        public bool IsAutoFilter { get; set; }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ListParameters"/>.
        /// </summary>
        public FiltersViewModel(bool isAutoFilter, bool operatorIsVisible = true)
        {
            OnInitializing = true;
            OperatorIsVisible = operatorIsVisible;
            IsAutoFilter = isAutoFilter;
            Filters = new ObservableItemsCollection<IFilterViewModel>();
            DefaultFilters = new ObservableItemsCollection<IFilterViewModel>();
            Filters.CollectionChanged += FiltersCollectionChanged;
            AllowedFilters = new List<Tuple<Func<IFilter>, string>>();

            ShowFiltersCommand = new DelegateCommand(ShowFilters);
            HideFiltersCommand = new DelegateCommand(HideFilters);
            ApplyFiltersCommand = new DelegateCommand(ApplyFilters);
            ResetFiltersCommand = new DelegateCommand(ResetFilters, CanResetFilters);
            AddFilterCommand = new DelegateCommand<Tuple<Func<IFilter>, string>>(AddFilter);
            RemoveFilterCommand = new DelegateCommand<IFilter>(RemoveFilter);
            OnInitializing = false;
        }

        /// <summary>
        /// Update collection.
        /// </summary>
        public virtual void UpdateCollection(IEnumerable<T> collection)
        {
            Items = collection;
        }

        #endregion Constructors

        #region Reset Filters

        /// <summary>
        /// Reset filters.
        /// </summary>
        public void ResetFilters()
        {
            using (DeferFilter())
            {
                SpeedFilter?.Filter.Reset();
                Filters.Clear();
                Filters.AddRange(DefaultFilters);
            }
        }

        /// <summary>
        /// Can reset filters ?
        /// </summary>
        /// <returns></returns>
        private bool CanResetFilters()
        {
            return Filters.Count > 0;
        }

        #endregion Reset Filters

        #region Apply Filters

        /// <summary>
        /// Apply filters.
        /// </summary>
        public void ApplyFilters()
        {
            FilterItems();
        }

        #endregion Apply Filters

        #region Show Filters

        /// <summary>
        /// Show filters.
        /// </summary>
        private void ShowFilters()
        {
            IsVisible = true;
        }

        #endregion Show Filters

        #region Hide Filters

        /// <summary>
        /// Hide filters.
        /// </summary>
        private void HideFilters()
        {
            IsVisible = false;
        }

        #endregion Hide Filters

        #region GetFilter

        /// <summary>
        /// Get a filter.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public IFilterViewModel GetFilter(string propertyName)
        {
            return Filters.FirstOrDefault(x => x.Filter.PropertyName == propertyName);
        }

        #endregion GetFilter

        #region AddFilter

        /// <summary>
        /// Add a filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="title"></param>
        public void AddFilter(IFilter filter, string title)
        {
            Filters.Add(new FilterViewModel(filter, title, LogicalOperator.And, OperatorIsVisible));
        }

        /// <summary>
        /// Add a filter.
        /// </summary>
        /// <param name="filter"></param>
        private void AddFilter(Tuple<Func<IFilter>, string> filter)
        {
            AddFilter(filter.Item1.Invoke(), filter.Item2);
        }

        #endregion AddFilter

        #region RemoveFilter

        /// <summary>
        /// Remove a filter.
        /// </summary>
        /// <param name="filter"></param>
        private void RemoveFilter(IFilter filter)
        {
            Filters.Remove(Filters.FirstOrDefault(x => ReferenceEquals(x.Filter, filter)));
        }

        #endregion RemoveFilter

        #region Filter

        /// <summary>
        /// Occurs when filters change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FiltersCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (IsAutoFilter) FilterItems();
            RaisePropertyChanged(() => CountActiveFilters);
            ResetFiltersCommand?.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Update list where filters changed.
        /// </summary>
        protected virtual void FilterItems()
        {
            if (!OnInitializing && !IsRefreshDeferred)
            {
                FilterItems(Filters.Where(x => x.IsEnabled));
                OnFilterApplied();
            }
        }

        /// <summary>
        /// Update list where filters changed.
        /// </summary>
        protected abstract void FilterItems(IEnumerable<IFilterViewModel> filters);

        /// <summary>
        /// Add a allowed filter.
        /// </summary>
        /// <param name="createFilter"></param>
        /// <param name="title"></param>
        protected void AddAllowedFilter(string title, Func<IFilter> createFilter)
        {
            AllowedFilters.Add(new Tuple<Func<IFilter>, string>(createFilter, title));
        }

        /// <summary>
        /// Called when speed filter changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpeedFilter_FilterChanged(object sender, EventArgs e)
        {
            using (DeferFilter())
            {
                if (SpeedFilter == null) return;

                if (!SpeedFilter.Filter.IsEmpty())
                {
                    if (!Filters.Any(x => ReferenceEquals(x.Filter, SpeedFilter.Filter)))
                        Filters.Add(SpeedFilter);
                }
                else
                {
                    Filters.Remove(Filters.FirstOrDefault(x => ReferenceEquals(x.Filter, SpeedFilter.Filter)));
                }
            }
        }

        #endregion Filter

        #region PropertyChanged

        /// <summary>
        /// Occurs when Speed Filter change.
        /// </summary>
        protected void OnSpeedFilterChanged()
        {
            if (SpeedFilter != null)
            {
                SpeedFilter.PropertyChanged -= SpeedFilter_FilterChanged;
                SpeedFilter.PropertyChanged += SpeedFilter_FilterChanged;
            }
        }

        #endregion PropertyChanged

        #region Events

        /// <summary>
        /// Call when items are refreshed.
        /// </summary>
        /// <returns></returns>
        public event EventHandler FilterApplied;

        /// <summary>
        /// Call when items are refreshed.
        /// </summary>
        /// <returns></returns>
        private void OnFilterApplied()
        {
            FilterApplied?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Call when filter change.
        /// </summary>
        protected virtual void OnFilterChanged()
        {
            RaisePropertyChanged(nameof(FilteredItemsCount));
            RaisePropertyChanged(nameof(AllItemsCount));
            RaisePropertyChanged(nameof(IsFiltered));
            OnFilterApplied();
        }

        /// <summary>
        /// Occures when items change.
        /// </summary>
        protected virtual void OnItemsChanged()
        {
        }

        #endregion Events

        #region Defer

        private int _deferLevel;

        /// <summary>
        /// IsRefreshDeferred returns true if there
        /// is still an outstanding DeferRefresh in
        /// use.  If at all possible, derived classes
        /// should not call Refresh if IsRefreshDeferred
        /// is true.
        /// </summary>
        protected bool IsRefreshDeferred => _deferLevel > 0;

        /// <summary>
        /// Enter a Defer Cycle.
        /// Defer cycles are used to coalesce changes to the ICollectionView.
        /// </summary>
        public virtual IDisposable DeferFilter()
        {
            ++_deferLevel;
            return new DeferHelper(this);
        }

        private void EndDefer()
        {
            --_deferLevel;

            if (_deferLevel == 0 && IsAutoFilter)
            {
                FilterItems();
            }
        }

        private class DeferHelper : IDisposable
        {
            public DeferHelper(FiltersViewModel<T> collectionView)
            {
                _collectionView = collectionView;
            }

            public void Dispose()
            {
                _collectionView?.EndDefer();

                GC.SuppressFinalize(this);
            }

            private readonly FiltersViewModel<T> _collectionView;
        }

        #endregion Defer
    }
}