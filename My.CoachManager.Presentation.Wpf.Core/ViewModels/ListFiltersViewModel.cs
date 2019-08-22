using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.Presentation.Core.Models.Filters;
using My.CoachManager.Presentation.Wpf.Core.Filters;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Base;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace My.CoachManager.Presentation.Wpf.Core.ViewModels
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