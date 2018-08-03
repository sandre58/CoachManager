using System.Collections.ObjectModel;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public interface IFilteredListViewModel
    {
        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        ObservableCollection<FilterViewModel> Filters { get; set; }

        /// <summary>
        /// Gets or sets the  active filters.
        /// </summary>
        int CountActiveFilters { get; }

        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        IFilterViewModel SpeedFilter { get; set; }

        /// <summary>
        /// Command to reset filters.
        /// </summary>
        DelegateCommand ResetFiltersCommand { get; set; }

        /// <summary>
        /// Gets or sets Show Filters Command.
        /// </summary>
        DelegateCommand ShowFiltersCommand { get; set; }
    }
}