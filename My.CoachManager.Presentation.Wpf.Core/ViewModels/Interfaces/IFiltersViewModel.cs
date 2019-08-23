using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.Presentation.Core.Models.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;

namespace My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces
{
    public interface IFiltersViewModel<T> : IFiltersViewModel
    {
        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        new IEnumerable<T> Items { get; }

        /// <summary>
        /// Update collection.
        /// </summary>
        void UpdateCollection(IEnumerable<T> collection);
    }

    public interface IFiltersViewModel
    {
        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        IEnumerable Items { get; }

        /// <summary>
        /// Gets or sets all items count.
        /// </summary>
        int FilteredItemsCount { get; }

        /// <summary>
        /// Gets or sets all items count.
        /// </summary>
        int AllItemsCount { get; }

        /// <summary>
        /// Gets is filtered.
        /// </summary>
        bool IsFiltered { get; }

        /// <summary>
        /// Gets or sets auto filter value.
        /// </summary>
        bool IsAutoFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        ObservableItemsCollection<IFilterViewModel> Filters { get; }

        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        ObservableItemsCollection<IFilterViewModel> DefaultFilters { get; }

        /// <summary>
        /// Gets the allowed filters.
        /// </summary>
        IList<Tuple<Func<IFilter>, string>> AllowedFilters { get; set; }

        /// <summary>
        /// Gets or sets the  active filters.
        /// </summary>
        int CountActiveFilters { get; }

        /// <summary>
        /// Gets or sets the filter visibility.
        /// </summary>
        bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        IFilterViewModel SpeedFilter { get; set; }

        /// <summary>
        /// Command to reset filters.
        /// </summary>
        RelayCommand ResetFiltersCommand { get; set; }

        /// <summary>
        /// Gets or sets Apply Filters Command.
        /// </summary>
        RelayCommand ApplyFiltersCommand { get; set; }

        /// <summary>
        /// Gets or sets Show Filters Command.
        /// </summary>
        RelayCommand ShowFiltersCommand { get; set; }

        /// <summary>
        /// Gets or sets Hide Filters Command.
        /// </summary>
        RelayCommand HideFiltersCommand { get; set; }

        /// <summary>
        /// Gets or sets the Add command.
        /// </summary>
        RelayCommand<Tuple<Func<IFilter>, string>> AddFilterCommand { get; set; }

        /// <summary>
        /// Gets or sets the Remove command.
        /// </summary>
        RelayCommand<IFilter> RemoveFilterCommand { get; set; }

        /// <summary>
        /// Reset filters.
        /// </summary>
        void ResetFilters();

        /// <summary>
        /// Apply Filters.
        /// </summary>
        void ApplyFilters();

        /// <summary>
        /// Get a filter.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IFilterViewModel GetFilter(string propertyName);

        /// <summary>
        /// Add a filter.
        /// </summary>
        void AddFilter(IFilter filter, string title);

        /// <summary>
        /// Calls when items are filtered.
        /// </summary>
        event EventHandler FilterApplied;
    }
}