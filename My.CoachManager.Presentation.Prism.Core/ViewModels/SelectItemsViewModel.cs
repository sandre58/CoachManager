using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Models;
using My.CoachManager.Presentation.Prism.Core.Models.Filters;
using My.CoachManager.Presentation.Prism.Core.Resources;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{

    public abstract class SelectItemsViewModel<TModel> : DialogViewModel, ISelectItemsViewModel<TModel>
    where TModel : ISelectable
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

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public IEnumerable<TModel> SelectedItems {
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
        public TModel SelectedItem {
            get => SelectedItems != null ? SelectedItems.FirstOrDefault() : default(TModel);
            set => SelectedItems = new List<TModel> {value};
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

        #endregion

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

        #endregion

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

        #endregion

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
            Items.CollectionChanged += ItemsCollectionChanged;
            SelectAllCommand.RaiseCanExecuteChanged();
        }

        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(() => AreAllSelected);
            SelectCommand.RaiseCanExecuteChanged();
        }


        #endregion Properties Changed

        #endregion Methods
    }
}