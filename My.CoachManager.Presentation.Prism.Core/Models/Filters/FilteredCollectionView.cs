﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Presentation.Prism.Core.Models.Filters
{
    /// <inheritdoc cref="ListCollectionView" />
    /// <summary>
    /// View on top of a collection of INotifyPropertyChanged elements.
    /// </summary>
    public class FilteredCollectionView<T> : ListCollectionView, IFilteredCollection, IEnumerable<T>
    {
        /// <summary>
        /// Instance of the registered filters
        /// </summary>
        private readonly IList<Tuple<LogicalOperator, IFilter>> _filters = new List<Tuple<LogicalOperator, IFilter>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FilteredCollectionView{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public FilteredCollectionView(IList collection) : base(collection)
        {
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            base.OnCollectionChanged(args);

            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in args.NewItems)
                    {
                        if (item is INotifyPropertyChanged i)
                        {
                            i.PropertyChanged += OnItemPropertyChanged;
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in args.OldItems)
                    {
                        if (item is INotifyPropertyChanged i)
                        {
                            i.PropertyChanged -= OnItemPropertyChanged;
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Called whenever one of the properties of an item in the source collection changes
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Refresh();
        }

        #region ICollectionView Members

        /// <summary>
        /// Gets or sets a callback that is used to determine whether an item is appropriate for inclusion in the view.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// A method that is used to determine whether an item is appropriate for inclusion in the view.
        /// </returns>
        Predicate<object> ICollectionView.Filter
        {
            get { return base.Filter; }
            set
            {
                //throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets the count filter.
        /// </summary>
        public int CountFilters => _filters.Count;

        #endregion ICollectionView Members

        #region IFilteredCollection Members

        /// <summary>
        /// Registers a filter withe the collection view.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="logicalOperator"></param>
        public void AddFilter(IFilter filter, LogicalOperator logicalOperator = LogicalOperator.And)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            _filters.Add(new Tuple<LogicalOperator, IFilter>(logicalOperator, filter));

            OnFilterChanged();
        }

        /// <summary>
        /// Called by the IFilter whenever it changes. Will cause a collection refresh.
        /// </summary>
        private void OnFilterChanged()
        {
            UpdateFilter();
        }

        /// <summary>
        /// Removes a filter from the collection.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public void RemoveFilter(IFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            _filters.Remove(_filters.FirstOrDefault(x => ReferenceEquals(x.Item2, filter)));

            OnFilterChanged();
        }

        /// <summary>
        /// Removes a filter from the collection.
        /// </summary>
        /// <param name="filters">The filter.</param>
        public void ChangeFilters(IEnumerable<Tuple<LogicalOperator, IFilter>> filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            _filters.Clear();

            foreach (var filter in filters)
            {
                _filters.Add(filter);
            }

            OnFilterChanged();
        }

        /// <summary>
        /// Updates filter and refresh collection.
        /// </summary>
        private void UpdateFilter()
        {
            if (CanFilter)
            {
                Filter = delegate (object o)
                {
                    if (_filters.Count == 0)
                        return true;

                    var res = _filters.First().Item2.IsMatch(o);

                    if (_filters.Count <= 1) return res;

                    for (var i = 1; i < _filters.Count; i++)
                    {
                        var filter = _filters[i];

                        switch (filter.Item1)
                        {
                            case LogicalOperator.Or:
                                res = res || filter.Item2.IsMatch(o);
                                break;

                            default:
                                res = res && filter.Item2.IsMatch(o);
                                break;
                        }
                    }

                    return res;
                };

                Refresh();
            }
        }

        #endregion IFilteredCollection Members

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)base.GetEnumerator();
        }
    }
}