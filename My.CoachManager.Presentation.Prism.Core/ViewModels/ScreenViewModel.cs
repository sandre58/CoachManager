using System;
using System.Threading.Tasks;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Services;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class ScreenViewModel : ViewModelBase, IScreenViewModel
    {
        #region Fields

        private ScreenState _state;
        private ScreenMode _mode;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ScreenViewModel"/>.
        /// </summary>
        public ScreenViewModel(IDialogService dialogService, ILogger logger)
        {
            Logger = logger;
            DialogService = dialogService;
            State = ScreenState.NotLoaded;
            Mode = ScreenMode.Read;
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets the logger.
        /// </summary>
        protected IDialogService DialogService { get; private set; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        protected ILogger Logger { get; private set; }

        /// <summary>
        /// Get or set the state.
        /// </summary>
        /// <remarks></remarks>
        public ScreenState State
        {
            get
            {
                return _state;
            }

            protected set
            {
                SetProperty(ref _state, value);
            }
        }

        /// <summary>
        /// Get or set the mode.
        /// </summary>
        /// <remarks></remarks>
        public virtual ScreenMode Mode
        {
            get
            {
                return _mode;
            }

            protected set
            {
                SetProperty(ref _mode, value, OnModeChanged);
            }
        }

        #endregion Members

        #region Methods

        /// <summary>
        /// Load Data.
        /// </summary>
        private async void LoadData(bool isFirstLoading = false)
        {
            try
            {
                await Task.Run(() =>
                {
                    try
                    {
                        BeforeLoadData(isFirstLoading);
                        LoadDataCore(isFirstLoading);
                        AfterLoadData(isFirstLoading);
                    }
                    catch (BusinessException e)
                    {
                        OnBusinessExceptionOccured(e);
                    }
                    catch (Exception e)
                    {
                        OnExceptionOccured(e);
                    }
                });
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

        /// <summary>
        /// Load Data
        /// </summary>
        protected virtual void LoadDataCore(bool isFirstLoading = false)
        {
        }

        /// <summary>
        /// Call before loading data.
        /// </summary>
        protected virtual void BeforeLoadData(bool isFirstLoading = false)
        {
            State = ScreenState.Loading;
        }

        /// <summary>
        /// Call after load data.
        /// </summary>
        protected virtual void AfterLoadData(bool isFirstLoading = false)
        {
            ResetModified();
            State = ScreenState.Ready;
        }

        /// <summary>
        /// Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnExceptionOccured(Exception e)
        {
            State = ScreenState.Ready;
            Logger.Fatal(e);
            DialogService.ShowErrorDialog(MessageResources.GetDataError);
        }

        /// <summary>
        /// Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBusinessExceptionOccured(BusinessException e)
        {
            State = ScreenState.Ready;
            DialogService.ShowErrorPopup(e.Message);
        }

        /// <summary>
        /// Refreshes Data.
        /// </summary>
        protected virtual void RefreshData(bool isFirstLoading = false)
        {
            LoadData(isFirstLoading);
        }

        /// <summary>
        /// Call when mode changed.
        /// </summary>
        protected virtual void OnModeChanged()
        {
        }

        #endregion Methods
    }
}