using Microsoft.Practices.ObjectBuilder2;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Enums;
using My.CoachManager.Presentation.Prism.Core.Models;
using My.CoachManager.Presentation.Prism.Core.Models.Filters;
using My.CoachManager.Presentation.Prism.Core.Resources;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;
using Prism.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class SelectItemsViewModel<TModel> : DialogViewModel, ISelectItemsViewModel<TModel>
    where TModel : class, ISelectable
    {
        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets items.
        /// </summary>
        ICollection ISelectItemsViewModel.Items
        {
            get => Items;
            set => Items = new ObservableItemsCollection<TModel>((IEnumerable<TModel>)value);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public ObservableItemsCollection<TModel> Items { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        IList ISelectItemsViewModel.SelectedItems
        {
            get => SelectedItems as IList;
            set => SelectedItems = value as IList<TModel>;
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public IList<TModel> SelectedItems
        {
            get { return Items?.Where(x => x.IsSelected).ToList(); }
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
        object ISelectItemsViewModel.SelectedItem
        {
            get => SelectedItem;
            set => SelectedItem = value as TModel;
        }

        /// <inheritdoc />
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
        public IList<TModel> NotSelectableItems { get; set; }

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
        /// Gets or sets list parameters.
        /// </summary>
        public ListParametersViewModel Parameters { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets list filters parameters.
        /// </summary>
        public IListFiltersViewModel Filters { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets if we can refresh after initialisation.
        /// </summary>
        public override bool RefreshOnInit => true;

        /// <summary>
        /// Gets or sets select all command.
        /// </summary>
        public DelegateCommand<bool?> SelectAllCommand { get; private set; }

        /// <summary>
        /// Gets or sets select command.
        /// </summary>
        public DelegateCommand SelectCommand { get; private set; }

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
        public DelegateCommand<TModel> OpenCommand { get; set; }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ListViewModel{TEntityModel}"/>
        /// </summary>
        protected SelectItemsViewModel()
        {
            SelectionMode = SelectionMode.Single;
        }

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

            Title = ControlResources.SelectItems;
            Items = new ObservableItemsCollection<TModel>();
        }

        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all command property.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            SelectCommand = new DelegateCommand(Select, CanSelect);
            CancelCommand = new DelegateCommand(Cancel, CanCancel);
            SelectAllCommand = new DelegateCommand<bool?>(SelectAll, CanSelectAll);
            SelectItemCommand = new DelegateCommand<TModel>(SelectItem, CanSelectItem);
            SelectItemsCommand = new DelegateCommand<IEnumerable<TModel>>(SelectItems, CanSelectItems);
            OpenCommand = new DelegateCommand<TModel>(Open, CanOpen);
        }

        #endregion Initialization

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            Items = LoadItems();

            if (NotSelectableItems != null)
            {
                Items.ForEach(x =>
                {
                    x.IsSelectable = !NotSelectableItems.Contains(x);
                });
            }
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
        protected bool CanSelectAll(bool? value)
        {
            return Items.Any(x => x.IsSelectable) && SelectionMode == SelectionMode.Multiple;
        }

        /// <summary>
        /// Select or unselect all.
        /// </summary>
        protected virtual void SelectAll(bool? value)
        {
            if (SelectionMode == SelectionMode.Multiple)
                Items.Where(x => x.IsSelectable).ForEach(x =>
                {
                    if (value != null) x.IsSelected = value.Value;
                });
        }

        #endregion SelectAll

        #region Select

        /// <summary>
        /// Can Validate ?
        /// </summary>
        /// <returns></returns>
        protected bool CanSelect()
        {
            return SelectedItems.Any();
        }

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        public virtual void Select()
        {
            Close(DialogResult.Ok);
        }

        #endregion Select

        #region Open

        /// <summary>
        /// Open Item.
        /// </summary>
        protected virtual void Open(TModel item)
        {
            if (item != null)
            {
                SelectItem(item);
                Select();
            }
        }

        /// <summary>
        /// Can Open item.
        /// </summary>
        protected virtual bool CanOpen(TModel item)
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
            items?.ForEach(SelectItem);
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
            if (Filters != null)
            {
                Filters.Items = new FilteredCollectionView<TModel>(Items.ToObservableCollection());
            }
            Items.ForEach(x => x.SelectedChanged += Item_SelectedChanged);
            SelectAllCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Calls when selected item change.
        /// </summary>
        protected virtual void OnSelectionModeChanged()
        {
            Title = SelectionMode == SelectionMode.Multiple
                ? ControlResources.SelectItems
                : ControlResources.SelectItem;

            SelectAllCommand?.RaiseCanExecuteChanged();
            RaisePropertyChanged(() => AreAllSelected);
            RaisePropertyChanged(() => SelectedItems);
            RaisePropertyChanged(() => SelectedItem);
        }

        /// <summary>
        /// Calls when selection Changed.
        /// </summary>
        private void Item_SelectedChanged(object sender, EventArgs e)
        {
            if (sender is ISelectable selected && SelectionMode == SelectionMode.Single)
            {
                Items.ForEach(x => x.SelectedChanged -= Item_SelectedChanged);

                if (selected.IsSelected)
                {
                    var itemsToChange = SelectedItems.ToList();

                    itemsToChange.Where(x => !x.Equals(selected)).ForEach(x => x.IsSelected = false);
                }
                else
                {
                    if (!SelectedItems.Any())
                    {
                        selected.IsSelected = true;
                    }
                }

                Items.ForEach(x => x.SelectedChanged += Item_SelectedChanged);
            }

            OnSelectionChanged();
        }

        /// <summary>
        /// Calls when selection Changed.
        /// </summary>
        protected virtual void OnSelectionChanged()
        {
            SelectCommand.RaiseCanExecuteChanged();
            RaisePropertyChanged(() => AreAllSelected);
            RaisePropertyChanged(() => SelectedItems);
            RaisePropertyChanged(() => SelectedItem);
        }

        #endregion Properties Changed

        #endregion Methods
    }
}