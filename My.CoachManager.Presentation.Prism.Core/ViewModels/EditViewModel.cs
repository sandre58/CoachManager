using System;
using System.ComponentModel;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.Presentation.Prism.Core.ComponentModel;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.Models;
using My.CoachManager.Presentation.Prism.Core.Resources;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class EditViewModel<TModel> : WorkspaceDialogViewModel, IEditViewModel<TModel>
        where TModel : class, IEntityModel, IValidatable, IModifiable, INotifyPropertyChanged, new()
    {
        #region Fields

        private int _activeId;

        /// <summary>
        /// The laod data background worker.
        /// </summary>
        private AbortableBackgroundWorker _saveDataBackgroundWorker;

        #endregion Fields

        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets item.
        /// </summary>
        IEntityModel IItemViewModel.Item
        {
            get => Item;
            set => Item = (TModel)value;
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

            Item = new TModel();
            Mode = ScreenMode.Creation;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            SaveCommand = new DelegateCommand(Save, CanSave);
            CancelCommand = new DelegateCommand(Cancel, CanCancel);
        }

        #endregion Initialization

        #region Save

        /// <summary>
        /// Load data.
        /// </summary>
        /// <returns></returns>
        private void SaveCore()
        {
            State = ScreenState.Saving;

            OnSaveRequested();

            if (Item.Validate())
            {
                _saveDataBackgroundWorker =
                    new AbortableBackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
                _saveDataBackgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
                _saveDataBackgroundWorker.DoWork += OnBackgroundWorkerOnDoWork;

                // Start the background worker
                _saveDataBackgroundWorker.RunWorkerAsync();
            }
            else
            {
                State = ScreenState.Ready;

                foreach (var error in Item.GetErrors())
                {
                    OnBusinessExceptionOccured(new BusinessException(error));
                }
            }
        }

        /// <summary>
        /// Called when [background worker run worker completed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                OnSaveCompleted();
                Mode = ScreenMode.Edition;
            }
            State = ScreenState.Ready;
        }

        /// <summary>
        /// Called when [background worker on do work].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="doWorkEventArgs">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        private void OnBackgroundWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            try
            {
                var result = SaveItemCore();
                if (!result) _saveDataBackgroundWorker.Abort();
            }
            catch (Exception exception)
            {
                if (exception.InnerException is ValidationBusinessException validationBusinessException)
                {
                    foreach (var error in validationBusinessException.Errors)
                    {
                        OnBusinessExceptionOccured(new BusinessException(error.ToString()));
                    }
                }
                else if (exception.InnerException is BusinessException businessException)
                {
                    OnBusinessExceptionOccured(businessException);
                }
                else
                {
                    OnExceptionOccured(exception.InnerException ?? exception);
                }
            }
        }

        /// <summary>
        /// Add a new item.
        /// </summary>
        protected virtual void Save()
        {
            SaveCore();
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
        public virtual void LoadItemById(int id)
        {
            _activeId = id;
            Mode = id == 0 ? ScreenMode.Creation : ScreenMode.Edition;
            Refresh();
        }

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override void LoadDataCore()
        {
            Item = new TModel();
            if (_activeId > 0) Item = LoadItemCore(_activeId);
        }

        /// <summary>
        /// Call after load data.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            Item?.ResetModified();
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
            if (Item != null && Item.Id > 0) _activeId = Item.Id;
            if (Item != null) Item.PropertyChanged += OnItemPropertyChanged;
            SaveCommand.RaiseCanExecuteChanged();
            Item?.ResetModified();
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