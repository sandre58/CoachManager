using System;
using System.Collections.Generic;

namespace My.CoachManager.Presentation.Mvvm.Core.Filters
{
    /// <summary>
    /// The contract for the IFilteredCollection
    /// </summary>
    public interface IFilteredCollection<out T> : IFilteredCollection, IEnumerable<T>
    {
    }

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
        /// Gets number of items to show.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets number of items no filtered.
        /// </summary>
        int AllItemsCount { get; }

        /// <summary>
        /// Gets number of items after filter.
        /// </summary>
        int FilteredItemsCount { get; }

        /// <summary>
        /// Removes a filter from the collection.
        /// </summary>
        /// <param name="filters">The filter.</param>
        void ChangeFilters(IEnumerable<Tuple<LogicalOperator, IFilter>> filters);

        event EventHandler Refreshed;
    }
}