using System.Collections.Generic;
using System.Collections.ObjectModel;
using My.CoachManager.Presentation.Prism.Core.Filters;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public interface IReadOnlyListViewModel : INavigatableWorkspaceViewModel
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        IFilteredCollection FilteredItems { get; }

        /// <summary>
        /// Gets or sets the preset columns to displayed.
        /// </summary>
        Dictionary<object, string[]> PresetColumns { get; }

        /// <summary>
        /// Gets or sets the columns to displayed.
        /// </summary>
        ObservableCollection<string> DisplayedColumns { get; set; }

        /// <summary>
        /// Command to change displayed columns.
        /// </summary>
        DelegateCommand<object> ChangeDisplayedColumnsCommand { get; set; }
    }
}