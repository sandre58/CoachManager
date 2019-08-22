using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.Presentation.Core.Models;
using My.CoachManager.Presentation.Wpf.Core.Enums;
using System.Collections;
using System.Collections.Generic;

namespace My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces
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
        new IEnumerable<TEntityModel> SelectedItems { get; }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        TEntityModel SelectedItem { get; }
    }

    public interface IListViewModel : IRefreshable
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        ICollection Items { get; set; }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        IList SelectedItems { get; }

        /// <summary>
        /// Gets or sets count all items.
        /// </summary>
        int AllItemsCount { get; }

        /// <summary>
        /// Gets or sets count items.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets or sets selection mode.
        /// </summary>
        SelectionMode SelectionMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicates the list is in read only.
        /// </summary>
        bool IsReadOnly { get; set; }

        /// <summary>
        /// Gets or sets list filters.
        /// </summary>
        IFiltersViewModel Filters { get; set; }

        /// <summary>
        /// Gets list parameters.
        /// </summary>
        ListParameters ListParameters { get; }
    }
}