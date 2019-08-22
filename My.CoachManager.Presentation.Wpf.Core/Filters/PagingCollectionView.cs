using System.Collections.Generic;
using System.Linq;

namespace My.CoachManager.Presentation.Wpf.Core.Filters
{
    /// <summary>
    /// View on top of a collection of INotifyPropertyChanged elements.
    /// </summary>
    public class PagingCollectionView<T> : FilteredCollectionView<T>, IPagingCollection<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilteredCollectionView{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="itemsPerPage"></param>
        public PagingCollectionView(IEnumerable<T> collection, int itemsPerPage) : base(collection)
        {
            ItemsPerPage = itemsPerPage;
            CurrentPage = 1;
        }

        #region Members

        /// <summary>
        /// Gets items number.
        /// </summary>
        public override int Count
        {
            get
            {
                if (ItemsPerPage == 0) return base.Count;
                if (InternalCount == 0) return 0;
                if (CurrentPage < PagesCount) return ItemsPerPage;

                var itemsLeft = InternalCount % ItemsPerPage;
                if (0 == itemsLeft)
                {
                    return ItemsPerPage; // exactly itemsPerPage left
                }

                // return the remaining items
                return itemsLeft;
            }
        }

        /// <summary>
        /// Gets or sets current page.
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// gets or set items count by page.
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// Gets filtered collection.
        /// </summary>
        public IEnumerable<T> FilteredCollection => SourceCollection.OfType<T>().Where(o => Filter?.Invoke(o) ?? true);

        /// <summary>
        /// Gets pages number.
        /// </summary>
        public int PagesCount => (ItemsPerPage == 0)
            ? 1
            : (InternalCount + ItemsPerPage - 1)
              / ItemsPerPage;

        /// <summary>
        /// Gets items count.
        /// </summary>
        public int ItemsCount => InternalCount;

        /// <summary>
        /// Gets start index.
        /// </summary>
        private int StartIndex => (CurrentPage - 1) * ItemsPerPage;

        #endregion Members

        #region Methods

        /// <summary>
        /// CheckCurrentPage.
        /// </summary>
        private void CheckCurrentPage()
        {
            var newPageCount = ItemsPerPage == 0 ? 1 : (FilteredCollection.Count() + ItemsPerPage - 1) / ItemsPerPage;
            if (CurrentPage > newPageCount)
            {
                CurrentPage = 1;
            }
        }

        /// <summary>
        /// Gets item to specified position in view.
        /// </summary>
        public override object GetItemAt(int index)
        {
            if (ItemsPerPage == 0) return base.GetItemAt(index);

            var offset = index % ItemsPerPage;
            return base.GetItemAt(StartIndex + offset);
        }

        /// <summary>
        /// Move to next page.
        /// </summary>
        public void MoveToNextPage()
        {
            if (CanMoveToNextPage())
            {
                CurrentPage += 1;
                Refresh();
            }
        }

        /// <summary>
        /// Can Move to next page ?
        /// </summary>
        public bool CanMoveToNextPage()
        {
            return CurrentPage < PagesCount;
        }

        /// <summary>
        /// Move to previous page.
        /// </summary>
        public void MoveToPreviousPage()
        {
            if (CanMoveToPreviousPage())
            {
                CurrentPage -= 1;
                Refresh();
            }
        }

        /// <summary>
        /// Can Move to previous page ?
        /// </summary>
        public bool CanMoveToPreviousPage()
        {
            return CurrentPage > 1;
        }

        /// <summary>
        /// Move to last page.
        /// </summary>
        public void MoveToLastPage()
        {
            if (CanMoveToLastPage())
            {
                MoveToPage(PagesCount);
            }
        }

        /// <summary>
        /// Can Move to last page ?
        /// </summary>
        public bool CanMoveToLastPage()
        {
            return CurrentPage != PagesCount;
        }

        /// <summary>
        /// Move to first page.
        /// </summary>
        public void MoveToFirstPage()
        {
            if (CanMoveToFirstPage())
            {
                MoveToPage(1);
            }
        }

        /// <summary>
        /// Can Move to first page ?
        /// </summary>
        public bool CanMoveToFirstPage()
        {
            return CurrentPage != 1;
        }

        /// <summary>
        /// Move to page.
        /// </summary>
        public void MoveToPage(int page)
        {
            if (CanMoveToPage(page))
            {
                CurrentPage = page;
                Refresh();
            }
        }

        /// <summary>
        /// Can Move to page ?
        /// </summary>
        public bool CanMoveToPage(int page)
        {
            return page >= 1 && page <= PagesCount;
        }

        #endregion Methods

        #region PropertyChanged

        public void OnItemsPerPageChanged()
        {
            Refresh();
        }

        protected override void RefreshOverride()
        {
            CheckCurrentPage();
            base.RefreshOverride();
        }

        #endregion PropertyChanged
    }
}