using System;
using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Presentation.Mvvm.Core.Filters;
using My.CoachManager.Presentation.Mvvm.Core.ViewModels.Base;
using My.CoachManager.Presentation.Mvvm.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Mvvm.Core.ViewModels
{
    public class ListFiltersViewModel<T> : FiltersViewModel<T>
    {
        #region Members

        public IFilteredCollection<T> FilteredItems => Items as IFilteredCollection<T>;

        /// <summary>
        /// Gets or sets filtered items count.
        /// </summary>
        public override int FilteredItemsCount => FilteredItems?.FilteredItemsCount ?? 0;

        /// <summary>
        /// Gets or sets all items count.
        /// </summary>
        public override int AllItemsCount => FilteredItems?.AllItemsCount ?? 0;

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ListParameters"/>.
        /// </summary>
        public ListFiltersViewModel(bool isAutoFilter, bool operatorIsVisible = true) : base(isAutoFilter, operatorIsVisible)
        {
        }

        /// <summary>
        /// Update collection.
        /// </summary>
        public override void UpdateCollection(IEnumerable<T> collection)
        {
            Items = new FilteredCollectionView<T>(collection);
            Application.Current.Dispatcher.Invoke(ApplyFilters);
        }

        #endregion Constructors

        #region Filter

        /// <summary>
        /// Update list where filters changed.
        /// </summary>
        protected override void FilterItems(IEnumerable<IFilterViewModel> filters)
        {
            FilteredItems?.ChangeFilters(filters.Select(x => new Tuple<LogicalOperator, IFilter>(x.Operator, x.Filter)));
        }

        #endregion Filter

        #region PropertyChanged

        /// <summary>
        /// Occurs when Items change.
        /// </summary>
        protected override void OnItemsChanged()
        {
            if (FilteredItems != null)
            {
                FilteredItems.Refreshed += (sender, args) =>
                {
                    OnFilterChanged();
                };
            }
        }

        #endregion PropertyChanged
    }
}