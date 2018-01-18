using System;
using System.Threading;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Services;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class ScreenViewModel : ViewModelBase, IScreenViewModel
    {
        #region Fields

        private ScreenState _state;
        private ScreenMode _mode;
        private bool _dataInitialized;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ScreenViewModel"/>.
        /// </summary>
        public ScreenViewModel(IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger)
        {
            Logger = logger;
            DialogService = dialogService;
            EventAggregator = eventAggregator;
            State = ScreenState.NotLoaded;
            Mode = ScreenMode.Read;
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets the dialog service.
        /// </summary>
        protected IDialogService DialogService { get; private set; }

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        protected IEventAggregator EventAggregator { get; private set; }

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
            get { return _state; }

            protected set { SetProperty(ref _state, value, OnStateChanged); }
        }

        /// <summary>
        /// Get or set the mode.
        /// </summary>
        /// <remarks></remarks>
        public ScreenMode Mode
        {
            get { return _mode; }

            protected set { SetProperty(ref _mode, value, OnModeChanged); }
        }

        #endregion Members

        #region Methods

        /// <summary>
        /// Refreshes Data.
        /// </summary>
        public virtual void RefreshData()
        {
            LoadData();
        }

        /// <summary>
        /// Load data.
        /// </summary>
        protected void LoadData()
        {
            State = ScreenState.Loading;

            OnLoadDataRequested();

            ThreadPool.QueueUserWorkItem(obj =>
            {
                try
                {
                    if (!_dataInitialized)
                    {
                        InitializeDataCore();
                        _dataInitialized = true;
                    }

                    LoadDataCore();

                    ResetModified();

                    OnLoadDataCompleted();

                    State = ScreenState.Ready;
                }
                catch (BusinessException e)
                {
                    State = ScreenState.Ready;
                    OnBusinessExceptionOccured(e);
                }
                catch (Exception e)
                {
                    State = ScreenState.Ready;
                    OnExceptionOccured(e);
                }
            });
        }

        /// <summary>
        /// Initialize data.
        /// </summary>
        protected virtual void InitializeDataCore()
        {
        }

        /// <summary>
        /// Load Data.
        /// </summary>
        protected virtual void LoadDataCore()
        {
        }

        /// <summary>
        /// Call before load data.
        /// </summary>
        protected virtual void OnLoadDataRequested()
        {
        }

        /// <summary>
        /// Call after load data.
        /// </summary>
        protected virtual void OnLoadDataCompleted()
        {
        }

        /// <summary>
        /// Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnExceptionOccured(Exception e)
        {
            Logger.Fatal(e);
            DialogService.ShowErrorDialog(MessageResources.GetDataError);
        }

        /// <summary>
        /// Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBusinessExceptionOccured(BusinessException e)
        {
            DialogService.ShowErrorPopup(e.Message);
        }

        /// <summary>
        /// Call when mode changed.
        /// </summary>
        protected virtual void OnModeChanged()
        {
        }

        /// <summary>
        /// Call when state changed.
        /// </summary>
        protected virtual void OnStateChanged()
        {
        }

        #endregion Methods
    }
}