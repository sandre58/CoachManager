using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces
{
    public interface ISelectItemsViewModel<TEntityModel> : ISelectItemsViewModel
        where TEntityModel : class, IEntityModel, IModifiable, IValidatable, new()
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        new ObservableCollection<TEntityModel> Items { get; set; }

        /// <summary>
        /// Gets or sets the selected items.
        /// </summary>
       IEnumerable<TEntityModel> SelectedItems { get; set; }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        TEntityModel SelectedItem { get; set; }
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