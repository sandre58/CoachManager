using System.Collections;
using System.Collections.Generic;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.Presentation.Core.Enums;
using My.CoachManager.Presentation.Core.Models;

namespace My.CoachManager.Presentation.Core.ViewModels.Interfaces
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
       new IList<TModel> SelectedItems { get; set; }

        /// <summary>
        /// Gets or sets not selectionnable items.
        /// </summary>
         new IList<TModel> NotSelectableItems { get; set; }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        new TModel SelectedItem { get; set; }
    }

    public interface ISelectItemsViewModel : IWorkspaceDialogViewModel
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        ICollection Items { get; set; }

        /// <summary>
        /// Gets or sets the selected items.
        /// </summary>
        IList SelectedItems { get; set; }

        /// <summary>
        /// Gets or sets not selectionnable items.
        /// </summary>
        IList NotSelectableItems { get; set; }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        object SelectedItem { get; set; }

        /// <summary>
        /// Gets or sets list filters.
        /// </summary>
        IListFiltersViewModel Filters { get; set; }

        /// <summary>
        /// Gets or sets selection mode.
        /// </summary>
        SelectionMode SelectionMode { get; set; }

        /// <summary>
        /// Gets list parameters.
        /// </summary>
        ListParameters ListParameters { get; }
    }
}