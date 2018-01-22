using System;
using System.Collections.Generic;
using My.CoachManager.Presentation.Prism.Core.Filters;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public interface IFiltersViewModel
    {
        /// <summary>
        /// When Filters changed.
        /// </summary>
        event EventHandler FiltersChanged;

        /// <summary>
        /// Gets or sets filters number.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets or sets filters.
        /// </summary>
        IEnumerable<IFilter> AvailableFilters { get; }
    }
}