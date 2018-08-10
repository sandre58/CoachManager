using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.Models;
using My.CoachManager.Presentation.Prism.Core.Resources;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class EditViewModel<TModel> : WorkspaceDialogViewModel, IEditViewModel<TModel>
        where TModel : class, IEntityModel, IValidatable, IModifiable, INotifyPropertyChanged, new()
    {
        #region Fields

        private int _activeId;

        #endregion Fields

        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets item.
        /// </summary>
        IEntityModel IItemViewModel.Item
        {
            get => Item;
            set => Item = (TModel) value;
        }
        
        /// <inheritdoc />
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

        /// <inheritdoc />
        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            SaveCommand = new DelegateCommand(SaveAsync, CanSave);
            CancelCommand = new DelegateCommand(Cancel, CanCancel);
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void InitializeShortcuts()
        {
            base.InitializeShortcuts();

            KeyboardShortcuts.Add(new KeyBinding(SaveCommand, Key.S, ModifierKeys.Control));
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

            State = ScreenState.Saving;

            OnSaveRequested();

            if (Item.Validate())
            {
                var task = Task.Factory.StartNew(() =>
                {
                    result = SaveItemCore();
                }, CancellationToken.None
                        , TaskCreationOptions.None
                        , TaskScheduler.FromCurrentSynchronizationContext())

                    // Error
                    .ContinueWith(t =>
                    {
                        if (t.Exception == null) return;
                        if (t.Exception.InnerException is ValidationBusinessException validationBusinessException)
                        {
                            foreach (var error in validationBusinessException.Errors)
                            {
                                OnBusinessExceptionOccured(new BusinessException(error.ToString()));
                            }
                        }
                        else if (t.Exception.InnerException is BusinessException businessException)
                        {
                            OnBusinessExceptionOccured(businessException);
                        }
                        else
                        {
                            OnExceptionOccured(t.Exception.InnerException);
                        }
                    }, TaskContinuationOptions.OnlyOnFaulted)

                    // Cancel
                    .ContinueWith(t =>
                    {
                    }, TaskContinuationOptions.OnlyOnCanceled)

                    // Success
                    .ContinueWith(t =>
                    {
                        if (result)
                        {
                            OnSaveCompleted();
                            Mode = ScreenMode.Edition;
                        }
                    }, TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously)

                    .ContinueWith(t =>
                    {
                        State = ScreenState.Ready;
                    });

                await task;
            }
            else
            {
                State = ScreenState.Ready;

                foreach (var error in Item.GetErrors())
                {
                    OnBusinessExceptionOccured(new BusinessException(error.ToString()));
                }
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
            NotificationManager.ShowSuccess(MessageResources.SavingSuccess);
            Close(DialogResult.Ok);
        }

        #endregion Save

        #region Cancel

        /// <summary>
        /// Cancel modification.
        /// </summary>
        protected virtual void Cancel()
        {
            if (!CanCancel()) return;
            if (!Item.IsModified() || DialogManager.ShowWarningDialog(MessageResources.CancelModifications, MessageDialogButtons.YesNo) == DialogResult.Yes)
            {
                Close(DialogResult.Cancel);
            }
        }

        /// <summary>
        /// Can cancel item.
        /// </summary>
        protected virtual bool CanCancel()
        {
            return true;
        }

        #endregion Cancel

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Load an item by id.
        /// </summary>
        public void LoadItemById(int id)
        {
            _activeId = id;
            Mode = id == 0 ? ScreenMode.Creation : ScreenMode.Edition;
            RefreshDataAsync();
        }

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override void LoadDataCore()
        {
            if(Item == null || Item.Id != _activeId)
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

            Item?.ResetModified();
            if (Item != null) Item.PropertyChanged += OnItemPropertyChanged;
            SaveCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Calls when Item property changes.
        /// </summary>
        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Called when mode changes.
        /// </summary>
        protected override void OnModeChanged()
        {
            Title = Mode == ScreenMode.Edition ? ControlResources.Edition : ControlResources.Creation;
        }

        #endregion Properties Changed

        #endregion Methods
    }
}