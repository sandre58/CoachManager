using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Presentation.Core.Models;
using My.CoachManager.Presentation.Wpf.Core.Dialog;
using My.CoachManager.Presentation.Wpf.Core.Enums;
using My.CoachManager.Presentation.Wpf.Core.Manager;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Base;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;
using Prism.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace My.CoachManager.Presentation.Wpf.Core.ViewModels
{
    public abstract class ListViewModel<TEntityModel, TEditView, TItemView> : ListViewModel<TEntityModel>
        where TEntityModel : class, ISelectable, IEntityModel, IModifiable, IValidatable, new()
        where TEditView : IEditViewModel<TEntityModel>
        where TItemView : IItemViewModel<TEntityModel>
    {
        #region Members

        /// <summary>
        /// Gets or sets the remove item message.
        /// </summary>
        protected string ConfirmationRemoveItemMessage { get; set; }

        /// <summary>
        /// Gets or sets the remove items message.
        /// </summary>
        protected string ConfirmationRemoveItemsMessage { get; set; }

        /// <summary>
        /// Gets or sets the add command.
        /// </summary>
        public DelegateCommand AddCommand { get; private set; }

        /// <summary>
        /// Gets or sets the edit command.
        /// </summary>
        public DelegateCommand<TEntityModel> OpenItemCommand { get; private set; }

        /// <summary>
        /// Gets or sets the edit command.
        /// </summary>
        public DelegateCommand<TEntityModel> EditItemCommand { get; private set; }

        /// <summary>
        /// Gets or sets the edit command.
        /// </summary>
        public DelegateCommand EditCommand { get; private set; }

        /// <summary>
        /// Gets or sets the remove command.
        /// </summary>
        public DelegateCommand<TEntityModel> RemoveItemCommand { get; private set; }

        /// <summary>
        /// Gets or sets the remove command.
        /// </summary>
        public DelegateCommand RemoveCommand { get; private set; }

        #endregion Members

        #region Constructors

        /// <inheritdoc />
        /// <summary>
        /// Initialise a new instance of <see cref="T:My.CoachManager.Presentation.Wpf.Core.ViewModels.ListViewModel`1" />
        /// </summary>
        protected ListViewModel()
        {
            ConfirmationRemoveItemMessage = MessageResources.ConfirmationRemovingItem;
            ConfirmationRemoveItemsMessage = MessageResources.ConfirmationRemovingItems;
        }

        #endregion Constructors

        #region Methods

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            AddCommand = new DelegateCommand(Add, CanAdd);
            RemoveItemCommand = new DelegateCommand<TEntityModel>(Remove, CanRemove);
            RemoveCommand = new DelegateCommand(Remove, CanRemove);
            EditCommand = new DelegateCommand(Edit, CanEdit);
            EditItemCommand = new DelegateCommand<TEntityModel>(Edit, CanEdit);
            OpenItemCommand = new DelegateCommand<TEntityModel>(Open, CanOpen);
        }

        #endregion Initialization

        #region Open

        /// <summary>
        /// Open Item.
        /// </summary>
        protected virtual void Open(TEntityModel item)
        {
            if (typeof(IEditViewModel).IsAssignableFrom(typeof(TItemView)))
            {
                Edit(item);
                return;
            }

            var x = item ?? SelectedItem;
            if (!CanOpen(x)) return;

            NavigationManager.NavigateTo(typeof(TItemView), x.Id);
        }

        /// <summary>
        /// Can Open item.
        /// </summary>
        protected virtual bool CanOpen(TEntityModel item)
        {
            if (typeof(TEditView) == typeof(TItemView))
            {
                return CanEdit(item);
            }

            return Mode == ScreenMode.Read && item != null;
        }

        #endregion Open

        #region Add

        /// <summary>
        /// Add a new item.
        /// </summary>
        protected virtual void Add()
        {
            DialogManager.ShowEditDialog<TEditView>(0, dialog =>
            {
                OnAddCompleted(dialog.Result);
            });
        }

        /// <summary>
        /// Can add a new item.
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanAdd()
        {
            return Mode == ScreenMode.Read && !IsReadOnly;
        }

        /// <summary>
        /// Called after the Add action.
        /// </summary>
        /// <param name="result"></param>
        protected virtual void OnAddCompleted(DialogResult result)
        {
            if (result == DialogResult.Ok) Refresh();
        }

        #endregion Add

        #region Edit

        /// <summary>
        /// Edit Item.
        /// </summary>
        protected virtual void Edit(TEntityModel item)
        {
            var x = item ?? SelectedItem;
            if (x == null) return;
            if (!CanEdit(x)) return;

            DialogManager.ShowEditDialog<TEditView>(x.Id, dialog =>
            {
                OnEditCompleted(dialog.Result);
            });
        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        protected virtual bool CanEdit(TEntityModel item)
        {
            return Mode == ScreenMode.Read && !IsReadOnly;
        }

        /// <summary>
        /// Called after the edit action.
        /// </summary>
        /// <param name="result">The dialog result.</param>
        protected virtual void OnEditCompleted(DialogResult result)
        {
            if (result == DialogResult.Ok) Refresh();
        }

        /// <summary>
        /// Edit Item.
        /// </summary>
        protected virtual void Edit()
        {
            Edit(SelectedItem);
        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        protected virtual bool CanEdit()
        {
            return Mode == ScreenMode.Read && SelectedItems != null && SelectedItems.Count() == 1 && !IsReadOnly;
        }

        #endregion Edit

        #region Remove

        /// <summary>
        /// Can Edit item.
        /// </summary>
        protected virtual bool CanRemove(TEntityModel item)
        {
            return Mode == ScreenMode.Read && !IsReadOnly;
        }

        /// <summary>
        /// Remove Item.
        /// </summary>
        protected virtual void Remove(TEntityModel item)
        {
            var list = new List<TEntityModel> { item };
            RemoveCore(list);
        }

        /// <summary>
        /// Remove Data.
        /// </summary>
        private void RemoveCore(IList<TEntityModel> items)
        {
            if (!OnRemoveRequested(items)) return;

            CallWebService(() => items.ToList().ForEach(RemoveItemCore),
                () => OnRemoveSucceeded(items),
                null,
                null,
                true);
        }

        /// <summary>
        /// Removes the item from the data source.
        /// </summary>
        /// <param name="item"></param>
        protected abstract void RemoveItemCore(TEntityModel item);

        /// <summary>
        /// Called before the edit action;
        /// </summary>
        protected virtual bool OnRemoveRequested(IList<TEntityModel> items)
        {
            var message = items.Count > 1 ? ConfirmationRemoveItemsMessage : ConfirmationRemoveItemMessage;
            return DialogManager.ShowWarningDialog(message, MessageDialogButtons.YesNo) == DialogResult.Yes;
        }

        /// <summary>
        /// Called after the edit action;
        /// </summary>
        protected virtual void OnRemoveSucceeded(IList<TEntityModel> items)
        {
            var message = items.Count > 1 ? string.Format(MessageResources.ItemsRemoved, items.Count) : MessageResources.ItemRemoved;
            NotificationManager.ShowSuccess(message);
        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        protected virtual bool CanRemove()
        {
            return Mode == ScreenMode.Read && SelectedItems != null && SelectedItems.Any();
        }

        /// <summary>
        /// Remove Item.
        /// </summary>
        protected virtual void Remove()
        {
            RemoveCore(SelectedItems.ToList());
        }

        #endregion Remove

        #region Properties Changed

        /// <inheritdoc />
        /// <summary>
        /// Occurs where selection change.
        /// </summary>
        protected override void OnSelectionChanged()
        {
            base.OnSelectionChanged();

            RemoveCommand?.RaiseCanExecuteChanged();
            EditCommand?.RaiseCanExecuteChanged();
        }

        #endregion Properties Changed

        #endregion Methods
    }

    public abstract class ListViewModel<TEntityModel> : NavigableWorkspaceViewModel, IListViewModel<TEntityModel>
    where TEntityModel : class, ISelectable, IEntityModel, IModifiable, IValidatable, new()
    {
        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets items.
        /// </summary>
        ICollection IListViewModel.Items
        {
            get => Items;
            set => Items = new ObservableItemsCollection<TEntityModel>((IEnumerable<TEntityModel>)value);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public ObservableItemsCollection<TEntityModel> Items { get; set; }

        /// <summary>
        /// Gets or sets count all items.
        /// </summary>
        public int AllItemsCount { get; protected set; }

        /// <summary>
        /// Gets or sets count items.
        /// </summary>
        public int Count { get; protected set; }

        /// <summary>
        /// Gets or sets the displayed items.
        /// </summary>
        public IEnumerable<TEntityModel> DisplayedItems
        {
            get
            {
                if (Filters != null) return Filters.Items;

                return Items;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets items.
        /// </summary>
        IList IListViewModel.SelectedItems => SelectedItems?.ToList();

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public IEnumerable<TEntityModel> SelectedItems
        {
            get { return Items?.Where(x => x.IsSelected).ToList(); }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public TEntityModel SelectedItem => SelectedItems?.FirstOrDefault();

        /// <summary>
        /// Gets or sets not selectable items.
        /// </summary>
        public IEnumerable<TEntityModel> NotSelectableItems { get; set; }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public bool AreAllSelected
        {
            get
            {
                return DisplayedItems != null && DisplayedItems.Any(x => x.IsSelectable) && DisplayedItems.Where(x => x.IsSelectable).All(x => x.IsSelected);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets selection mode.
        /// </summary>
        public SelectionMode SelectionMode
        { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets a value indicates the list is in read only.
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets list parameters.
        /// </summary>
        public ListParameters ListParameters { get; protected set; }

        /// <summary>
        /// Gets or sets list filters parameters.
        /// </summary>
        public IFiltersViewModel<TEntityModel> Filters { get; protected set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets list filters parameters.
        /// </summary>
        IFiltersViewModel IListViewModel.Filters
        {
            get => Filters;
            set => Filters = value as IFiltersViewModel<TEntityModel>;
        }

        /// <summary>
        /// Gets or sets select all command.
        /// </summary>
        public DelegateCommand<bool?> SelectAllCommand { get; private set; }

        /// <summary>
        /// Gets or sets select command.
        /// </summary>
        public DelegateCommand<TEntityModel> SelectItemCommand { get; private set; }

        /// <summary>
        /// Gets or sets select command.
        /// </summary>
        public DelegateCommand<IEnumerable<TEntityModel>> SelectItemsCommand { get; private set; }

        #endregion Members

        #region Methods

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            
            SelectItemCommand = new DelegateCommand<TEntityModel>(SelectItem, CanSelectItem);
            SelectItemsCommand = new DelegateCommand<IEnumerable<TEntityModel>>(SelectItems, CanSelectItems);
            SelectAllCommand = new DelegateCommand<bool?>(SelectAll, CanSelectAll);

            SelectionMode = SelectionMode.Multiple;
            
            Items = new ObservableItemsCollection<TEntityModel>();

            IsReadOnly = false;
        }

        #endregion Initialization

        #region Data

        /// <summary>
        /// Initialize data asynchronous.
        /// </summary>
        protected override void OnInitializeDataCompleted()
        {
            Application.Current.Dispatcher.Invoke(() => Filters?.ResetFilters());
        }

        /// <summary>
        /// Call before load data.
        /// </summary>
        protected override void OnLoadDataRequested()
        {
            SelectAll(false);
        }

        /// <summary>
        /// Call after load data.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            if (AllItemsCount == 0) AllItemsCount = Filters?.AllItemsCount ?? (Items?.Count ?? 0);
            if (Count == 0) Count = Filters?.FilteredItemsCount ?? (Items?.Count ?? 0);

            base.OnLoadDataCompleted();
        }

        #endregion Data

        #region SelectAll

        /// <summary>
        /// Can Select All ?
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanSelectAll(bool? value)
        {
            return DisplayedItems != null && DisplayedItems.Any(x => x.IsSelectable);
        }

        /// <summary>
        /// Select or unselect all.
        /// </summary>
        protected virtual void SelectAll(bool? value)
        {
            if (DisplayedItems != null)
            {
                foreach (var x in DisplayedItems.Where(x => x.IsSelectable))
                {
                    if (value != null) x.IsSelected = value.Value;
                }
            }
        }

        #endregion SelectAll

        #region SelectItem

        /// <summary>
        /// Can select an item.
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanSelectItem(TEntityModel item)
        {
            return item != null && item.IsSelectable;
        }

        /// <summary>
        /// Select an item.
        /// </summary>
        public virtual void SelectItem(TEntityModel item)
        {
            if (item == null || !item.IsSelectable) return;

            item.IsSelected = true;
        }

        #endregion SelectItem

        #region SelectItems

        /// <summary>
        /// Can select items.
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanSelectItems(IEnumerable<TEntityModel> items)
        {
            return items.Any(x => x.IsSelectable);
        }

        /// <summary>
        /// Select items.
        /// </summary>
        public virtual void SelectItems(IEnumerable<TEntityModel> items)
        {
            var entityModels = items as IList<TEntityModel> ?? items.ToList();
            foreach (var x in entityModels)
            {
                SelectItem(x);
            }
        }

        #endregion SelectItems

        #region Properties Changed

        /// <summary>
        /// Calls when selected item change.
        /// </summary>
        protected virtual void OnItemsChanged()
        {
            Filters?.UpdateCollection(Items);

            foreach (var x in Items)
            {
                x.SelectedChanged += Item_SelectedChanged;
            }

            OnSelectionChanged();
        }

        /// <summary>
        /// Calls when selection Changed.
        /// </summary>
        private void Item_SelectedChanged(object sender, EventArgs e)
        {
            if (sender is ISelectable selected && (SelectionMode == SelectionMode.Single && selected.IsSelected))
            {
                foreach (var x in Items)
                {
                    x.SelectedChanged -= Item_SelectedChanged;
                }

                var itemsToChange = SelectedItems.ToList();

                foreach (var x in itemsToChange.Where(x => !x.Equals(selected)))
                {
                    x.IsSelected = false;
                }

                foreach (var x in Items)
                {
                    x.SelectedChanged += Item_SelectedChanged;
                }
            }

            OnSelectionChanged();
        }

        /// <summary>
        /// Calls when selection Changed.
        /// </summary>
        protected virtual void OnSelectionChanged()
        {
            RaisePropertyChanged(() => AreAllSelected);
            RaisePropertyChanged(() => SelectedItems);
            RaisePropertyChanged(() => SelectedItem);
            SelectAllCommand?.RaiseCanExecuteChanged();
        }

        #endregion Properties Changed

        #endregion Methods
    }
}