using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Presentation.Core.Models;
using My.CoachManager.Presentation.Wpf.Core.Dialog;
using My.CoachManager.Presentation.Wpf.Core.Enums;
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
    public abstract class SelectItemsViewModel<TModel> : WorkspaceDialogViewModel, ISelectItemsViewModel<TModel>
        where TModel : class, ISelectable, IEntityModel, IModifiable, IValidatable, new()
    {
        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets items.
        /// </summary>
        ICollection IListViewModel.Items
        {
            get => Items;
            set => Items = new ObservableItemsCollection<TModel>((IEnumerable<TModel>)value);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public ObservableItemsCollection<TModel> Items { get; set; }

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
        public IEnumerable<TModel> DisplayedItems
        {
            get
            {
                if (Filters != null) return Filters.Items;

                return Items;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        IList IListViewModel.SelectedItems => SelectedItems as IList;

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public IEnumerable<TModel> SelectedItems
        {
            get { return Items?.Where(x => x.IsSelected).ToList(); }
            set
            {
                foreach (var x in Items)
                {
                    x.IsSelected = false;
                }

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
        object ISelectItemsViewModel.SelectedItem
        {
            get => SelectedItem;
            set => SelectedItem = value as TModel;
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public TModel SelectedItem
        {
            get => SelectedItems?.FirstOrDefault();
            set => SelectedItems = new List<TModel> { value };
        }

        /// <summary>
        /// Gets or sets selection mode.
        /// </summary>
        public SelectionMode SelectionMode { get; set; }

        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Gets or sets not selectionnable items.
        /// </summary>
        IList ISelectItemsViewModel.NotSelectableItems
        {
            get => NotSelectableItems as IList;
            set => NotSelectableItems = new ObservableItemsCollection<TModel>((IEnumerable<TModel>)value);
        }

        /// <summary>
        /// Gets or sets not selectionnable items.
        /// </summary>
        public IEnumerable<TModel> NotSelectableItems { get; set; }

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
        /// Gets list parameters.
        /// </summary>
        public ListParameters ListParameters { get; protected set; }

        /// <summary>
        /// Gets or sets list filters parameters.
        /// </summary>
        public IFiltersViewModel<TModel> Filters { get; protected set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets list filters parameters.
        /// </summary>
        IFiltersViewModel IListViewModel.Filters
        {
            get => Filters;
            set => Filters = value as IFiltersViewModel<TModel>;
        }

        /// <summary>
        /// Gets or sets select all command.
        /// </summary>
        public DelegateCommand<bool?> SelectAllCommand { get; private set; }

        /// <summary>
        /// Gets or sets select command.
        /// </summary>
        public DelegateCommand ValidateSelectionCommand { get; private set; }

        /// <summary>
        /// Gets or sets cancel command.
        /// </summary>
        public DelegateCommand CancelCommand { get; private set; }

        /// <summary>
        /// Gets or sets select command.
        /// </summary>
        public DelegateCommand<TModel> SelectItemCommand { get; private set; }

        /// <summary>
        /// Gets or sets select command.
        /// </summary>
        public DelegateCommand<IEnumerable<TModel>> SelectItemsCommand { get; private set; }

        /// <summary>
        /// Gets or sets the edit command.
        /// </summary>
        public DelegateCommand<TModel> OpenItemCommand { get; set; }

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

            SelectionMode = SelectionMode.Single;

            ValidateSelectionCommand = new DelegateCommand(ValidateSelection, CanValidateSelection);
            CancelCommand = new DelegateCommand(Cancel, CanCancel);
            SelectAllCommand = new DelegateCommand<bool?>(SelectAll, CanSelectAll);
            SelectItemCommand = new DelegateCommand<TModel>(SelectItem, CanSelectItem);
            SelectItemsCommand = new DelegateCommand<IEnumerable<TModel>>(SelectItems, CanSelectItems);
            OpenItemCommand = new DelegateCommand<TModel>(OpenItem, CanOpenItem);

            Title = ControlResources.SelectItems;
            Items = new ObservableItemsCollection<TModel>();
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

        /// <inheritdoc />
        /// <summary>
        /// Call before load data.
        /// </summary>
        protected override void OnLoadDataRequested()
        {
            SelectAll(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            Items = LoadItems();
        }

        /// <summary>
        /// Call after load data.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            if (NotSelectableItems != null)
            {
                foreach (var x in Items)
                {
                    x.IsSelectable = !NotSelectableItems.Contains(x);
                }
            }

            if (AllItemsCount == 0) AllItemsCount = Filters?.AllItemsCount ?? (Items?.Count ?? 0);
            if (Count == 0) Count = Filters?.FilteredItemsCount ?? (Items?.Count ?? 0);

            base.OnLoadDataCompleted();
        }

        /// <summary>
        /// Load items.
        /// </summary>
        /// <returns></returns>
        protected abstract ObservableItemsCollection<TModel> LoadItems();

        #endregion Data

        #region SelectAll

        /// <summary>
        /// Can Select All ?
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanSelectAll(bool? value)
        {
            return DisplayedItems != null && DisplayedItems.Any(x => x.IsSelectable) && SelectionMode == SelectionMode.Multiple;
        }

        /// <summary>
        /// Select or unselect all.
        /// </summary>
        protected virtual void SelectAll(bool? value)
        {
            if (SelectionMode == SelectionMode.Multiple)
            {
                if (DisplayedItems != null)
                {
                    foreach (var x in DisplayedItems.Where(x => x.IsSelectable))
                    {
                        if (value != null) x.IsSelected = value.Value;
                    }
                }
            }
        }

        #endregion SelectAll

        #region ValidateSelection

        /// <summary>
        /// Can Validate ?
        /// </summary>
        /// <returns></returns>
        protected bool CanValidateSelection()
        {
            return SelectedItems.Any();
        }

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        public virtual void ValidateSelection()
        {
            Close(DialogResult.Ok);
        }

        #endregion ValidateSelection

        #region OpenItem

        /// <summary>
        /// Open Item.
        /// </summary>
        protected virtual void OpenItem(TModel item)
        {
            if (item != null)
            {
                SelectItem(item);
                ValidateSelection();
            }
        }

        /// <summary>
        /// Can Open item.
        /// </summary>
        protected virtual bool CanOpenItem(TModel item)
        {
            return true;
        }

        #endregion Open

        #region SelectItem

        /// <summary>
        /// Can select an item.
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanSelectItem(TModel item)
        {
            return item != null && item.IsSelectable;
        }

        /// <summary>
        /// Select an item.
        /// </summary>
        public virtual void SelectItem(TModel item)
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
        protected virtual bool CanSelectItems(IEnumerable<TModel> items)
        {
            return items.Any(x => x.IsSelectable);
        }

        /// <summary>
        /// Select items.
        /// </summary>
        public virtual void SelectItems(IEnumerable<TModel> items)
        {
            var enumerable = items as IList<TModel> ?? items.ToList();

            foreach (var x in enumerable)
            {
                SelectItem(x);
            }
        }

        #endregion SelectItems

        #region Cancel

        /// <summary>
        /// Can Cancel ?
        /// </summary>
        /// <returns></returns>
        protected bool CanCancel()
        {
            return true;
        }

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        public virtual void Cancel()
        {
            Close(DialogResult.Cancel);
        }

        #endregion Cancel

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
        /// Calls when selected item change.
        /// </summary>
        protected virtual void OnSelectionModeChanged()
        {
            Title = SelectionMode == SelectionMode.Multiple
                ? ControlResources.SelectItems
                : ControlResources.SelectItem;

            OnSelectionChanged();
        }

        /// <summary>
        /// Calls when selection Changed.
        /// </summary>
        private void Item_SelectedChanged(object sender, EventArgs e)
        {
            if (sender is ISelectable selected && SelectionMode == SelectionMode.Single)
            {
                foreach (var x in Items)
                {
                    x.SelectedChanged -= Item_SelectedChanged;
                }

                if (selected.IsSelected)
                {
                    var itemsToChange = SelectedItems.ToList();

                    foreach (var x in itemsToChange.Where(x => !x.Equals(selected)))
                    {
                        x.IsSelected = false;
                    }
                }
                else
                {
                    if (!SelectedItems.Any())
                    {
                        selected.IsSelected = true;
                    }
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
            ValidateSelectionCommand?.RaiseCanExecuteChanged();
        }

        #endregion Properties Changed

        #endregion Methods
    }
}