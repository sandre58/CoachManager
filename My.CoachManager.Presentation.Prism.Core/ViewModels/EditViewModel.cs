using System;
using System.Windows.Input;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Interactivity;
using My.CoachManager.Presentation.Prism.Core.Services;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class EditViewModel<TEntityViewModel> : WorkspaceDialogViewModel, IEditViewModel
        where TEntityViewModel : class, IEntityViewModel, IValidatable, IModificable, new()
    {
        #region Fields

        private TEntityViewModel _item;
        private int _activeId;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initialise a new instance of <see cref="EditViewModel{TEntityViewModel}"/>.
        /// </summary>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="logger">The logger.</param>
        protected EditViewModel(IDialogService dialogService, ILogger logger)
            : base(dialogService, logger)
        {
            Mode = ScreenMode.Creation;
            Item = new TEntityViewModel();

            SaveCommand = new DelegateCommand(Save, CanSave);
            CancelCommand = new DelegateCommand(Cancel, CanCancel);
            RefreshCommand = new DelegateCommand(Refresh, CanRefresh);

            RefreshData(true);
        }

        #endregion Constructor

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
                SetProperty(ref _item, value, () =>
                {
                    if (value != null) _activeId = value.Id;
                    RaisePropertyChanged(() => Title);
                    RaisePropertyChanged(() => SaveCommand);
                    RaisePropertyChanged(() => CancelCommand);
                });
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public override string Title
        {
            get { return Mode == ScreenMode.Edition ? string.Format(MessageResources.EditItem, base.Title) : string.Format(MessageResources.CreateItem, base.Title); }
            set { base.Title = value; }
        }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        public override ScreenMode Mode
        {
            get { return base.Mode; }
            protected set
            {
                base.Mode = value;
                RaisePropertyChanged(() => Title);
            }
        }

        /// <summary>
        /// Get or Set Save Command.
        /// </summary>
        public ICommand SaveCommand { get; set; }

        /// <summary>
        /// Get or Set Cancel Command.
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Get or Set Refresh Command.
        /// </summary>
        public ICommand RefreshCommand { get; set; }

        #endregion Members

        #region Methods

        #region Save

        /// <summary>
        /// Add a new item.
        /// </summary>
        public virtual void Save()
        {
            if (CanSave())
            {
                State = ScreenState.Saving;
                BeforeSave();
                if (Item.IsValid())
                {
                    if (SaveItem())
                    {
                        Mode = ScreenMode.Edition;
                        DialogService.ShowSuccessPopup(MessageResources.SavingSuccess);
                        Close();
                    }
                }
                else
                {
                    DialogService.ShowErrorPopup(MessageResources.InvalidForm);
                }

                State = ScreenState.Ready;
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
        private bool SaveItem()
        {
            try
            {
                SaveItemCore();
            }
            catch (BusinessException e)
            {
                OnBusinessExceptionOccured(e);
                return false;
            }
            catch (Exception e)
            {
                OnExceptionOccured(e);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Save.
        /// </summary>
        protected abstract void SaveItemCore();

        /// <summary>
        /// Before Save.
        /// </summary>
        protected virtual void BeforeSave()
        {
        }

        /// <summary>
        /// After Save.
        /// </summary>
        protected virtual void AfterSave()
        {
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
                    DialogService.ShowWarningDialog(MessageResources.CancelModifications, dialog =>
                    {
                        OnAfterCancelItem(dialog.Result);
                    }, MessageDialogType.YesNo);
                }
                else
                {
                    OnAfterCancelItem(DialogResult.Yes);
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
        protected virtual void OnAfterCancelItem(DialogResult result)
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
            RefreshData();
        }

        /// <summary>
        /// Can refresh item.
        /// </summary>
        public virtual bool CanRefresh()
        {
            return true;
        }

        #endregion Refresh

        /// <summary>
        /// Load an item by id.
        /// </summary>
        public void LoadItemById(int id)
        {
            _activeId = id;
            Mode = ScreenMode.Edition;
            RefreshData();
        }

        /// <summary>
        /// Save.
        /// </summary>
        protected override void LoadDataCore(bool isFirstLoading = false)
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

        /// <summary>
        /// Call after load data.
        /// </summary>
        public override void ResetModified()
        {
            Item.ResetModified();
            base.ResetModified();
        }

        #endregion Methods
    }
}