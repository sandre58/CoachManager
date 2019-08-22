using Prism.Commands;

namespace My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces
{
    public interface IPagingFiltersViewModel<T> : IFiltersViewModel<T>, IPagingFiltersViewModel
    {
    }

    public interface IPagingFiltersViewModel : IFiltersViewModel
    {

        /// <summary>
        /// Gets or sets current page.
        /// </summary>
        int CurrentPage { get; }

        /// <summary>
        /// Gets or sets pages count.
        /// </summary>
        int PagesCount { get; }

        /// <summary>
        /// Gets or sets items per page.
        /// </summary>
        int ItemsPerPage { get; set; }

        /// <summary>
        /// Gets or sets Hide MoveToPreviousPage Command.
        /// </summary>
        DelegateCommand MoveToPreviousPageCommand { get; set; }
        
        /// <summary>
        /// Gets or sets Hide MoveToNextPage Command.
        /// </summary>
        DelegateCommand MoveToNextPageCommand { get; set; }

        /// <summary>
        /// Gets or sets Hide MoveToFirstPageCommand Command.
        /// </summary>
        DelegateCommand MoveToFirstPageCommand { get; set; }

        /// <summary>
        /// Gets or sets Hide MoveToLastPageCommand Command.
        /// </summary>
        DelegateCommand MoveToLastPageCommand { get; set; }

        /// <summary>
        /// Gets or sets the Move to page command.
        /// </summary>
        DelegateCommand<int?> MoveToPageCommand { get; set; }

    }
}