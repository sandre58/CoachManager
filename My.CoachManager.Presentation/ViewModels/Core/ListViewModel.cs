using System;
using System.Windows.Input;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Core.Helpers;
using My.CoachManager.Presentation.Core.Commands;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Enums;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces;
using My.CoachManager.Presentation.Resources.Strings;

namespace My.CoachManager.Presentation.ViewModels.Core
{
    public abstract class ListViewModel<TEntityViewModel, TEditViewModel, TFiltersViewModel> : ListViewModel<TEntityViewModel, TEditViewModel>
        where TFiltersViewModel : FiltersViewModel
        where TEntityViewModel : EntityViewModel, new()
        where TEditViewModel : EditViewModel<TEntityViewModel>, IDialogViewModel
    {
        #region Fields

        private TFiltersViewModel _filters;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected ListViewModel()
            : this(Activator.CreateInstance<TFiltersViewModel>())
        {
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="filters"></param>
        protected ListViewModel(TFiltersViewModel filters)
        {
            Filters = filters;
            ActiveFilters = ConvertHelper.ChangeType<TFiltersViewModel>(Filters.Clone());
            DefaultFilters = ConvertHelper.ChangeType<TFiltersViewModel>(ActiveFilters.Clone());
            FilterCommand = new DelegateCommand(Filter, CanFilter);
        }

        #endregion Constructors and Destructors

        #region Public Properties

        /// <summary>
        /// Get or Set Filter Command.
        /// </summary>
        public ICommand FilterCommand { get; set; }

        /// <summary>
        /// Get or set search parameter.
        /// </summary>
        public TFiltersViewModel Filters
        {
            get
            {
                return _filters;
            }

            set
            {
                if (_filters == value)
                {
                    return;
                }

                _filters = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Get or set actives search parameters.
        /// </summary>
        protected TFiltersViewModel ActiveFilters { get; set; }

        /// <summary>
        /// Get or set actives search parameters.
        /// </summary>
        protected TFiltersViewModel DefaultFilters { get; set; }

        #endregion Public Properties

        #region Public Methods and Operators

        /// <summary>
        /// Search.
        /// </summary>
        public virtual void Filter()
        {
            ActiveFilters = ConvertHelper.ChangeType<TFiltersViewModel>(Filters.Clone());

            RefreshData();
        }

        /// <summary>
        /// Refresh items.
        /// </summary>
        public override void RefreshItems()
        {
            Filters = ConvertHelper.ChangeType<TFiltersViewModel>(ActiveFilters.Clone());

            RefreshData();
        }

        /// <summary>
        /// Reset items.
        /// </summary>
        public override void ResetItems()
        {
            Filters = ConvertHelper.ChangeType<TFiltersViewModel>(DefaultFilters.Clone());
            ActiveFilters = ConvertHelper.ChangeType<TFiltersViewModel>(DefaultFilters.Clone());

            RefreshData();
        }

        /// <summary>
        /// Can Filter.
        /// </summary>
        public virtual bool CanFilter()
        {
            return Filters != null;
        }

        #endregion Public Methods and Operators
    }

    /// <summary>
    /// A base class for all viewmodels that cater to workspace views.
    /// </summary>
    public abstract class ListViewModel<TEntityViewModel, TEditViewModel> : ReadOnlyListViewModel<TEntityViewModel>
        where TEntityViewModel : EntityViewModel, new()
        where TEditViewModel : EditViewModel<TEntityViewModel>, IDialogViewModel
    {
        #region Constructors and Destructors

        protected ListViewModel()
        {
            AddCommand = new DelegateCommand(Add, CanAdd);
            EditCommand = new DelegateCommand(Edit, CanEdit);
            RemoveCommand = new DelegateCommand(Remove, CanRemove);
        }

        #endregion Constructors and Destructors

        #region Public Properties

        /// <summary>
        /// Get or Set Add Command.
        /// </summary>
        public ICommand AddCommand { get; set; }

        /// <summary>
        /// Get or Set Edit Command.
        /// </summary>
        public ICommand EditCommand { get; set; }

        /// <summary>
        /// Get or Set Remove Command.
        /// </summary>
        public ICommand RemoveCommand { get; set; }

        #endregion Public Properties

        #region Methods

        /// <summary>
        /// Add Item.
        /// </summary>
        public virtual async void Add()
        {
            if (CanAdd())
            {
                var vm = await ServiceLocator.DialogService.ShowDialog<TEditViewModel>();

                if (vm.DialogResult)
                {
                    RefreshItems();
                }
            }
        }

        /// <summary>
        /// Can add item.
        /// </summary>
        public virtual bool CanAdd()
        {
            return CanAddItem(SelectedItem);
        }

        /// <summary>
        /// Can add item.
        /// </summary>
        protected virtual bool CanAddItem(TEntityViewModel entity)
        {
            return true;
        }

        /// <summary>
        /// Open an item.
        /// </summary>
        /// <param name="entity"></param>
        protected override async void OpenItemCore(TEntityViewModel entity)
        {
            if (entity == null) return;
            var viewModel = (IDialogViewModel)Activator.CreateInstance(typeof(TEditViewModel), entity.Id);
            var vm = await ServiceLocator.DialogService.ShowDialog(viewModel);

            if (vm.DialogResult)
            {
                RefreshData();
            }
        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        public bool CanEdit()
        {
            return CanEditItem(SelectedItem);
        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        public virtual bool CanEditItem(TEntityViewModel entity)
        {
            return entity != null;
        }

        /// <summary>
        /// Edit Item.
        /// </summary>
        public void Edit()
        {
            EditItem(SelectedItem);
        }

        /// <summary>
        /// Edit Item.
        /// </summary>
        public void EditItem(TEntityViewModel entity)
        {
            if (CanEditItem(entity))
                EditItemCore(entity);
        }

        /// <summary>
        /// Edit Item.
        /// </summary>
        protected virtual void EditItemCore(TEntityViewModel entity)
        {
            OpenItem(entity);
        }

        /// <summary>
        /// Delete Item.
        /// </summary>
        public void Remove()
        {
            RemoveItem(SelectedItem);
        }

        /// <summary>
        /// Can Remove item.
        /// </summary>
        public bool CanRemove()
        {
            return CanRemoveItem(SelectedItem);
        }

        /// <summary>
        /// Can Remove item.
        /// </summary>
        public virtual bool CanRemoveItem(TEntityViewModel entity)
        {
            return entity != null;
        }

        /// <summary>
        /// Delete Item.
        /// </summary>
        public virtual async void RemoveItem(TEntityViewModel entity)
        {
            if (CanRemove())
            {
                if (entity != null &&
                    await DialogService.ShowQuestion(MessageResources.ConfirmationRemovingItem) ==
                    MessageDialogResponse.Yes)
                {
                    RemoveItemCore(entity);
                    RefreshItems();
                }
            }
        }

        /// <summary>
        /// Remove Item.
        /// </summary>
        protected abstract void RemoveItemCore(TEntityViewModel item);

        /// <summary>
        /// Create keyboard shortcut on item.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="entity"></param>
        public override void KeyboardActionOnItem(KeyEventArgs args, TEntityViewModel entity)
        {
            base.KeyboardActionOnItem(args, entity);
            switch (args.Key)
            {
                case Key.Delete:
                    RemoveItem(entity);
                    break;
            }
        }

        #endregion Methods
    }
}