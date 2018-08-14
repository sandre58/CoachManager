using System;
using System.Collections.Generic;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.Presentation.Prism.Core.Models.Filters;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces
{

    public interface IListFiltersViewModel
    {
        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        IFilteredCollection Items { get; set; }

        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        ItemsObservableCollection<IFilterViewModel> Filters { get; set; }

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
        DelegateCommand ResetFiltersCommand { get; set; }

        /// <summary>
        /// Gets or sets Apply Filters Command.
        /// </summary>
        DelegateCommand ApplyFiltersCommand { get; set; }

        /// <summary>
        /// Gets or sets Show Filters Command.
        /// </summary>
        DelegateCommand ShowFiltersCommand { get; set; }

        /// <summary>
        /// Gets or sets Hide Filters Command.
        /// </summary>
        DelegateCommand HideFiltersCommand { get; set; }

        /// <summary>
        /// Gets or sets the Add command.
        /// </summary>
        DelegateCommand<Tuple<Func<IFilter>, string>> AddFilterCommand { get; set; }

        /// <summary>
        /// Gets or sets the Remove command.
        /// </summary>
        DelegateCommand<IFilter> RemoveFilterCommand { get; set; }
    }
}