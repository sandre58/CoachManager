using System;
using System.Threading.Tasks;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Models;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class EditViewModel<TModel> : WorkspaceDialogViewModel, IEditViewModel<TModel>
        where TModel : class, IEntityModel, IValidatable, IModifiable, new()
    {
        #region Fields
        
        private int _activeId;

        #endregion Fields

        #region Members

        /// <summary>
        /// Get or set Item.
        /// </summary>
        public TModel Item { get; set; }

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
        public DelegateCommand RefreshCommand { get; set; }

        #endregion Members

        #region Methods

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes data in constructor.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            Mode = ScreenMode.Creation;
            Item = new TModel();
        }

        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

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
                        var exception = task.Exception.InnerException as BusinessException;
                        if (exception != null)
                        {
                            OnBusinessExceptionOccured(exception);
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
        protected virtual async void SaveAsync()
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
        protected virtual bool CanSave()
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
            DialogService.ShowSuccessPopup(MessageResources.SavingSuccess);
            Close(DialogResult.Ok);
        }

        #endregion Save

        #region Cancel

        /// <summary>
        /// Cancel modification.
        /// </summary>
        public virtual void Cancel()
        {
            if (!CanCancel()) return;
            if (Item.IsModified)
            {
                DialogService.ShowWarningDialog(MessageResources.CancelModifications, dialog =>
                {
                    OnCancelCompleted(dialog.Result);
                }, MessageDialogType.YesNo);
            }
            else
            {
                OnCancelCompleted(DialogResult.Yes);
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
        protected virtual void Refresh()
        {
            RefreshDataAsync();
        }

        /// <summary>
        /// Can refresh item.
        /// </summary>
        protected virtual bool CanRefresh()
        {
            return true;
        }

        #endregion Refresh

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Load an item by id.
        /// </summary>
        public void LoadItemById(int id)
        {
            _activeId = id;
            Mode = ScreenMode.Edition;
            RefreshDataAsync();
        }

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override void LoadDataCore()
        {
            Item = _activeId > 0 ? LoadItemCore(_activeId) : new TModel();
        }

        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected abstract TModel LoadItemCore(int id);

        #endregion Data

        #region Properties Changed

        /// <summary>
        /// Calls when Item changes.
        /// </summary>
        protected virtual void OnItemChanged()
        {
            if (Item != null) _activeId = Item.Id;

            RaisePropertyChanged(() => Title);

            SaveCommand?.RaiseCanExecuteChanged();
            CancelCommand?.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Called when mode changes.
        /// </summary>
        protected virtual void OnModeChanged()
        {
            Title = Mode == ScreenMode.Edition ? string.Format(MessageResources.EditItem, Title) : string.Format(MessageResources.CreateItem, Title);
        }

        #endregion Properties Changed

        #endregion Methods
    }
}