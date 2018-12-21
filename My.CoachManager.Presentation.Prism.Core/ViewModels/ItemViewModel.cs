using System.ComponentModel;
using System.Linq;
using My.CoachManager.Presentation.Prism.Core.Models;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class ItemViewModel<TModel> : NavigatableWorkspaceViewModel, IItemViewModel<TModel>
        where TModel : class, IEntityModel, IValidatable, IModifiable, INotifyPropertyChanged, new()
    {
        #region Fields

        private int _activeId;

        #endregion Fields

        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets item.
        /// </summary>
        IEntityModel IItemViewModel.Item
        {
            get => Item;
            set => Item = (TModel)value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Get or set Item.
        /// </summary>
        public TModel Item { get; set; }

        #endregion Members

        #region Methods

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes data in constructor.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            Item = new TModel();
        }

        #endregion Initialization

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Load an item by id.
        /// </summary>
        public virtual void LoadItemById(int id)
        {
            _activeId = id;
            Refresh();
        }

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override void LoadDataCore()
        {
            Item = _activeId > 0 ? LoadItemCore(_activeId) : new TModel();
        }

        /// <summary>
        /// Call after load data.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            Item?.ResetModified();
        }

        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected abstract TModel LoadItemCore(int id);

        #endregion Data

        #region Navigation

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        protected override void OnNavigatedToCore(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.Any(x => x.Key.ToUpper() == "ID"))
            {
                LoadItemById(int.Parse(navigationContext.Parameters.First(x => x.Key.ToUpper() == "ID").Value.ToString()));
            }
            
        }

        #endregion

        #region PropertyChanged

        /// <summary>
        /// Calls when Item changes.
        /// </summary>
        protected virtual void OnItemChanged()
        {
            if (Item != null) _activeId = Item.Id;

            Item?.ResetModified();
        }

        #endregion PropertyChanged

        #endregion Methods
    }
}