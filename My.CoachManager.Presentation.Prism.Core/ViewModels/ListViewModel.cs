using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.Models;
using My.CoachManager.Presentation.Prism.Core.Resources;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class ListViewModel<TEntityModel, TEditView, TItemView> : NavigatableWorkspaceViewModel, IListViewModel<TEntityModel>
        where TEntityModel : class, IEntityModel, IModifiable, IValidatable, new()
        where TEditView : FrameworkElement
        where TItemView : FrameworkElement
    {
        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets items.
        /// </summary>
        ICollection IListViewModel.Items
        {
            get => Items;
            set => Items = new ObservableCollection<TEntityModel>((IEnumerable<TEntityModel>)value);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public ObservableCollection<TEntityModel> Items { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public TEntityModel SelectedItem { get; set; }

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
        /// Gets or sets list parameters.
        /// </summary>
        public ListFiltersViewModel Filters { get; set; }

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
        protected override void InitializeData()
        {
            base.InitializeData();

            Items = new ObservableCollection<TEntityModel>();

            IsReadOnly = false;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void InitializeShortcuts()
        {
            base.InitializeShortcuts();

            KeyboardShortcuts.Add(new KeyBinding(AddCommand, Key.N, ModifierKeys.Control));
            KeyboardShortcuts.Add(new KeyBinding(OpenCommand, Key.Enter, ModifierKeys.None));
            KeyboardShortcuts.Add(new KeyBinding(EditCommand, Key.F2, ModifierKeys.None));
            KeyboardShortcuts.Add(new KeyBinding(RemoveCommand, Key.Delete, ModifierKeys.None));
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
            if (result == DialogResult.Ok) RefreshDataAsync();
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
            if (result == DialogResult.Ok) RefreshDataAsync();
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
            RefreshDataAsync();
        }

        #endregion Remove

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