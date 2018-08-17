using System;
using System.Linq;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Models;
using Prism.Commands;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces
{
    public abstract class ItemViewModel<TEntityViewModel> : NavigatableWorkspaceViewModel, IItemViewModel<TEntityViewModel>
        where TEntityViewModel : class, IEntityModel, IValidatable, IModifiable, new()
    {
        #region Fields

        private int _activeId;

        #endregion Fields

        #region Members

        IEntityModel IItemViewModel.Item
        {
            get => Item;
            set => Item = (TEntityViewModel)value;
        }


        /// <summary>
        /// Get or set Item.
        /// </summary>
        public TEntityViewModel Item { get; set; }

        /// <summary>
        /// Gets or sets the edit command.
        /// </summary>
        public DelegateCommand EditCommand { get; set; }

        #endregion Members

        #region Methods

        #region Abstract Methods

        /// <summary>
        /// Get Item View Type.
        /// </summary>
        /// <returns></returns>
        protected abstract Type GetEditViewType();

        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected abstract TEntityViewModel LoadItemCore(int id);

        #endregion Abstract Methods

        #region Initialization

        /// <summary>
        /// Initializes data in constructor.
        /// </summary>
        protected override void InitializeData()
        {
            Mode = ScreenMode.Read;
            Item = new TEntityViewModel();
        }

        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            EditCommand = new DelegateCommand(Edit, CanEdit);
        }

        #endregion Initialization

        #region Edit

        /// <summary>
        /// Edit Item.
        /// </summary>
        protected virtual void Edit()
        {
            if (CanEdit())
            {
                //DialogService.ShowWorkspaceDialog(GetEditViewType(), null, before =>
                //    {
                //        var vm = before.Context as IEditViewModel<TEntityViewModel>;
                //        OnEditRequested(Item, vm);
                //    },
                //    after =>
                //    {
                //        OnEditCompleted(Item, after.Result);
                //    });
            }
        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        protected virtual bool CanEdit()
        {
            return Mode == ScreenMode.Read && Item != null && GetEditViewType() != null;
        }

        /// <summary>
        /// Called before the edit action;
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="viewModel">The view model.</param>
        protected virtual void OnEditRequested(TEntityViewModel item, IEditViewModel<TEntityViewModel> viewModel)
        {
            viewModel?.LoadItemById(item.Id);
        }

        /// <summary>
        /// Called after the edit action.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="result">The dialog result.</param>
        protected virtual void OnEditCompleted(TEntityViewModel item, DialogResult result)
        {
            if (result == DialogResult.Ok) Refresh();
        }

        #endregion Edit

        #region Data

        /// <summary>
        /// Load an item by id.
        /// </summary>
        public void LoadItemById(int id)
        {
            _activeId = id;
            Refresh();
        }

        /// <summary>
        /// Save.
        /// </summary>
        protected override void LoadDataCore()
        {
            if (_activeId > 0)
            {
                Item = LoadItemCore(_activeId);
            }
            else
            {
                Item = new TEntityViewModel();
            }
        }

        #endregion Data

        #region Navigation

        /// <summary>
        /// Gets a value indicating whether this instance should be kept-alive upon deactivation.
        /// </summary>
        public override bool KeepAlive { get { return false; } }

        /// <inheritdoc />
        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        protected override void OnNavigatedToCore(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.Any(x => x.Key == "Id"))
            {
                var param = navigationContext.Parameters["Id"].ToString();
                int id;
                if (int.TryParse(param, out id))
                {
                    LoadItemById(id);
                }
            }
        }

        #endregion Navigation

        #region Properties Changed

        /// <summary>
        /// Calls when Item changes.
        /// </summary>
        protected virtual void OnItemChanged()
        {
            if (Item != null) _activeId = Item.Id;
            RaisePropertyChanged(() => Title);
        }

        #endregion Properties Changed

        #endregion Methods
    }
}