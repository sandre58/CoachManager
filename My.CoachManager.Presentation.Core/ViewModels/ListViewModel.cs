using Microsoft.Practices.ObjectBuilder2;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Core.Dialog;
using My.CoachManager.Presentation.Core.Enums;
using My.CoachManager.Presentation.Core.Manager;
using My.CoachManager.Presentation.Core.Models;
using My.CoachManager.Presentation.Core.Models.Filters;
using My.CoachManager.Presentation.Core.ViewModels.Interfaces;
using Prism.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using My.CoachManager.CrossCutting.Core.Resources;

namespace My.CoachManager.Presentation.Core.ViewModels
{
    public abstract class ListViewModel<TEntityModel, TEditView, TItemView> : ListViewModel<TEntityModel>
        where TEntityModel : class, ISelectable, IEntityModel, IModifiable, IValidatable, new()
        where TEditView : FrameworkElement
        where TItemView : FrameworkElement
    {
        #region Members

        /// <summary>
        /// Gets or sets the remove item message.
        /// </summary>
        public string ConfirmationRemoveItemMessage { get; set; }

        /// <summary>
        /// Gets or sets the remove items message.
        /// </summary>
        public string ConfirmationRemoveItemsMessage { get; set; }

        /// <summary>
        /// Gets or sets the add command.
        /// </summary>
        public DelegateCommand AddCommand { get; set; }

        /// <summary>
        /// Gets or sets the edit command.
        /// </summary>
        public DelegateCommand<TEntityModel> OpenCommand { get; set; }

        /// <summary>
        /// Gets or sets the edit command.
        /// </summary>
        public DelegateCommand<TEntityModel> EditCommand { get; set; }

        /// <summary>
        /// Gets or sets the edit command.
        /// </summary>
        public DelegateCommand EditSelectedItemCommand { get; set; }

        /// <summary>
        /// Gets or sets the remove command.
        /// </summary>
        public DelegateCommand<TEntityModel> RemoveCommand { get; set; }

        /// <summary>
        /// Gets or sets the remove command.
        /// </summary>
        public DelegateCommand RemoveSelectedItemsCommand { get; set; }

        #endregion Members

        #region Constructors
        
        /// <summary>
        /// Initialise a new instance of <see cref="ListViewModel{TEntityModel}"/>
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
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            AddCommand = new DelegateCommand(Add, CanAdd);
            RemoveCommand = new DelegateCommand<TEntityModel>(Remove, CanRemove);
            RemoveSelectedItemsCommand = new DelegateCommand(Remove, CanRemove);
            EditSelectedItemCommand = new DelegateCommand(Edit, CanEdit);
            EditCommand = new DelegateCommand<TEntityModel>(Edit, CanEdit);
            OpenCommand = new DelegateCommand<TEntityModel>(Open, CanOpen);
        }

        #endregion Initialization

        #region Data

        /// <summary>
        /// Call after load data.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            base.OnLoadDataCompleted();

