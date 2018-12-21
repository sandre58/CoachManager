using System.Collections;
using System.Collections.Generic;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.Presentation.Prism.Core.Enums;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces
{
    public interface IListViewModel<TEntityModel> : IListViewModel
        where TEntityModel : class, IEntityModel, IModifiable, IValidatable, new()
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        new ObservableItemsCollection<TEntityModel> Items { get; set; }

        /// <summary>
        /// Gets or sets the selected items.
        /// </summary>
        new IEnumerable<TEntityModel> SelectedItems { get; set; }

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
        /// Gets or sets the selected item.
        /// </summary>
        IEnumerable SelectedItems { get; set; }

        /// <summary>
        /// Gets or sets selection mode.
        /// </summary>
        SelectionMode SelectionMode { get; set; }

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
        IListFiltersViewModel Filters { get; set; }
    }
}