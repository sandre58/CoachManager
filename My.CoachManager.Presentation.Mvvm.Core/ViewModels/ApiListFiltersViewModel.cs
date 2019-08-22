using System.Collections.Generic;
using My.CoachManager.Presentation.Mvvm.Core.ViewModels.Base;
using My.CoachManager.Presentation.Mvvm.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Mvvm.Core.ViewModels
{
    public class ApiListFiltersViewModel<T> : FiltersViewModel<T> where T : class, ISelectable, IEntityModel, IModifiable, IValidatable, new()
    {
        private readonly IListViewModel<T> _list;

        #region Members

        /// <summary>
        /// Gets or sets filtered items count.
        /// </summary>
        public override int FilteredItemsCount => _list.Count;

        /// <summary>
        /// Gets or sets all items count.
        /// </summary>
        public override int AllItemsCount => _list.AllItemsCount;

        /// <summary>
        /// Gets or sets sort description.
        /// </summary>
        public SortDescription SortDescription { get; set; }

        /// <summary>
        /// Gets or sets command.
        /// </summary>
        public DelegateCommand<SortDescription?> SortCommand { get; }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ApiListFiltersViewModel{T}"/>.
        /// </summary>
        public ApiListFiltersViewModel(IListViewModel<T> list, bool isAutoFilter, bool operatorIsVisible = false) : base(isAutoFilter, operatorIsVisible)
        {
            OnInitializing = true;
            SortCommand = new DelegateCommand<SortDescription?>(Sort);
            _list = list;
            _list.Refreshed += (sender, args) => { Application.Current.Dispatcher.Invoke(OnFilterChanged); };

            OnInitializing = false;
        }

        #endregion Constructors

        #region Filter

        /// <summary>
        /// Update list where filters changed.
        /// </summary>
        protected override void FilterItems(IEnumerable<IFilterViewModel> filters)
        {
            _list.Refresh();
        }

        #endregion Filter

        #region Sort

        /// <summary>
        /// Sort.
        /// </summary>
        /// <param name="sortDescription"></param>
        protected virtual void Sort(SortDescription? sortDescription)
        {
            if (sortDescription != null)
            {
                SortDescription = sortDescription.Value;
                _list?.Refresh();
            }
        }

        #endregion Sort
    }
}