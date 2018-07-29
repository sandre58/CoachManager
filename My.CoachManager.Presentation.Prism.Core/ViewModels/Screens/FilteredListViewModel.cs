using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Filters;
using My.CoachManager.Presentation.Prism.Core.Models;
using My.CoachManager.Presentation.Prism.Core.Views;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class FilteredListViewModel<TEntityViewModel> : ListViewModel<TEntityViewModel>, IFilteredListViewModel
        where TEntityViewModel : class, IEntityModel, IModifiable, IValidatable, INotifyPropertyChanged, new()
    {
        #region Fields

        private ObservableCollection<FilterViewModel> _filters;
        private IFilterViewModel _speedFilter;
        private FiltersViewModel _filtersViewModel;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        public ObservableCollection<FilterViewModel> Filters
        {
            get { return _filters; }
            set { SetProperty(ref _filters, value); }
        }

        /// <summary>
        /// Gets or sets the  active filters.
        /// </summary>
        public int CountActiveFilters
        {
            get { return Filters != null ? Filters.Count(x => x.IsEnabled) : 0; }
        }

        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        public IFilterViewModel SpeedFilter
        {
            get { return _speedFilter; }
            set
            {
                SetProperty(ref _speedFilter, value);
            }
        }

        /// <summary>
        /// Command to reset filters.
        /// </summary>
        public DelegateCommand ResetFiltersCommand { get; set; }

        /// <summary>
        /// Gets or sets Show Filters Command.
        /// </summary>
        public DelegateCommand ShowFiltersCommand { get; set; }

        #endregion Members

        #region Methods

        #region Initialization

        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            ShowFiltersCommand = new DelegateCommand(ShowFilter);
            ResetFiltersCommand = new DelegateCommand(ResetFilters, CanResetFilters);
        }

        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            _filtersViewModel = new FiltersViewModel();
            Filters = new ObservableCollection<FilterViewModel>();

            _filtersViewModel.CreateFilter = CreateFilter;
            _filtersViewModel.FiltersChanged += OnFiltersViewChanged;
        }

        #endregion Initialization

        #region Show Filters

        /// <summary>
        /// Show Filters dialog.
        /// </summary>
        private void ShowFilter()
        {
            DialogService.ShowWorkspaceDialog(typeof(IFiltersView), _filtersViewModel, before =>
                {
                    if (Filters != null)
                    {
                        var model = before.Context as IFiltersViewModel;
                        if (model != null)
                        {
                            model.Filters = Filters.Clone();
                        }
                    }
                },
                after =>
                {
                    var model = after.Context as IFiltersViewModel;
                    if (model != null)
                    {
                        if (after.Result == DialogResult.Ok)
                        {
                            Filters = model.Filters;
                        }

                        FilterItems();
                    }
                });
        }

        #endregion Show Filters

        #region Reset Filters

        /// <summary>
        /// Reset filters.
        /// </summary>
        private void ResetFilters()
        {
            if (CanResetFilters())
            {
                Filters.Clear();
                FilterItems();
            }
        }

        /// <summary>
        /// Can reset filters ?
        /// </summary>
        /// <returns></returns>
        private bool CanResetFilters()
        {
            return CountActiveFilters > 0;
        }

        #endregion Reset Filters

        #region Filter Items

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
            var filterViewModels = filters as IList<IFilterViewModel> ?? filters.ToList();
            FilteredItems.ChangeFilters(filterViewModels.Select(x => new Tuple<LogicalOperator, IFilter>(x.Operator, x.Filter)));
            OnFiltersChanged();
        }

        /// <summary>
        /// Create a filter.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected virtual IFilterViewModel CreateFilter(string propertyName)
        {
            return new FilterViewModel(new StringFilter(propertyName), propertyName);
        }

        /// <summary>
        /// Add a allowed filter.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="title"></param>
        protected void AddAllowedFilter(string propertyName, string title)
        {
            if (_filtersViewModel != null)
            {
                _filtersViewModel.AllowedFilters.Add(propertyName, title);
            }
        }

        #endregion Filter Items

        /// <summary>
        /// Called when filters change.
        /// </summary>
        protected virtual void OnSpeedFilterChanged()
        {
            if (_speedFilter != null)
            {
                _speedFilter.PropertyChanged -= SpeedFilter_FilterChanged;
                _speedFilter.PropertyChanged += SpeedFilter_FilterChanged;
            }
        }

        /// <summary>
        /// Called when filters change.
        /// </summary>
        protected virtual void OnFiltersChanged()
        {
            RaisePropertyChanged(() => CountActiveFilters);
            if (ResetFiltersCommand != null) ResetFiltersCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Call when Filters View Change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFiltersViewChanged(object sender, EventArgs e)
        {
            var filters = sender as IFiltersViewModel;
            if (filters != null && filters.UpdateOnLive)
            {
                FilterItems(filters.Filters.Where(x => x.IsEnabled));
            }
        }

        /// <summary>
        /// Called when speed filter changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpeedFilter_FilterChanged(object sender, EventArgs e)
        {
            if (SpeedFilter != null)
            {
                Filters.Remove(Filters.FirstOrDefault(x => x.Filter.Equals(SpeedFilter.Filter)));

                if (!SpeedFilter.Filter.IsEmpty())
                {
                    Filters.Add((FilterViewModel)SpeedFilter);
                }

                FilterItems();
            }
        }

        #endregion Methods
    }
}