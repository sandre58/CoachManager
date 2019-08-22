using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Presentation.Core.Models;
using My.CoachManager.Presentation.Wpf.Core.Constants;
using My.CoachManager.Presentation.Wpf.Core.Dialog;
using My.CoachManager.Presentation.Wpf.Core.Manager;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Base;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;
using Prism.Commands;
using PropertyChanged;
using System.ComponentModel;

namespace My.CoachManager.Presentation.Wpf.Core.ViewModels
{
    public abstract class EditViewModel<TModel> : WorkspaceDialogViewModel, IEditViewModel<TModel>
        where TModel : class, IEntityModel, IValidatable, IModifiable, INotifyPropertyChanged, new()
    {

        #region Members

        public string SavingSuccessMessage { get; set; }

        public string NewItemMessage { get; set; }

        public string EditItemMessage { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets item.
        /// </summary>
        [DoNotCheckEquality]
        IEntityModel IItemViewModel.Item
        {
            get => Item;
            set => Item = (TModel)value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Get or set Item.
        /// </summary>
        [DoNotCheckEquality]
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
        protected override void Initialize()
        {
            base.Initialize();
            
            SaveCommand = new DelegateCommand(Save, CanSave);
            CancelCommand = new DelegateCommand(Cancel, CanCancel);

            SavingSuccessMessage = MessageResources.SavingSuccess;
            NewItemMessage = ControlResources.Creation;
            EditItemMessage = ControlResources.Edition;

            Item = new TModel();
            Mode = ScreenMode.Creation;

        }

        #endregion Initialization

        #region Save

        /// <summary>
        /// Refreshes Data.
        /// </summary>
        private void SaveCore()
        {
            OnSaveRequested();

            if (Item.Validate())
            {
                CallWebService(() =>
                    {
                        var result = SaveItemCore();
                        Item.Id = result;
                    },
                     OnSaveSucceeded);
            }
            else
            {
                foreach (var error in Item.GetErrors())
                {
                    OnBusinessExceptionOccured(new BusinessException(error));
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
        protected abstract int SaveItemCore();

        /// <summary>
        /// Before Save.
        /// </summary>
        protected virtual void OnSaveRequested()
        {
        }

        /// <summary>
        /// After Save.
        /// </summary>
        protected virtual void OnSaveSucceeded()
        {
            NotificationManager.ShowSuccess(SavingSuccessMessage);
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

        /// <summary>
        /// Call before load data.
        /// </summary>
        protected override void OnLoadDataRequested()
        {
            base.OnLoadDataRequested();

            var id = GetParameter<int>(ParametersConstants.Id);
            Mode = id == 0 ? ScreenMode.Creation : ScreenMode.Edition;

        }

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override void LoadDataCore()
        {
            Item = new TModel();

            if (Mode == ScreenMode.Edition) Item = LoadItemCore(GetParameter<int>(ParametersConstants.Id));
        }

        /// <summary>
        /// Call after load data.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            base.OnLoadDataCompleted();
            Mode = Item.Id == 0 ? ScreenMode.Creation : ScreenMode.Edition;
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
            if (Item != null && Item.Id > 0)
            {
                SetParameter(ParametersConstants.Id, Item.Id);
            }

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

        /// <inheritdoc />
        /// <summary>
        /// Called when mode changes.
        /// </summary>
        protected override void OnModeChanged()
        {
            Title = Mode == ScreenMode.Edition ? EditItemMessage : NewItemMessage;
        }

        #endregion Properties Changed

        #endregion Methods
    }
}