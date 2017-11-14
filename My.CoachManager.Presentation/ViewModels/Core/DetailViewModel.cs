using My.CoachManager.Presentation.Core.ViewModels;

namespace My.CoachManager.Presentation.ViewModels.Core
{
    /// <summary>
    /// A base class for all viewmodels that cater to workspace views.
    /// </summary>
    public abstract class DetailViewModel<TItemsViewModel, TEntityViewModel> : DetailViewModel
        where TItemsViewModel : NavigatableViewModel
        where TEntityViewModel : EntityViewModel
    {
        #region Fields

        private TEntityViewModel _item;
        private readonly int _itemId;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance.
        /// </summary>
        /// <param name="itemId">Item id.</param>
        public DetailViewModel(int itemId)
        {
            _itemId = itemId;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get or set Item.
        /// </summary>
        public virtual TEntityViewModel Item
        {
            get
            {
                return _item;
            }
            set
            {
                if (_item != null && _item.Equals(value)) return;

                _item = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => DisplayName);
                NotifyOfPropertyChange(() => IsModified);
            }
        }

        /// <summary>
        /// Get Item id.
        /// </summary>
        public virtual int ItemId
        {
            get
            {
                return _itemId;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Go to items view.
        /// </summary>
        protected virtual void GoToItemsView()
        {
            ServiceLocator.NavigationService.NavigateToView<TItemsViewModel>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != GetType()) return false;
            var vm = (DetailViewModel<TItemsViewModel, TEntityViewModel>)obj;
            if (Item == null && vm.Item == null) return true;
            return Item != null && Item.Equals(vm.Item);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion Methods
    }

    /// <summary>
    /// A base class for all viewmodels that cater to workspace views.
    /// </summary>
    public abstract class DetailViewModel : NavigatableViewModel
    {
    }
}