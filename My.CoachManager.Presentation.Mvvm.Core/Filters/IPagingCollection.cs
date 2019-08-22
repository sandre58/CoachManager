namespace My.CoachManager.Presentation.Mvvm.Core.Filters
{
    /// <summary>
    /// The contract for the IPagingCollection
    /// </summary>
    public interface IPagingCollection<out T> : IFilteredCollection<T>, IPagingCollection
    {
    }

    /// <summary>
    /// The contract for the IFilteredCollection
    /// </summary>
    public interface IPagingCollection : IFilteredCollection
    {
        /// <summary>
        /// Gets or sets current page.
        /// </summary>
        int CurrentPage { get; }

        /// <summary>
        /// gets or set items count by page.
        /// </summary>
        int ItemsPerPage { get; set; }

        /// <summary>
        /// Gets pages number.
        /// </summary>
        int PagesCount { get; }

        /// <summary>
        /// Gets items count.
        /// </summary>
        int ItemsCount { get; }

        /// <summary>
        /// Move to next page.
        /// </summary>
        void MoveToNextPage();

        /// <summary>
        /// Can Move to next page ?
        /// </summary>
        bool CanMoveToNextPage();

        /// <summary>
        /// Move to previous page.
        /// </summary>
        void MoveToPreviousPage();

        /// <summary>
        /// Can Move to previous page ?
        /// </summary>
        bool CanMoveToPreviousPage();

        /// <summary>
        /// Move to last page.
        /// </summary>
        void MoveToLastPage();

        /// <summary>
        /// Can Move to last page ?
        /// </summary>
        bool CanMoveToLastPage();

        /// <summary>
        /// Move to first page.
        /// </summary>
        void MoveToFirstPage();

        /// <summary>
        /// Can Move to first page ?
        /// </summary>
        bool CanMoveToFirstPage();

        /// <summary>
        /// Move to page.
        /// </summary>
        void MoveToPage(int page);

        /// <summary>
        /// Can Move to page ?
        /// </summary>
        bool CanMoveToPage(int page);
    }
}