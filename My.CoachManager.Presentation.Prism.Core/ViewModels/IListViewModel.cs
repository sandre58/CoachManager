using System.Collections;
using System.Collections.ObjectModel;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public interface IListViewModel<TEntityModel> : IListViewModel
        where TEntityModel : class, IEntityModel, IModifiable, IValidatable, new()
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        new ObservableCollection<TEntityModel> Items { get; set; }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        TEntityModel SelectedItem { get; set; }
    }

    public interface IListViewModel : INavigatableWorkspaceViewModel
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        ICollection Items { get; set; }

        /// <summary>
        /// Gets or sets a value indicates the list is in read only.
        /// </summary>
        bool IsReadOnly { get; set; }

        /// <summary>
        /// Gets or sets list parameters.
        /// </summary>
        ListParametersViewModel Parameters { get; set; }

        /// <summary>
        /// Gets or sets list filters.
        /// </summary>
        ListFiltersViewModel Filters { get; set; }
    }
}