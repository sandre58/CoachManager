using My.CoachManager.Presentation.Core.Models;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;
using Prism.Commands;

namespace My.CoachManager.Presentation.Wpf.Core.ViewModels
{
    public class ApiPagingFiltersViewModel<T> : ApiListFiltersViewModel<T>, IPagingFiltersViewModel<T> where T : class, ISelectable, IEntityModel, IModifiable, IValidatable, new()
    {
        #region Members

        /// <summary>
        /// Gets or sets current page.
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// Gets pages number.
        /// </summary>
        public int PagesCount => (ItemsPerPage == 0)
            ? 1
            : (FilteredItemsCount + ItemsPerPage - 1)
              / ItemsPerPage;

        /// <summary>
        /// Gets or sets items per page.
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets Hide MoveToPreviousPage Command.
        /// </summary>
        public DelegateCommand MoveToPreviousPageCommand { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets Hide MoveToNextPage Command.
        /// </summary>
        public DelegateCommand MoveToNextPageCommand { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets Hide MoveToFirstPageCommand Command.
        /// </summary>
        public DelegateCommand MoveToFirstPageCommand { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets Hide MoveToLastPageCommand Command.
        /// </summary>
        public DelegateCommand MoveToLastPageCommand { get; set; }

        /// <summary>
        /// Gets or sets the Move to page command.
        /// </summary>
        public DelegateCommand<int?> MoveToPageCommand { get; set; }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ListParameters"/>.
        /// </summary>
        public ApiPagingFiltersViewModel(IListViewModel<T> list, int itemsPerPage, bool isAutoFilter, bool operatorIsVisible = false) : base(list, isAutoFilter, operatorIsVisible)
        {
            OnInitializing = true;
            CurrentPage = 1;
            ItemsPerPage = itemsPerPage;
            MoveToPreviousPageCommand = new DelegateCommand(MoveToPreviousPage, CanMoveToPreviousPage);
            MoveToNextPageCommand = new DelegateCommand(MoveToNextPage, CanMoveToNextPage);
            MoveToPreviousPageCommand = new DelegateCommand(MoveToPreviousPage, CanMoveToPreviousPage);
            MoveToNextPageCommand = new DelegateCommand(MoveToNextPage, CanMoveToNextPage);
            MoveToFirstPageCommand = new DelegateCommand(MoveToFirstPage, CanMoveToFirstPage);
            MoveToLastPageCommand = new DelegateCommand(MoveToLastPage, CanMoveToLastPage);
            MoveToPageCommand = new DelegateCommand<int?>(MoveToPage, CanMoveToPage);

            OnInitializing = false;
        }

        #endregion Constructors

        #region MoveToPage

        /// <summary>
        /// Move to page.
        /// </summary>
        public void MoveToPage(int? page)
        {
            if (page != null && page != CurrentPage)
            {
                CurrentPage = page.Value;
                FilterItems();
            }
        }

        /// <summary>
        /// Can Move to page.
        /// </summary>
        public bool CanMoveToPage(int? page)
        {
            return page >= 1 && page <= PagesCount;
        }

        #endregion MoveToPage

        #region MoveToPreviousPage

        /// <summary>
        /// Move to Previous page.
        /// </summary>
        public void MoveToPreviousPage()
        {
            MoveToPage(CurrentPage - 1);
        }

        /// <summary>
        /// Can Move to Previous page.
        /// </summary>
        public bool CanMoveToPreviousPage()
        {
            return CurrentPage > 1;
        }

        #endregion MoveToPreviousPage

        #region MoveToNextPage

        /// <summary>
        /// Move to next page.
        /// </summary>
        public void MoveToNextPage()
        {
            MoveToPage(CurrentPage + 1);
        }

        /// <summary>
        /// Can Move to next page.
        /// </summary>
        public bool CanMoveToNextPage()
        {
            return CurrentPage < PagesCount;
        }

        #endregion MoveToNextPage

        #region MoveToFirstPage

        /// <summary>
        /// Move to First page.
        /// </summary>
        public void MoveToFirstPage()
        {
            MoveToPage(1);
        }

        /// <summary>
        /// Can Move to First page.
        /// </summary>
        public bool CanMoveToFirstPage()
        {
            return CurrentPage != 1;
        }

        #endregion MoveToFirstPage

        #region MoveToLastPage

        /// <summary>
        /// Move to last page.
        /// </summary>
        public void MoveToLastPage()
        {
            MoveToPage(PagesCount);
        }

        /// <summary>
        /// Can Move to last page.
        /// </summary>
        public bool CanMoveToLastPage()
        {
            return CurrentPage != PagesCount;
        }

        #endregion MoveToLastPage

        #region PropertyChanged

        protected void OnItemsPerPageChanged()
        {
            if (CurrentPage == 1)
                FilterItems();
            else
                MoveToFirstPage();
        }

        /// <summary>
        /// Call when filter change.
        /// </summary>
        protected override void OnFilterChanged()
        {
            MoveToPageCommand?.RaiseCanExecuteChanged();
            MoveToNextPageCommand?.RaiseCanExecuteChanged();
            MoveToPreviousPageCommand?.RaiseCanExecuteChanged();
            MoveToFirstPageCommand?.RaiseCanExecuteChanged();
            MoveToLastPageCommand?.RaiseCanExecuteChanged();
            RaisePropertyChanged(nameof(PagesCount));
            RaisePropertyChanged(nameof(CurrentPage));
            base.OnFilterChanged();
        }

        #endregion PropertyChanged
    }
}