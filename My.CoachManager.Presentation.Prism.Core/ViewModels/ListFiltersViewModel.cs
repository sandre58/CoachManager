using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.Models;
using My.CoachManager.Presentation.Prism.Core.Models.Filters;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public class ListFiltersViewModel : ModelBase, IListFiltersViewModel
    {
        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        public IFilteredCollection Items { get; set; }

        /// <summary>
        /// Gets the allowed filters.
        /// </summary>
        public IList<Tuple<Func<IFilter>, string>> AllowedFilters { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        public ItemsObservableCollection<IFilterViewModel> Filters { get; set; }

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

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ListParametersViewModel"/>.
        /// </summary>
        public ListFiltersViewModel()
        {
            Filters = new ItemsObservableCollection<IFilterViewModel>();
            Filters.CollectionChanged += FiltersCollectionChanged;
            AllowedFilters = new List<Tuple<Func<IFilter>, string>>();

            ShowFiltersCommand = new DelegateCommand(ShowFilters);
            HideFiltersCommand = new DelegateCommand(HideFilters);
            ApplyFiltersCommand = new DelegateCommand(ApplyFilters);
            ResetFiltersCommand = new DelegateCommand(ResetFilters, CanResetFilters);
            AddFilterCommand = new DelegateCommand<Tuple<Func<IFilter>, string>>(AddFilter);
            RemoveFilterCommand = new DelegateCommand<IFilter>(RemoveFilter);

            KeyboardManager.RegisterWorkspaceShortcut(new KeyBinding(ShowFiltersCommand, Key.F, ModifierKeys.Control));
        }

        #endregion Constructors

        #region Reset Filters

        /// <summary>
        /// Reset filters.
        /// </summary>
        private void ResetFilters()
        {
                Filters.Clear();
                FilterItems();
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
        private void ApplyFilters()
        {
            FilterItems(Filters.Where(x => x.IsEnabled));
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

        #region AddFilter

        /// <summary>
        /// Add a filter.
        /// </summary>
        /// <param name="filter"></param>
        private void AddFilter(Tuple<Func<IFilter>, string> filter)
        {
           Filters.Add(new FilterViewModel(filter.Item1.Invoke(), filter.Item2));
        }

        #endregion

        #region RemoveFilter

        /// <summary>
        /// Remove a filter.
        /// </summary>
        /// <param name="filter"></param>
        private void RemoveFilter(IFilter filter)
        {
            Filters.Remove(Filters.FirstOrDefault(x => ReferenceEquals(x.Filter, filter)));
        }

        #endregion

        #region Filter

        /// <summary>
        /// Occurs when filters change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FiltersCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            FilterItems();
            RaisePropertyChanged(() => CountActiveFilters);
            ResetFiltersCommand?.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Update list where filters changed.
        /// </summary>
        protected virtual void FilterItems()
        {
            FilterItems(Filters.Where(x => x.IsEnabled));
        }

        /// <summary>
        /// Update list where filters changed.
        /// </summary>
        protected virtual void FilterItems(IEnumerable<IFilterViewModel> filters)
        {
            Items.ChangeFilters(filters.Select(x => new Tuple<LogicalOperator, IFilter>(x.Operator, x.Filter)));
        }

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
            if (SpeedFilter == null) return;

            if (!SpeedFilter.Filter.IsEmpty())
            {
                if(!Filters.Any(x => x.Filter.Equals(SpeedFilter.Filter)))
                Filters.Add(SpeedFilter);
            }
            else
            {
                Filters.Remove(Filters.FirstOrDefault(x => x.Filter.Equals(SpeedFilter.Filter)));
            }

            FilterItems();
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

        #endregion
    }
}