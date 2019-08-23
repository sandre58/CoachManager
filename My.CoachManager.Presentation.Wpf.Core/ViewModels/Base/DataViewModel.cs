using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;
using GalaSoft.MvvmLight.Command;
using System;
using System.Threading.Tasks;
using My.CoachManager.CrossCutting.Logging;

namespace My.CoachManager.Presentation.Wpf.Core.ViewModels.Base
{
    public abstract class DataViewModel : ScreenViewModel, IRefreshable
    {
        #region Fields

        private bool _dataInitialized;

        #endregion Fields

        #region Members

        /// <summary>
        /// Get or set the state.
        /// </summary>
        /// <remarks></remarks>
        public ScreenState State { get; protected set; }

        /// <summary>
        /// Get or set the mode.
        /// </summary>
        /// <remarks></remarks>
        public ScreenMode Mode { get; protected set; }

        /// <summary>
        /// Gets or sets the refresh command.
        /// </summary>
        public RelayCommand RefreshCommand { get; private set; }

        /// <summary>
        /// Initialise.
        /// </summary>
        protected bool OnRefreshing { get; set; }

        #endregion Members

        #region Initialisation

        /// <inheritdoc />
        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            
            RefreshCommand = new RelayCommand(Refresh, CanRefresh);

            State = ScreenState.NotLoaded;
            Mode = ScreenMode.Read;

        }

        #endregion Initialisation

        #region Loading data

        /// <summary>
        /// Calls before a web service task.
        /// </summary>
        /// <param name="beforeCallback"></param>
        /// <param name="startState"></param>
        private void OnCallWebServiceRequested(Action beforeCallback = null, ScreenState startState = ScreenState.Saving)
        {
            State = startState;
            beforeCallback?.Invoke();
        }

        /// <summary>
        /// Calls after a web service task.
        /// </summary>
        /// <param name="success"></param>
        /// <param name="succeededCallback"></param>
        /// <param name="completedCallback"></param>
        /// <param name="refresh"></param>
        private void OnCallWebServiceCompleted(bool success, Action succeededCallback = null, Action completedCallback = null, bool refresh = false)
        {
            if (success) succeededCallback?.Invoke();

            completedCallback?.Invoke();

            if (success && refresh)
            {
                OnLoadDataRequested();

                LoadDataCore();
                OnLoadDataCompleted();
                State = ScreenState.Ready;
            }

            State = ScreenState.Ready;
        }

        /// <summary>
        /// Call a web service.
        /// </summary>
        /// <param name="call"></param>
        /// <param name="successCallback"></param>
        /// <param name="beforeCallback"></param>
        /// <param name="afterCallback"></param>
        /// <param name="refreshData"></param>
        /// <param name="startState"></param>
        protected async void CallWebService(Action call, Action successCallback = null, Action beforeCallback = null, Action afterCallback = null, bool refreshData = false, ScreenState startState = ScreenState.Saving)
        {
            await Task.Run(() =>
            {
                // Before
                OnCallWebServiceRequested(beforeCallback, startState);

                if (call != null)
                {
                    using (LogManager.TraceGroup(GetType().Name + "." + call.Method.Name))
                    {
                        // Main action
                        call.Invoke();
                    }
                }
            }).ContinueWith(x =>
            {
                // After
                OnCallWebServiceCompleted(!x.IsFaulted && !x.IsCanceled, successCallback, afterCallback, refreshData);
                
                // Exception
                if (x.IsFaulted && x.Exception != null)
                {
                    if (x.Exception.InnerException is ApiException apiException)
                    {
                        if (apiException.CastInnerException is BusinessException businessException)
                            OnBusinessExceptionOccured(businessException);
                        else if (apiException.CastInnerException is ValidationBusinessException validationBusinessException)
                        {
                            foreach (var error in validationBusinessException.Errors)
                            {
                                OnBusinessExceptionOccured(new BusinessException(error.ToString()));
                            }
                        }
                    }
                    else
                    {
                        OnExceptionOccured(x.Exception.InnerException ?? x.Exception);
                    }
                }
            });
        }

        /// <summary>
        /// Refreshes Data.
        /// </summary>
        private void RefreshDataCore()
        {
            if (!OnRefreshing && !OnInitializing)
            {
                OnRefreshing = true;
                CallWebService(LoadDataCore,
                    HandleRefreshed,
                    () =>
                    {
                        if (!_dataInitialized)
                        {
                            InitializeDataCore();

                            _dataInitialized = true;
                            OnInitializeDataCompleted();
                        }

                        OnLoadDataRequested();
                    },
                    () =>
                {
                    OnLoadDataCompleted();
                    OnRefreshing = false;
                },
                    false,
                    ScreenState.Loading);
            }
        }

        /// <summary>
        /// Initialize data asynchronous.
        /// </summary>
        protected virtual void InitializeDataCore()
        {
        }

        /// <summary>
        /// Initialize data asynchronous.
        /// </summary>
        protected virtual void OnInitializeDataCompleted()
        {
        }

        /// <summary>
        /// Load Data asynchronous.
        /// </summary>
        protected abstract void LoadDataCore();

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

        #endregion Loading data

        #region Refresh

        public event EventHandler Refreshed;

        private void HandleRefreshed()
        {
            Refreshed?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc />
        /// <summary>
        /// Refresh Items.
        /// </summary>
        public virtual void Refresh()
        {
            RefreshDataCore();
        }

        /// <inheritdoc />
        /// <summary>
        /// Can refresh item.
        /// </summary>
        public virtual bool CanRefresh()
        {
            return true;
        }

        #endregion Refresh

        #region Exceptions Management

        /// <summary>
        /// Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExceptionOccured(Exception e)
        {
            State = ScreenState.Ready;
            base.OnExceptionOccured(e);
        }

        /// <summary>
        /// Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBusinessExceptionOccured(BusinessException e)
        {
            State = ScreenState.Ready;
            base.OnBusinessExceptionOccured(e);
        }

        #endregion Exceptions Management

        #region PropertyChanged

        /// <summary>
        /// Called when mode changes.
        /// </summary>
        protected virtual void OnModeChanged()
        {
        }

        /// <summary>
        /// Called when mode changes.
        /// </summary>
        protected virtual void OnStateChanged()
        {
        }

        #endregion PropertyChanged
    }
}