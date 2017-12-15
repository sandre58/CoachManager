using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Prism.Tests
{
    /// <summary>
    /// View on top of a collection of INotifyPropertyChanged elements.
    /// </summary>
    public class FilteredCollectionView : ListCollectionView, IFilteredCollection
    {
        /// <summary>
        /// Instance of the registered filters
        /// </summary>
        private readonly Dictionary<string, IFilter> _filters = new Dictionary<string, IFilter>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FilteredCollectionView"/> class.
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
                foreach (IFilter filter in _filters.Values)
                {
                    if (!filter.IsMatch(item))
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

        #endregion ICollectionView Members

        #region IFilteredCollection Members

        /// <summary>
        /// Registers a filter withe the collection view.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public void AddFilter(IFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }
            if (filter.PropertyInfo == null)
            {
                throw new ArgumentException("Invalid filter, missing property info.");
            }
            if (!_filters.ContainsKey(filter.PropertyInfo.Name))
            {
                _filters[filter.PropertyInfo.Name] = filter;
                filter.FilteringChanged += OnFilterChanged;

                UpdateFilter();
            }
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
            if (filter.PropertyInfo == null)
            {
                throw new ArgumentException("Invalid filter, missing property info.");
            }

            if (_filters.ContainsKey(filter.PropertyInfo.Name))
            {
                _filters.Remove(filter.PropertyInfo.Name);
                filter.FilteringChanged -= OnFilterChanged;

                UpdateFilter();
            }
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
                    return _filters.Values.All(f => f.IsMatch(o));
                };

                Refresh();
            }
        }

        #endregion IFilteredCollection Members
    }
}