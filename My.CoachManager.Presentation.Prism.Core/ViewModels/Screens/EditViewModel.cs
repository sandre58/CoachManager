using System;
using System.Threading.Tasks;
using System.Windows.Input;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Entities;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class EditViewModel<TEntityViewModel> : WorkspaceDialogViewModel, IEditViewModel
        where TEntityViewModel : class, IEntityViewModel, IValidatable, IModifiable, new()
    {
        #region Fields

        private TEntityViewModel _item;
        private int _activeId;

        #endregion Fields

        #region Members

        /// <summary>
        /// Get or set Item.
        /// </summary>
        public TEntityViewModel Item
        {
            get
            {
                return _item;
            }
            protected set
            {
                SetProperty(ref _item, value);
            }
        }

        /// <summary>
        /// Get or Set Save Command.
        /// </summary>
        public DelegateCommand SaveCommand { get; set; }

        /// <summary>
        /// Get or Set Cancel Command.
        /// </summary>
        public DelegateCommand CancelCommand { get; set; }

        /// <summary>
        /// Get or Set Refresh Command.
        /// </summary>
        public ICommand RefreshCommand { get; set; }

        #endregion Members

        #region Methods

        #region Initialization

        /// <summary>
        /// Initializes data in constructor.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            Mode = ScreenMode.Creation;
            Item = new TEntityViewModel();
        }

        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void InitializeCommands()
        {
            base.InitializeCommands();

            SaveCommand = new DelegateCommand(SaveAsync, CanSave);
            CancelCommand = new DelegateCommand(Cancel, CanCancel);
            RefreshCommand = new DelegateCommand(Refresh, CanRefresh);
        }

        #endregion Initialization

        #region Save

        /// <summary>
        /// Load data.
        /// </summary>
        /// <returns></returns>
        private async Task SaveDataAsync()
        {
            var result = false;
            var task = Task.Run(() =>
            {
                result = SaveItemCore();
            });

            State = ScreenState.Saving;

            OnSaveRequested();

            if (Item.IsValid())
            {
                try
                {
                    await task.ConfigureAwait(true);
                }
                catch (Exception)
                {
                    // ignored
                }

                ResetModified();
                State = ScreenState.Ready;
                Mode = ScreenMode.Edition;

                // Is Cancelled
                if (task.IsCanceled)
                {
                }

                // Exception
                else if (task.IsFaulted)
                {
                    if (task.Exception != null)
                    {
                        if (task.Exception.InnerException is BusinessException)
                        {
                            OnBusinessExceptionOccured((BusinessException)task.Exception.InnerException);
                        }
                        else
                        {
                            OnExceptionOccured(task.Exception.InnerException);
                        }
                    }
                }

                // Is Completed
                else if (task.IsCompleted)
                {
                    if (result)
                        OnSaveCompleted();
                }
            }
            else
            {
                OnBusinessExceptionOccured(new BusinessException(MessageResources.InvalidForm));
            }
        }

        /// <summary>
        /// Add a new item.
        /// </summary>
        public virtual async void SaveAsync()
        {
            if (CanSave())
            {
                await SaveDataAsync();
            }
        }

        /// <summary>
        /// Can save item.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanSave()
        {
            return true;
        }

        /// <summary>
        /// Save.
        /// </summary>
        protected abstract bool SaveItemCore();

        /// <summary>
        /// Before Save.
        /// </summary>
        protected virtual void OnSaveRequested()
        {
        }

        /// <summary>
        /// After Save.
        /// </summary>
        protected virtual void OnSaveCompleted()
        {
            Locator.DialogService.ShowSuccessPopup(MessageResources.SavingSuccess);
            Close(DialogResult.Ok);
        }

        #endregion Save

        #region Cancel

        /// <summary>
        /// Cancel modification.
        /// </summary>
        public virtual void Cancel()
        {
            if (CanCancel())
            {
                if (Item.IsModified)
                {
                    Locator.DialogService.ShowWarningDialog(MessageResources.CancelModifications, dialog =>
                    {
                        OnCancelCompleted(dialog.Result);
                    }, MessageDialogType.YesNo);
                }
                else
                {
                    OnCancelCompleted(DialogResult.Yes);
                }
            }
        }

        /// <summary>
        /// Can cancel item.
        /// </summary>
        public virtual bool CanCancel()
        {
            return true;
        }

        /// <summary>
        /// Called after the cancel action.
        /// </summary>
        /// <param name="result">The dialog result.</param>
        protected virtual void OnCancelCompleted(DialogResult result)
        {
            if (result == DialogResult.Yes) Close(DialogResult.Cancel);
        }

        #endregion Cancel

        #region Refresh

        /// <summary>
        /// Refresh Items.
        /// </summary>
        public virtual void Refresh()
        {
            RefreshDataAsync();
        }

        /// <summary>
        /// Can refresh item.
        /// </summary>
        public virtual bool CanRefresh()
        {
            return true;
        }

        #endregion Refresh

        #region Data

        /// <summary>
        /// Load an item by id.
        /// </summary>
        public void LoadItemById(int id)
        {
            _activeId = id;
            Mode = ScreenMode.Edition;
            RefreshDataAsync();
        }

        /// <summary>
        /// Save.
        /// </summary>
        protected override void LoadDataCore()
        {
            if (_activeId > 0)
            {
                Item = LoadItemCore(_activeId);
            }
            else
            {
                Item = new TEntityViewModel();
            }
        }

        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected abstract TEntityViewModel LoadItemCore(int id);

        #endregion Data

        #region Properties Changed

        /// <summary>
        /// Calls when Item changes.
        /// </summary>
        protected virtual void OnItemChanged()
        {
            if (Item != null) _activeId = Item.Id;

            RaisePropertyChanged(() => Title);

            if (SaveCommand != null) SaveCommand.RaiseCanExecuteChanged();
            if (CancelCommand != null) CancelCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Called when mode changes.
        /// </summary>
        protected virtual void OnModeChanged()
        {
            Title = Mode == ScreenMode.Edition ? string.Format(MessageResources.EditItem, Title) : string.Format(MessageResources.CreateItem, Title);
        }

        /// <summary>
        /// Reset modified property.
        /// </summary>
        public override void ResetModified()
        {
            Item.ResetModified();
            base.ResetModified();
        }

        #endregion Properties Changed

        #endregion Methods
    }
}