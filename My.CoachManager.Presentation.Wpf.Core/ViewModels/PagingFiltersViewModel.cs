using My.CoachManager.Presentation.Wpf.Core.Filters;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;
using Prism.Commands;
using System.Collections.Generic;
using System.Windows;

namespace My.CoachManager.Presentation.Wpf.Core.ViewModels
{
    public class PagingFiltersViewModel<T> : ListFiltersViewModel<T>, IPagingFiltersViewModel<T>
    {
        #region Fields

        private readonly int _defaultItemsPerPage;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        protected IPagingCollection<T> PagingItems => Items as IPagingCollection<T>;

        /// <summary>
        /// Gets or sets current page.
        /// </summary>
        public int CurrentPage => PagingItems?.CurrentPage ?? 1;

        /// <summary>
        /// Gets or sets page count.
        /// </summary>
        public int PagesCount => PagingItems?.PagesCount ?? 1;

        /// <summary>
        /// Gets or sets items per page.
        /// </summary>
        public int ItemsPerPage
        {
            get => PagingItems?.ItemsPerPage ?? 0;
            set
            {
                if (PagingItems != null)
                {
                    PagingItems.ItemsPerPage = value;
                }
            }
        }

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
        public PagingFiltersViewModel(int itemsPerPage, bool isAutoFilter, bool operatorIsVisible = true) : base(isAutoFilter, operatorIsVisible)
        {
            OnInitializing = true;
            _defaultItemsPerPage = itemsPerPage;
            MoveToPreviousPageCommand = new DelegateCommand(MoveToPreviousPage, CanMoveToPreviousPage);
            MoveToNextPageCommand = new DelegateCommand(MoveToNextPage, CanMoveToNextPage);
            MoveToPreviousPageCommand = new DelegateCommand(MoveToPreviousPage, CanMoveToPreviousPage);
            MoveToNextPageCommand = new DelegateCommand(MoveToNextPage, CanMoveToNextPage);
            MoveToFirstPageCommand = new DelegateCommand(MoveToFirstPage, CanMoveToFirstPage);
            MoveToLastPageCommand = new DelegateCommand(MoveToLastPage, CanMoveToLastPage);
            MoveToPageCommand = new DelegateCommand<int?>(MoveToPage, CanMoveToPage);

            OnInitializing = false;
        }

        /// <summary>
        /// Update collection.
        /// </summary>
        public override void UpdateCollection(IEnumerable<T> collection)
        {
            Items = new PagingCollectionView<T>(collection, PagingItems?.ItemsPerPage ?? _defaultItemsPerPage);
            Application.Current.Dispatcher.Invoke(ApplyFilters);
            ApplyFilters();
            RaisePropertyChanged(nameof(ItemsPerPage));
        }

        #endregion Constructors

        #region MoveToPage

        /// <summary>
        /// Move to page.
        /// </summary>
        public void MoveToPage(int? page)
        {
            if (page != null) PagingItems?.MoveToPage(page.Value);
        }

        /// <summary>
        /// Can Move to page.
        /// </summary>
        public bool CanMoveToPage(int? page)
        {
            return page != null && (PagingItems?.CanMoveToPage(page.Value) ?? false);
        }

        #endregion MoveToPage

        #region MoveToPreviousPage

        /// <summary>
        /// Move to Previous page.
        /// </summary>
        public void MoveToPreviousPage()
        {
            PagingItems?.MoveToPreviousPage();
        }

        /// <summary>
        /// Can Move to Previous page.
        /// </summary>
        public bool CanMoveToPreviousPage()
        {
            return PagingItems?.CanMoveToPreviousPage() ?? false;
        }

        #endregion MoveToPreviousPage

        #region MoveToNextPage

        /// <summary>
        /// Move to next page.
        /// </summary>
        public void MoveToNextPage()
        {
            PagingItems?.MoveToNextPage();
        }

        /// <summary>
        /// Can Move to next page.
        /// </summary>
        public bool CanMoveToNextPage()
        {
            return PagingItems?.CanMoveToNextPage() ?? false;
        }

        #endregion MoveToNextPage

        #region MoveToFirstPage

        /// <summary>
        /// Move to First page.
        /// </summary>
        public void MoveToFirstPage()
        {
            PagingItems?.MoveToFirstPage();
        }

        /// <summary>
        /// Can Move to First page.
        /// </summary>
        public bool CanMoveToFirstPage()
        {
            return PagingItems?.CanMoveToFirstPage() ?? false;
        }

        #endregion MoveToFirstPage

        #region MoveToLastPage

        /// <summary>
        /// Move to last page.
        /// </summary>
        public void MoveToLastPage()
        {
            PagingItems?.MoveToLastPage();
        }

        /// <summary>
        /// Can Move to last page.
        /// </summary>
        public bool CanMoveToLastPage()
        {
            return PagingItems?.CanMoveToLastPage() ?? false;
        }

        #endregion MoveToLastPage

        #region PropertyChanged

        protected override void OnFilterChanged()
        {
            MoveToPageCommand.RaiseCanExecuteChanged();
            MoveToNextPageCommand.RaiseCanExecuteChanged();
            MoveToPreviousPageCommand.RaiseCanExecuteChanged();
            MoveToFirstPageCommand.RaiseCanExecuteChanged();
            MoveToLastPageCommand.RaiseCanExecuteChanged();
            RaisePropertyChanged(nameof(PagesCount));
            RaisePropertyChanged(nameof(CurrentPage));
            base.OnFilterChanged();
        }

        protected void OnItemsPerPageChanged()
        {
            PagingItems?.Refresh();
        }

        #endregion PropertyChanged
    }
}