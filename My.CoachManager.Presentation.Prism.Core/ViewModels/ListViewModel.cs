using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.ServiceLocation;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.Models;
using My.CoachManager.Presentation.Prism.Core.Models.Filters;
using My.CoachManager.Presentation.Prism.Core.Resources;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class ListViewModel<TEntityModel, TEditView, TItemView> : ListViewModel<TEntityModel>
        where TEntityModel : class, ISelectable, IEntityModel, IModifiable, IValidatable, new()
        where TEditView : FrameworkElement
        where TItemView : FrameworkElement
    {
        #region Members

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
        /// Gets or sets the remove command.
        /// </summary>
        public DelegateCommand<TEntityModel> RemoveCommand { get; set; }

        #endregion Members

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
            EditCommand = new DelegateCommand<TEntityModel>(Edit, CanEdit);
            OpenCommand = new DelegateCommand<TEntityModel>(Open, CanOpen);
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void InitializeShortcuts()
        {
            base.InitializeShortcuts();

            KeyboardShortcuts.Add(new KeyBinding(AddCommand, Key.N, ModifierKeys.Control));
        }

        #endregion Initialization

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
            var view = ServiceLocator.Current.GetInstance<TEditView>();
            var model = view.DataContext as IEditViewModel<TEntityModel>;
            model?.LoadItemById(0);

            DialogManager.ShowWorkspaceDialog(view, dialog =>
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
            if (!CanEdit(x)) return;

            var view = ServiceLocator.Current.GetInstance<TEditView>();
            var model = view.DataContext as IEditViewModel<TEntityModel>;
            model?.LoadItemById(x.Id);

            DialogManager.ShowWorkspaceDialog(view, dialog =>
            {
                OnEditCompleted(dialog.Result);
            });
        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        protected virtual bool CanEdit(TEntityModel item)
        {
            return Mode == ScreenMode.Read && item != null && !IsReadOnly;
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

        #region Remove

        /// <summary>
        /// Remove Item.
        /// </summary>
        protected virtual void Remove(TEntityModel item)
        {
            var x = item ?? SelectedItem;
            if (!CanRemove(x)) return;

            if (DialogManager.ShowWarningDialog(MessageResources.ConfirmationRemovingItem, MessageDialogButtons.YesNo) != DialogResult.Yes) return;

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
            return Mode == ScreenMode.Read && item != null && !IsReadOnly;
        }

        /// <summary>
        /// Called after the edit action;
        /// </summary>
        /// <param name="item">The item.</param>
        protected virtual void OnRemoveCompleted(TEntityModel item)
        {
            Refresh();
        }

        #endregion Remove

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

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public IEnumerable<TEntityModel> SelectedItems
        {
            get { return Items?.Where(x => x.IsSelected); }
            set
            {
                Items.ForEach(x => x.IsSelected = false);

                var items = Items.Where(x => x.IsSelectable).ToArray();
                foreach (var item in value)
                {
                    var toModify = items.Single(x => x.Equals(item));
                    toModify.IsSelected = true;
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public TEntityModel SelectedItem
        {
            get => SelectedItems?.FirstOrDefault();
            set => SelectedItems = new List<TEntityModel> { value };
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

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets a value indicates the list is in read only.
        /// </summary>
        public bool IsReadOnly { get; set; }

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
        protected bool CanSelectAll(bool? value)
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

        #endregion

        #region SelectItem

        /// <summary>
        /// Can select an item. 
        /// </summary>
        /// <returns></returns>
        protected bool CanSelectItem(TEntityModel item)
        {
            return item != null && item.IsSelectable;
        }

        /// <summary>
        /// Select an item.
        /// </summary>
        public virtual void SelectItem(TEntityModel item)
        {
            if(item == null || !item.IsSelectable) return;

            item.IsSelected = true;
        }

        #endregion

        #region SelectItem

        /// <summary>
        /// Can select items. 
        /// </summary>
        /// <returns></returns>
        protected bool CanSelectItems(IEnumerable<TEntityModel> items)
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

        #endregion

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
            Items.CollectionChanged += ItemsCollectionChanged;
            SelectAllCommand.RaiseCanExecuteChanged();
        }

        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(() => AreAllSelected);
        }


        #endregion Properties Changed

        #endregion Methods
    }
}