using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using My.CoachManager.Presentation.Prism.Core.Filters;
using My.CoachManager.Presentation.Prism.Core.Models;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public class ListFiltersViewModel : ModelBase
    {
        #region Members

        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        public ObservableCollection<IFilterViewModel> Filters { get; set; }

        /// <summary>
        /// Gets or sets the  active filters.
        /// </summary>
        public int CountActiveFilters => Filters?.Count(x => x.IsEnabled) ?? 0;

        /// <summary>
        /// Gets or sets the filter visibility.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        public IStringFilterViewModel SpeedFilter { get; set; }

        /// <summary>
        /// Command to reset filters.
        /// </summary>
        public DelegateCommand ResetFiltersCommand { get; set; }

        /// <summary>
        /// Gets or sets Apply Filters Command.
        /// </summary>
        public DelegateCommand ApplyFiltersCommand { get; set; }

        /// <summary>
        /// Gets or sets Show Filters Command.
        /// </summary>
        public DelegateCommand ShowFiltersCommand { get; set; }

        /// <summary>
        /// Gets or sets Hide Filters Command.
        /// </summary>
        public DelegateCommand HideFiltersCommand { get; set; }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ListParametersViewModel"/>.
        /// </summary>
        public ListFiltersViewModel()
        {
            Filters = new ObservableCollection<IFilterViewModel>();

            ShowFiltersCommand = new DelegateCommand(ShowFilters);
            HideFiltersCommand = new DelegateCommand(HideFilters);
            ApplyFiltersCommand = new DelegateCommand(ApplyFilters);
            ResetFiltersCommand = new DelegateCommand(ResetFilters, CanResetFilters);
        }

        #endregion Constructors

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

        #region Filter

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
            FilteredItems.ChangeFilters(filters.Select(x => new Tuple<LogicalOperator, IFilter>(x.Operator, x.Filter)));
            OnFiltersApplied();
        }

        /// <summary>
        /// Called when filters change.
        /// </summary>
        protected virtual void OnFiltersApplied()
        {
            RaisePropertyChanged(() => CountActiveFilters);
            ResetFiltersCommand?.RaiseCanExecuteChanged();
        }

        #endregion Filter
    }
}