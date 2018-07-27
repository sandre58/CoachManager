using System;
using System.ComponentModel;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Models;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class
        ListViewModel<TEntityViewModel> : ReadOnlyListViewModel<TEntityViewModel>
        where TEntityViewModel : class, IEntityModel, IModifiable, IValidatable, INotifyPropertyChanged, new()
    {
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

        #region Abstract Methods

        /// <summary>
        /// Get Item View Type.
        /// </summary>
        /// <returns></returns>
        protected abstract Type GetEditViewType();

        /// <summary>
        /// Get Item View Type.
        /// </summary>
        /// <returns></returns>
        protected override Type GetItemViewType()
        {
            return null;
        }

        #endregion Abstract Methods

        #region Initialization

        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            AddCommand = new DelegateCommand(Add, CanAdd);
            RemoveCommand = new DelegateCommand<TEntityViewModel>(Remove, CanRemove);
            EditCommand = new DelegateCommand<TEntityViewModel>(Edit, CanEdit);
            RefreshCommand = new DelegateCommand(Refresh, CanRefresh);
        }

        #endregion Initialization

        #region Add

        /// <summary>
        /// Add a new item.
        /// </summary>
        public virtual void Add()
        {
            DialogService.ShowWorkspaceDialog(GetEditViewType(), null, null, dialog =>
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
            if (result == DialogResult.Ok) RefreshDataAsync();
        }

        #endregion Add

        #region Edit

        /// <summary>
        /// Edit Item.
        /// </summary>
        public virtual void Edit(TEntityViewModel item)
        {
            if (CanEdit(item))
            {
                DialogService.ShowWorkspaceDialog(GetEditViewType(), null, before =>
                    {
                        var vm = before.Context as IEditViewModel<TEntityViewModel>;
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
        protected virtual void OnEditRequested(TEntityViewModel item, IEditViewModel<TEntityViewModel> viewModel)
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
            if (result == DialogResult.Ok) RefreshDataAsync();
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
                    DialogService.ShowQuestionDialog(MessageResources.ConfirmationRemovingItem, dialog =>
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