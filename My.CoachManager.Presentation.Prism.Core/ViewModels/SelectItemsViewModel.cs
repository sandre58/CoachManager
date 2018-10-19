using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Core.Models;
using My.CoachManager.Presentation.Prism.Core.Models.Filters;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{

    public abstract class SelectItemsViewModel<TEntityModel> : DialogViewModel, ISelectItemsViewModel<TEntityModel>
    where TEntityModel : class, IEntityModel, IModifiable, IValidatable, new()
    {
        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets items.
        /// </summary>
        ICollection ISelectItemsViewModel.Items
        {
            get => Items;
            set => Items = new ObservableCollection<TEntityModel>((IEnumerable<TEntityModel>)value);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public ObservableCollection<TEntityModel> Items { get; set; }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public IEnumerable<TEntityModel> SelectedItems { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public TEntityModel SelectedItem { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets list parameters.
        /// </summary>
        public ListParametersViewModel Parameters { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets list filters parameters.
        /// </summary>
        public IListFiltersViewModel Filters { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets if we can refresh after initialisation.
        /// </summary>
        public override bool RefreshOnInit => true;

        #endregion Members

        #region Methods

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            Items = new ObservableCollection<TEntityModel>();
        }

        #endregion Initialization

        #region Properties Changed

        /// <summary>
        /// Calls when selected item change.
        /// </summary>
        protected virtual void OnItemsChanged()
        {
            if (Filters != null)
            {
                Filters.Items = new FilteredCollectionView<TEntityModel>(Items.ToObservableCollection());
            }
        }

        #endregion Properties Changed

        #endregion Methods
    }
}