            Filters?.ApplyFilters();
        }


        #endregion

        #region Open

        /// <summary>
        /// Open Item.
        /// </summary>
        protected virtual void Open(TEntityModel item)
        {
            if (typeof(TEditView) == typeof(TItemView))
            {
                Edit(item);
                return;
            }

            var x = item ?? SelectedItem;
            if (!CanOpen(x)) return;

            NavigationManager.NavigateTo<TItemView>(x.Id);
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
            if(x == null) return;
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

        #endregion Edit

        #region Edit

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
        /// Remove Item.
        /// </summary>
        protected virtual void Remove(TEntityModel item)
        {
            var x = item ?? SelectedItem;
            if (x == null) return;
            if (!CanRemove(x)) return;

            if (!OnRemoveRequested(x)) return;

            try
            {
                RemoveItemCore(x);
                OnRemoveCompleted(x);
            }
            catch (BusinessException e)
            {
                OnBusinessExceptionOccured(e);
            }
            catch (Exception e)
            {
                OnExceptionOccured(e);
            }
        }

        /// <summary>
        /// Removes the item from the data source.
        /// </summary>
        /// <param name="item"></param>
        protected abstract void RemoveItemCore(TEntityModel item);

        /// <summary>
        /// Can Edit item.
        /// </summary>
        protected virtual bool CanRemove(TEntityModel item)
        {
            return Mode == ScreenMode.Read && !IsReadOnly;
        }

        /// <summary>
        /// Called after the edit action;
        /// </summary>
        /// <param name="item">The item.</param>
        protected virtual void OnRemoveCompleted(TEntityModel item)
        {
            Refresh();
        }

        /// <summary>
        /// Called before the edit action;
        /// </summary>
        /// <param name="item">The item.</param>
        protected virtual bool OnRemoveRequested(TEntityModel item)
        {
            return DialogManager.ShowWarningDialog(ConfirmationRemoveItemMessage, MessageDialogButtons.YesNo) == DialogResult.Yes;
        }

        #endregion Remove

        #region Remove

        /// <summary>
        /// Remove Item.
        /// </summary>
        protected virtual void Remove()
        {
            if (!CanRemove()) return;

            if (!OnRemoveRequested()) return;

            try
            {
                var itemToDelete = SelectedItems.ToList();
                itemToDelete.ForEach(RemoveItemCore);
                OnRemoveCompleted();
            }
            catch (BusinessException e)
            {
                OnBusinessExceptionOccured(e);
            }
            catch (Exception e)
            {
                OnExceptionOccured(e);
            }
        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        protected virtual bool CanRemove()
        {
            return Mode == ScreenMode.Read && SelectedItems != null && SelectedItems.Any();
        }

        /// <summary>
        /// Called after the edit action;
        /// </summary>
        protected virtual void OnRemoveCompleted()
        {
            Refresh();
        }


        /// <summary>
        /// Called before the edit action;
        /// </summary>
        protected virtual bool OnRemoveRequested()
        {
            return DialogManager.ShowWarningDialog(ConfirmationRemoveItemsMessage, MessageDialogButtons.YesNo) == DialogResult.Yes;
        }

        #endregion Remove

        #region Properties Changed

        protected override void OnSelectionChanged()
        {
            base.OnSelectionChanged();

            RemoveSelectedItemsCommand.RaiseCanExecuteChanged();
            EditSelectedItemCommand.RaiseCanExecuteChanged();
        }

        #endregion Properties Changed

            #endregion Methods
        }

    public abstract class ListViewModel<TEntityModel> : NavigatableWorkspaceViewModel, IListViewModel<TEntityModel>
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

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets items.
        /// </summary>
        IEnumerable IListViewModel.SelectedItems
        {
            get => SelectedItems;
        }

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
        public TEntityModel SelectedItem
        {
            get => SelectedItems?.FirstOrDefault();
        }

        /// <summary>
        /// Gets or sets not selectionnable items.
        /// </summary>
        public IEnumerable<TEntityModel> NotSelectableItems { get; set; }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public bool AreAllSelected
        {
            get
            {
                return Items != null && Items.Any(x => x.IsSelectable) && Items.Where(x => x.IsSelectable).All(x => x.IsSelected);
            }
        }

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

        /// <summary>
        /// Gets list parameters.
        /// </summary>
        public ListParameters ListParameters { get; protected set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets list filters parameters.
        /// </summary>
        public IListFiltersViewModel Filters { get; set; }

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

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ListViewModel{TEntityModel}"/>
        /// </summary>
        protected ListViewModel() => SelectionMode = SelectionMode.Multiple;

        #endregion Constructors

        #region Methods

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            Items = new ObservableItemsCollection<TEntityModel>();

            IsReadOnly = false;
        }

        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all command property.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            SelectItemCommand = new DelegateCommand<TEntityModel>(SelectItem, CanSelectItem);
            SelectItemsCommand = new DelegateCommand<IEnumerable<TEntityModel>>(SelectItems, CanSelectItems);
            SelectAllCommand = new DelegateCommand<bool?>(SelectAll, CanSelectAll);
        }

        #endregion Initialization

        #region SelectAll

        /// <summary>
        /// Can Select All ?
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanSelectAll(bool? value)
        {
            return Items.Any(x => x.IsSelectable);
        }

        /// <summary>
        /// Select or unselect all.
        /// </summary>
        protected virtual void SelectAll(bool? value)
        {
            Items.Where(x => x.IsSelectable).ForEach(x =>
            {
                if (value != null) x.IsSelected = value.Value;
            });
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
            items?.ForEach(SelectItem);
        }

        #endregion SelectItems

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

            Items.ForEach(x => x.SelectedChanged += Item_SelectedChanged);
            SelectAllCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Calls when selection Changed.
        /// </summary>
        private void Item_SelectedChanged(object sender, EventArgs e)
        {
            if (sender is ISelectable selected && (SelectionMode == SelectionMode.Single && selected.IsSelected))
            {
                Items.ForEach(x => x.SelectedChanged -= Item_SelectedChanged);

                var itemsToChange = SelectedItems.ToList();

                itemsToChange.Where(x => !x.Equals(selected)).ForEach(x => x.IsSelected = false);

                Items.ForEach(x => x.SelectedChanged += Item_SelectedChanged);
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
        }

        #endregion Properties Changed

        #endregion Methods
    }
}