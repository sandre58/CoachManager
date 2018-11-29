using System.Collections;
using System.Collections.Generic;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces
{
    public interface ISelectItemsViewModel<TModel> : ISelectItemsViewModel
        where TModel : ISelectable
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        new ObservableItemsCollection<TModel> Items { get; set; }

        /// <summary>
        /// Gets or sets the selected items.
        /// </summary>
       IEnumerable<TModel> SelectedItems { get; set; }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        TModel SelectedItem { get; set; }
    }

    public interface ISelectItemsViewModel : IWorkspaceDialogViewModel
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        ICollection Items { get; set; }

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