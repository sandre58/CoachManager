using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Filters;
using My.CoachManager.Presentation.Prism.Core.Models;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class ListViewModel<TEntityModel> : NavigatableWorkspaceViewModel
        where TEntityModel : class, IEntityModel, IModifiable, IValidatable, new()
    {
        #region Fields

        private ObservableCollection<TEntityModel> _items;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public ObservableCollection<TEntityModel> Items
        {
            get { return _items; }
            set
            {
                if (_items == null)
                {
                    _items = new ObservableCollection<TEntityModel>();
                }

                _items.Clear();
                _items.AddRange(value);

                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public FilteredCollectionView<TEntityModel> FilteredItems { get; private set; }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public TEntityModel SelectedItem { get; set; }

        /// <summary>
        /// Gets or sets a value indicates the list is in read only.
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Gets or sets the columns to displayed.
        /// </summary>
        public ObservableCollection<string> DisplayedColumns { get; set; }

        /// <summary>
        /// Gets or sets the preset columns to displayed.
        /// </summary>
        public Dictionary<object, string[]> PresetColumns { get; private set; }

        /// <summary>
        /// Gets or sets the add command.
        /// </summary>
        public DelegateCommand AddCommand { get; set; }

        /// <summary>
        /// Gets or sets the edit command.
        /// </summary>
        public DelegateCommand<TEntityModel> EditCommand { get; set; }

        /// <summary>
        /// Gets or sets the remove command.
        /// </summary>
        public DelegateCommand<TEntityModel> RemoveCommand { get; set; }

        /// <summary>
        /// Command to change displayed columns.
        /// </summary>
        public DelegateCommand<object> ChangeDisplayedColumnsCommand { get; set; }

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
            ChangeDisplayedColumnsCommand = new DelegateCommand<object>(ChangeDisplayedColumns);
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            Items = new ObservableCollection<TEntityModel>();
            FilteredItems = new FilteredCollectionView<TEntityModel>(Items);

            PresetColumns = new Dictionary<object, string[]>();
            IsReadOnly = false;
        }

        #endregion Initialization

        #region Open

        /// <summary>
        /// Edit Item.
        /// </summary>
        protected virtual void Open(TEntityModel item)
        {
            if (CanOpen(item))
            {
                //ServiceLocator.Current.TryResolve<INavigationService>().NavigateTo(GetItemViewType(), item.Id);
            }
        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        protected virtual bool CanOpen(TEntityModel item)
        {
            return Mode == ScreenMode.Read && item != null;
        }

        #endregion Open

        #region Add

        /// <summary>
        /// Add a new item.
        /// </summary>
        protected virtual void Add()
        {
            //DialogService.ShowWorkspaceDialog(GetEditViewType(), null, null, dialog =>
            //{
            //    OnAddCompleted(dialog.Result);
            //});
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
            if (result == DialogResult.Ok) RefreshDataAsync();
        }

        #endregion Add

        #region Edit

        /// <summary>
        /// Edit Item.
        /// </summary>
        protected virtual void Edit(TEntityModel item)
        {
            if (CanEdit(item))
            {
                //DialogService.ShowWorkspaceDialog(GetEditViewType(), null, before =>
                //    {
                //        var vm = before.Context as IEditViewModel<TEntityViewModel>;
                //        OnEditRequested(item, vm);
                //    },
                //    after =>
                //    {
                //        OnEditCompleted(item, after.Result);
                //    });
            }
        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        protected virtual bool CanEdit(TEntityModel item)
        {
            return Mode == ScreenMode.Read && item != null && !IsReadOnly;
        }

        /// <summary>
        /// Called before the edit action;
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="viewModel">The view model.</param>
        protected virtual void OnEditRequested(TEntityModel item, IEditViewModel<TEntityModel> viewModel)
        {
            viewModel?.LoadItemById(item.Id);
        }

        /// <summary>
        /// Called after the edit action.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="result">The dialog result.</param>
        protected virtual void OnEditCompleted(TEntityModel item, DialogResult result)
        {
            if (result == DialogResult.Ok) RefreshDataAsync();
        }

        #endregion Edit

        #region Remove

        /// <summary>
        /// Remove Item.
        /// </summary>
        protected virtual void Remove(TEntityModel item)
        {
            if (!CanRemove(item)) return;

            if (item != null)
            {
                DialogService.ShowQuestionDialog(MessageResources.ConfirmationRemovingItem, dialog =>
                {
                    if (dialog.Result != DialogResult.Yes) return;

                    try
                    {
                        RemoveItemCore(item);
                        OnRemoveCompleted(item);
                    }
                    catch (BusinessException e)
                    {
                        OnBusinessExceptionOccured(e);
                    }
                    catch (Exception e)
                    {
                        OnExceptionOccured(e);
                    }
                });
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
            RefreshDataAsync();
        }

        #endregion Remove

        #region Columns Management

        /// <summary>
        /// Changes displayed columns.
        /// </summary>
        protected void ChangeDisplayedColumns(object type)
        {
            if (type != null)
                DisplayedColumns = new ObservableCollection<string>(PresetColumns[type]);
        }

        /// <summary>
        /// Add a preset columns.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="columns"></param>
        protected void AddPresetColumns(object key, string[] columns)
        {
            PresetColumns?.Add(key, columns);
        }

        #endregion Columns Management

        #region Properties Changed

        /// <summary>
        /// Calls when selected item change.
        /// </summary>
        protected virtual void OnSelectedItemChanged()
        {
            EditCommand.RaiseCanExecuteChanged();
            RemoveCommand.RaiseCanExecuteChanged();
        }

        #endregion Properties Changed

        #endregion Methods
    }
}