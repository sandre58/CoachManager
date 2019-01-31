using System;
using System.Collections.Generic;
using System.ComponentModel;
using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Presentation.Core.Models.Filters
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
        /// <param name="logicalOperator"></param>
        void AddFilter(IFilter filter, LogicalOperator logicalOperator);

        /// <summary>
        /// Removes the filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        void RemoveFilter(IFilter filter);

        /// <summary>
        /// Gets number of items
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Removes a filter from the collection.
        /// </summary>
        /// <param name="filters">The filter.</param>
        void ChangeFilters(IEnumerable<Tuple<LogicalOperator, IFilter>> filters);
    }
}