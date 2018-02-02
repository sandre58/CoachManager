using System;
using System.ComponentModel;
using System.Windows.Input;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Interactivity;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Entities;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class
        ListViewModel<TEntityViewModel, TEditViewModel> : ReadOnlyListViewModel<TEntityViewModel>
        where TEntityViewModel : class, IEntityViewModel, INotifyPropertyChanged
        where TEditViewModel : class, IDialogViewModel, IEditViewModel
    {
        #region Constructor

        /// <summary>
        /// Initialise a new instance of <see cref="ListViewModel{TEntityViewModel,TEditViewModel}"/>.
        /// </summary>
        protected ListViewModel()
        {
            AddCommand = new DelegateCommand(Add, CanAdd);
            RemoveCommand = new DelegateCommand<TEntityViewModel>(Remove, CanRemove);
            EditCommand = new DelegateCommand<TEntityViewModel>(Edit, CanEdit);
            RefreshCommand = new DelegateCommand(Refresh, CanRefresh);
            KeyboardActionCommand = new DelegateCommand<KeyDownItemEventArgs>(KeyboardAction, CanKeyboardAction);
        }

        #endregion Constructor

        #region Members

        /// <summary>
        /// Gets or sets the add command.
        /// </summary>
        public DelegateCommand AddCommand { get; set; }

        /// <summary>
        /// Gets or sets the edit command.
        /// </summary>
        public DelegateCommand<TEntityViewModel> EditCommand { get; set; }

        /// <summary>
        /// Gets or sets the remove command.
        /// </summary>
        public DelegateCommand<TEntityViewModel> RemoveCommand { get; set; }

        #endregion Members

        #region Methods

        /// <summary>
        /// Get Item View Type.
        /// </summary>
        /// <returns></returns>
        protected override Type GetItemViewType()
        {
            return null;
        }

        #region Add

        /// <summary>
        /// Add a new item.
        /// </summary>
        public virtual void Add()
        {
            Locator.DialogService.ShowWorkspaceDialog(GetEditViewType(), null, dialog =>
            {
                OnAddCompleted(dialog.Result);
            });
        }

        /// <summary>
        /// Can add a new item.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanAdd()
        {
            return Mode == ScreenMode.Read && GetEditViewType() != null;
        }

        /// <summary>
        /// Called after the Add action.
        /// </summary>
        /// <param name="result"></param>
        protected virtual void OnAddCompleted(DialogResult result)
        {
            if (result == DialogResult.Ok) RefreshData();
        }

        #endregion Add

        #region Edit

        /// <summary>
        /// Get Item View Type.
        /// </summary>
        /// <returns></returns>
        protected virtual Type GetEditViewType()
        {
            return null;
        }

        /// <summary>
        /// Edit Item.
        /// </summary>
        public virtual void Edit(TEntityViewModel item)
        {
            if (CanEdit(item))
            {
                Locator.DialogService.ShowWorkspaceDialog(GetEditViewType(), before =>
                    {
                        var vm = before.Context as TEditViewModel;
                        OnEditRequested(item, vm);
                    },
                    after =>
                    {
                        OnEditCompleted(item, after.Result);
                    });
            }
        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        public virtual bool CanEdit(TEntityViewModel item)
        {
            return Mode == ScreenMode.Read && item != null && GetEditViewType() != null;
        }

        /// <summary>
        /// Called before the edit action;
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="viewModel">The view model.</param>
        protected virtual void OnEditRequested(TEntityViewModel item, TEditViewModel viewModel)
        {
            if (viewModel != null) viewModel.LoadItemById(item.Id);
        }

        /// <summary>
        /// Called after the edit action.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="result">The dialog result.</param>
        protected virtual void OnEditCompleted(TEntityViewModel item, DialogResult result)
        {
            if (result == DialogResult.Ok) RefreshData();
        }

        #endregion Edit

        #region Remove

        /// <summary>
        /// Remove Item.
        /// </summary>
        public virtual void Remove(TEntityViewModel item)
        {
            if (CanRemove(item))
            {
                if (item != null)
                {
                    Locator.DialogService.ShowQuestionDialog(MessageResources.ConfirmationRemovingItem, dialog =>
                    {
                        if (dialog.Result == DialogResult.Yes)
                        {
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
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Removes the item from the data source.
        /// </summary>
        /// <param name="item"></param>
        protected abstract void RemoveItemCore(TEntityViewModel item);

        /// <summary>
        /// Can Edit item.
        /// </summary>
        public virtual bool CanRemove(TEntityViewModel item)
        {
            return Mode == ScreenMode.Read && item != null;
        }

        /// <summary>
        /// Called after the edit action;
        /// </summary>
        /// <param name="item">The item.</param>
        protected virtual void OnRemoveCompleted(TEntityViewModel item)
        {
            RefreshData();
        }

        #endregion Remove

        #region Keyboard

        /// <summary>
        /// Do action by keyboard trigger.
        /// </summary>
        public override void KeyboardAction(KeyDownItemEventArgs e)
        {
            if (CanKeyboardAction(e))
            {
                if (e.EventArgs.Key == Key.Enter)
                {
                    if (GetItemViewType() == null)
                    {
                        Edit((TEntityViewModel)e.Item);
                        e.EventArgs.Handled = true;
                        return;
                    }
                }

                switch (e.EventArgs.Key)
                {
                    case Key.Delete:
                        Remove((TEntityViewModel)e.Item);
                        e.EventArgs.Handled = true;
                        break;

                    default:
                        base.KeyboardAction(e);
                        break;
                }
            }
        }

        #endregion Keyboard

        /// <summary>
        /// Calls when selected item change.
        /// </summary>
        protected virtual void OnSelectedItemChanged()
        {
            EditCommand.RaiseCanExecuteChanged();
            RemoveCommand.RaiseCanExecuteChanged();
        }

        #endregion Methods
    }
}