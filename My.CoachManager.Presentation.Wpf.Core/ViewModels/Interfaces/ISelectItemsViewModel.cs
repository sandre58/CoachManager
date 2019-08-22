using My.CoachManager.Presentation.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces
{
    public interface ISelectItemsViewModel<TModel> : ISelectItemsViewModel, IListViewModel<TModel>
        where TModel : class, ISelectable, IEntityModel, IModifiable, IValidatable, new()
    {
        /// <summary>
        /// Gets or sets not selectionnable items.
        /// </summary>
        new IEnumerable<TModel> NotSelectableItems { get; set; }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        new TModel SelectedItem { get; set; }
    }

    public interface ISelectItemsViewModel : IListViewModel, IWorkspaceDialogViewModel
    {
        /// <summary>
        /// Gets or sets not selectionnable items.
        /// </summary>
        IList NotSelectableItems { get; set; }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        object SelectedItem { get; set; }
    }
}