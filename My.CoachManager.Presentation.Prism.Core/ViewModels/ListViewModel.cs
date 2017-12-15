using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Interactivity;
using My.CoachManager.Presentation.Prism.Core.Services;
using Prism.Commands;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class
        ListViewModel<TEntityViewModel, TEditView, TEditViewModel> : ReadOnlyListViewModel<TEntityViewModel>
        where TEntityViewModel : class, IEntityViewModel, INotifyPropertyChanged
        where TEditView : FrameworkElement
        where TEditViewModel : class, IDialogViewModel, IEditViewModel
    {
        #region Constructor

        /// <summary>
        /// Initialise a new instance of <see cref="ListViewModel{TEntityViewModel,TEditView,TEditViewModel}"/>.
        /// </summary>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="eventAggregator"></param>
        /// <param name="logger">The logger.</param>
        protected ListViewModel(IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger)
            : base(dialogService, eventAggregator, logger)
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

        #region Add

        /// <summary>
        /// Add a new item.
        /// </summary>
        public virtual void Add()
        {
            DialogService.ShowWorkspaceDialog<TEditView>(null, dialog =>
            {
                OnAfterAddItem(dialog.Result);
            });
        }

        /// <summary>
        /// Can add a new item.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanAdd()
        {
            return Mode == ScreenMode.Read;
        }

        /// <summary>
        /// Called after the Add action.
        /// </summary>
        /// <param name="result"></param>
        protected virtual void OnAfterAddItem(DialogResult result)
        {
            if (result == DialogResult.Ok) RefreshData();
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
                DialogService.ShowWorkspaceDialog<TEditView>(before =>
                    {
                        var vm = before.Context as TEditViewModel;
                        OnBeforeEditItem(item, vm);
                    },
                    after =>
                    {
                        OnAfterEditItem(item, after.Result);
                    });
            }
        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        public virtual bool CanEdit(TEntityViewModel item)
        {
            return Mode == ScreenMode.Read && item != null;
        }

        /// <summary>
        /// Called before the edit action;
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="viewModel">The view model.</param>
        protected virtual void OnBeforeEditItem(TEntityViewModel item, TEditViewModel viewModel)
        {
            if (viewModel != null) viewModel.LoadItemById(item.Id);
        }

        /// <summary>
        /// Called after the edit action.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="result">The dialog result.</param>
        protected virtual void OnAfterEditItem(TEntityViewModel item, DialogResult result)
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
                    DialogService.ShowQuestionDialog(MessageResources.ConfirmationRemovingItem, dialog =>
                    {
                        if (dialog.Result == DialogResult.Yes)
                        {
                            try
                            {
                                RemoveItemCore(item);
                                OnAfterRemoveItem(item);
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
        protected virtual void OnAfterRemoveItem(TEntityViewModel item)
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
                switch (e.EventArgs.Key)
                {
                    case Key.Enter:
                        Edit((TEntityViewModel)e.Item);
                        e.EventArgs.Handled = true;
                        break;

                    case Key.Delete:
                        Remove((TEntityViewModel)e.Item);
                        e.EventArgs.Handled = true;
                        break;
                }
            }
        }

        #endregion Keyboard

        /// <summary>
        /// Calls when selected item change.
        /// </summary>
        protected override void OnSelectedItemChanged()
        {
            EditCommand.RaiseCanExecuteChanged();
            RemoveCommand.RaiseCanExecuteChanged();
        }

        #endregion Methods
    }
}