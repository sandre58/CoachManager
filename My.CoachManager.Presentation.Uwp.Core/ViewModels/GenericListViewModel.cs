using System.Collections.Generic;
using My.CoachManager.Presentation.Core.Models;
using Prism.Commands;

namespace My.CoachManager.Presentation.Uwp.Core.ViewModels
{
    public abstract class GenericListViewModel<TModel> : ScreenViewModel
        where TModel : IEntityModel
    {
        #region Members

        /// <summary>
        /// Gets or sets Items.
        /// </summary>
        public IList<TModel> Items { get; set; }

        /// <summary>
        /// Gets items count.
        /// </summary>
        public int ItemsCount => Items?.Count ?? 0;

        /// <summary>
        /// Gets or sets selected Item.
        /// </summary>
        public TModel SelectedItem { get; set; }

        /// <summary>
        /// Gets or sets selected Items.
        /// </summary>
        public IList<TModel> SelectedItems { get; set; }

        /// <summary>
        /// Gets or sets selection Mode.
        /// </summary>
        public SelectionMode SelectionMode { get; set; }

        /// <summary>
        /// Gets or sets Edit Command.
        /// </summary>
        public DelegateCommand<TModel> EditCommand { get; private set; }

        /// <summary>
        /// Gets or sets Add Command.
        /// </summary>
        public DelegateCommand AddCommand { get; private set; }

        /// <summary>
        /// Gets or sets Remove Command.
        /// </summary>
        public DelegateCommand<TModel> RemoveCommand { get; private set; }

        /// <summary>
        /// Gets or sets Remove Command.
        /// </summary>
        public DelegateCommand SelectItemsCommand { get; private set; }

        /// <summary>
        /// Gets or sets Cancel select Command.
        /// </summary>
        public DelegateCommand CancelSelectItemsCommand { get; private set; }

        #endregion

        #region EditItem

        #endregion
    }
}
