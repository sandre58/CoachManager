using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public interface IFiltersViewModel
    {
        /// <summary>
        /// Gets or sets a methods that create a filter.
        /// </summary>
        Func<string, IFilterViewModel> CreateFilter { get; set; }

        /// <summary>
        /// Gets the allowed filters.
        /// </summary>
        Dictionary<string, string> AllowedFilters { get; }

        /// <summary>
        /// Gets the update on live.
        /// </summary>
        bool UpdateOnLive { get; set; }

        /// <summary>
        /// Gets or sets the filters.
        /// </summary>
        ObservableCollection<FilterViewModel> Filters { get; set; }

        /// <summary>
        /// When Filters changed.
        /// </summary>
        event EventHandler FiltersChanged;
    }
}