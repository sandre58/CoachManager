using System;
using System.Windows.Input;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.Presentation.Core.Commands;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Enums;
using My.CoachManager.Presentation.Resources.Strings;

namespace My.CoachManager.Presentation.ViewModels.Core
{
    /// <summary>
    /// A base class for all viewmodels that cater to workspace views.
    /// </summary>
    public abstract class EditViewModel<TEntityViewModel> : DialogViewModel
        where TEntityViewModel : EntityViewModel, new()
    {
        #region Fields

        private TEntityViewModel _item;
        private ModeView _mode;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="EditViewModel{TEntityViewModel}"/>.
        /// </summary>
        public EditViewModel()
        {
            Mode = ModeView.Creation;
        }

        /// <summary>
        /// Initialise a new instance of <see cref="EditViewModel{TEntityViewModel}"/>.
        /// </summary>
        /// <param name="id"></param>
        public EditViewModel(int id)
            : this()
        {
            ItemId = id;
            Mode = ModeView.Modification;
        }

        #endregion Constructors

        #region Properties

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
                if (_item != null && _item.Equals(value)) return;

                _item = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => DisplayName);
                NotifyOfPropertyChange(() => IsModified);
            }
        }

        /// <summary>
        /// Get or set Item.
        /// </summary>
        protected int ItemId { get; private set; }

        /// <summary>
        /// Get or set the mode.
        /// </summary>
        public ModeView Mode
        {
            get
            {
                return _mode;
            }
            protected set
            {
                if (_mode != null && _mode.Equals(value)) return;

                _mode = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Get or Set Save Command.
        /// </summary>
        public ICommand SaveCommand { get; set; }

        /// <summary>
        /// Get or Set Save and close Command.
        /// </summary>
        public ICommand SaveAndCloseCommand { get; set; }

        /// <summary>
        /// Get or Set Cancel Command.
        /// </summary>
        public ICommand CancelCommand { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initialise properties.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            if (Item == null)
                Item = new TEntityViewModel();

            SaveCommand = new DelegateCommand(Save, CanSave);
            CancelCommand = new DelegateCommand(Close, CanClose);
            SaveAndCloseCommand = new DelegateCommand(SaveAndClose, CanSaveAndClose);
        }

        /// <summary>
        /// Call after load data.
        /// </summary>
        protected override void AfterLoadData()
        {
            Item.ResetModified();
            base.AfterLoadData();
        }

        /// <summary>
        /// Save.
        /// </summary>
        public void Save()
        {
            if (Item.IsValid())
            {
                if (SaveInternal())
                {
                    ServiceLocator.DialogService.ShowSuccessPopup(MessageResources.SavingSuccess);
                }
            }
            else
            {
                ServiceLocator.DialogService.ShowErrorPopup(MessageResources.SavingFailed);
            }
        }

        /// <summary>
        /// Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFailureSaving(Exception e)
        {
            State = ViewModelState.Ready;
            ServiceLocator.DialogService.ShowError(e.Message);
        }

        /// <summary>
        /// Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBusinessFailureSaving(BusinessException e)
        {
            State = ViewModelState.Ready;
            ServiceLocator.DialogService.ShowErrorPopup(e.Message);
        }

        /// <summary>
        /// Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFailureLoadingData(Exception e)
        {
            base.OnFailureLoadingData(e);
            ServiceLocator.DialogService.ShowError(e.Message);
        }

        /// <summary>
        /// Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBusinessFailureLoadingData(BusinessException e)
        {
            base.OnFailureLoadingData(e);
            ServiceLocator.DialogService.ShowErrorPopup(e.Message);
        }

        /// <summary>
        /// Save.
        /// </summary>
        protected virtual void SaveDataCore()
        {
        }

        /// <summary>
        /// Save.
        /// </summary>
        private bool SaveInternal()
        {
            try
            {
                BeforeSave();
                SaveDataCore();
                AfterSave();
            }
            catch (BusinessException e)
            {
                OnBusinessFailureSaving(e);
                return false;
            }
            catch (Exception e)
            {
                OnFailureSaving(e);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Before Save.
        /// </summary>
        protected virtual void BeforeSave()
        {
            State = ViewModelState.Saving;
        }

        /// <summary>
        /// After Save.
        /// </summary>
        protected virtual void AfterSave()
        {
            State = ViewModelState.Ready;
        }

        /// <summary>
        /// Can Save.
        /// </summary>
        public virtual bool CanSave()
        {
            return true;
        }

        /// <summary>
        /// Save.
        /// </summary>
        public virtual void SaveAndClose()
        {
            if (Item.IsValid())
            {
                if (SaveInternal())
                {
                    ServiceLocator.DialogService.ShowSuccessPopup(MessageResources.SavingSuccess);
                    TryClose(true);
                }
            }
            else
            {
                ServiceLocator.DialogService.ShowErrorPopup(MessageResources.SavingFailed);
            }
        }

        /// <summary>
        /// Can Save.
        /// </summary>
        public virtual bool CanSaveAndClose()
        {
            return true;
        }

        /// <summary>
        /// Save.
        /// </summary>
        protected override async void Close()
        {
            if (!Item.IsModified || await ServiceLocator.DialogService.ShowQuestion(MessageResources.CancelModifications) == MessageDialogResponse.Yes)
            {
                TryClose(false);
            }
        }

        #endregion Methods
    }
}