using System;
using System.Linq;
using System.Windows.Input;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Entities;
using Prism.Commands;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class ItemViewModel<TEntityViewModel> : NavigatableWorkspaceViewModel, IItemViewModel
        where TEntityViewModel : class, IEntityViewModel, IValidatable, IModifiable, new()
    {
        #region Fields

        private TEntityViewModel _item;
        private int _activeId;

        #endregion Fields

        #region Members

        /// <summary>
        /// Get or set Item.
        /// </summary>
        public TEntityViewModel Item
        {
            get
            {
                return _item;
            }
            protected set
            {
                SetProperty(ref _item, value);
            }
        }

        /// <summary>
        /// Get or Set Refresh Command.
        /// </summary>
        public ICommand RefreshCommand { get; set; }

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
        protected override void InitializeCommands()
        {
            base.InitializeCommands();

            EditCommand = new DelegateCommand(Edit, CanEdit);
            RefreshCommand = new DelegateCommand(Refresh, CanRefresh);
        }

        #endregion Initialization

        #region Refresh

        /// <summary>
        /// Refresh Items.
        /// </summary>
        public virtual void Refresh()
        {
            RefreshDataAsync();
        }

        /// <summary>
        /// Can refresh item.
        /// </summary>
        public virtual bool CanRefresh()
        {
            return true;
        }

        #endregion Refresh

        #region Edit

        /// <summary>
        /// Edit Item.
        /// </summary>
        protected virtual void Edit()
        {
            if (CanEdit())
            {
                Locator.DialogService.ShowWorkspaceDialog(GetEditViewType(), null, before =>
                    {
                        var vm = before.Context as IEditViewModel;
                        OnEditRequested(Item, vm);
                    },
                    after =>
                    {
                        OnEditCompleted(Item, after.Result);
                    });
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
        protected virtual void OnEditRequested(TEntityViewModel item, IEditViewModel viewModel)
        {
            if (viewModel != null) viewModel.LoadItemById(item.Id);
        }

        /// <summary>
        /// Called after the edit action.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="result">The dialog result.</param>
        protected virtual void OnEditCompleted(TEntityViewModel item, DialogResult result)
        {
            if (result == DialogResult.Ok) RefreshDataAsync();
        }

        #endregion Edit

        #region Data

        /// <summary>
        /// Load an item by id.
        /// </summary>
        public void LoadItemById(int id)
        {
            _activeId = id;
            RefreshDataAsync();
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