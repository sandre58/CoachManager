using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
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
                        var i = item as INotifyPropertyChanged;
                        if (i != null)
                        {
                            i.PropertyChanged += OnItemPropertyChanged;
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

        /// <summary>
        /// Checks if the item is passing the registered filters.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public override bool PassesFilter(object item)
        {
            bool result = true;
            if (CanFilter && (_filters.Count > 0))
            {
                foreach (var filter in _filters)
                {
                    if (!filter.Item2.IsMatch(item))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
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
        public int CountFilters
        {
            get { return _filters.Count; }
        }

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
                throw new ArgumentNullException("filter");
            }

            _filters.Add(new Tuple<LogicalOperator, IFilter>(logicalOperator, filter));

            OnFilterChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called by the IFilter whenever it changes. Will cause a collection refresh.
        /// </summary>
        /// <param name="sender">The IFIlter registered with the collection.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnFilterChanged(object sender, EventArgs e)
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
                throw new ArgumentNullException("filter");
            }

            _filters.Remove(_filters.FirstOrDefault(x => ReferenceEquals(x.Item2, filter)));

            OnFilterChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Removes a filter from the collection.
        /// </summary>
        /// <param name="filters">The filter.</param>
        public void ChangeFilters(IEnumerable<Tuple<LogicalOperator, IFilter>> filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException($"filter");
            }

            _filters.Clear();

            foreach (var filter in filters)
            {
                _filters.Add(filter);
            }

            OnFilterChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Updates filter and refresh collection.
        /// </summary>
        protected void UpdateFilter()
        {
            if (CanFilter)
            {
                Filter = delegate (object o)
                {
                    if (_filters.Count == 0)
                        return true;

                    var res = _filters.First().Item2.IsMatch(o);

                    if (_filters.Count > 1)
                    {
                        for (int i = 1; i < _filters.Count; i++)
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