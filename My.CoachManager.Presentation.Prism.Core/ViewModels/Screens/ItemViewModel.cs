using System.Windows.Input;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Entities;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class ItemViewModel<TEntityViewModel> : NavigatableWorkspaceViewModel, IItemViewModel
        where TEntityViewModel : class, IEntityViewModel, IValidatable, IModifiable, new()
    {
        #region Fields

        private TEntityViewModel _item;
        private int _activeId;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initialise a new instance of <see cref="ItemViewModel{TEntityViewModel}"/>.
        /// </summary>
        protected ItemViewModel()
        {
            Mode = ScreenMode.Read;
            Item = new TEntityViewModel();

            RefreshCommand = new DelegateCommand(Refresh, CanRefresh);
        }

        #endregion Constructor

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
                SetProperty(ref _item, value, () =>
                {
                    if (value != null) _activeId = value.Id;
                    OnItemChanged();
                });
            }
        }

        /// <summary>
        /// Get or Set Refresh Command.
        /// </summary>
        public ICommand RefreshCommand { get; set; }

        #endregion Members

        #region Methods

        #region Refresh

        /// <summary>
        /// Refresh Items.
        /// </summary>
        public virtual void Refresh()
        {
            RefreshData();
        }

        /// <summary>
        /// Can refresh item.
        /// </summary>
        public virtual bool CanRefresh()
        {
            return true;
        }

        #endregion Refresh

        /// <summary>
        /// Load an item by id.
        /// </summary>
        public void LoadItemById(int id)
        {
            _activeId = id;
            RefreshData();
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

        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected abstract TEntityViewModel LoadItemCore(int id);

        /// <summary>
        /// Calls when Item changes.
        /// </summary>
        protected virtual void OnItemChanged()
        {
            RaisePropertyChanged(() => Title);
        }

        #endregion Methods
    }
}