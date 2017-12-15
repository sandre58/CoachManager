using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    /// The contract for the IFilteredCollection
    /// </summary>
    public interface IFilteredCollection : ICollectionView
    {
        /// <summary>
        /// Adds the filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        void AddFilter(IFilter filter);

        /// <summary>
        /// Removes the filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        void RemoveFilter(IFilter filter);
    }
}