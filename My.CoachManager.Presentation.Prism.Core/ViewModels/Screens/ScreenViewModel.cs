using System;
using System.Threading;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;

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
        public ScreenViewModel()
        {
            State = ScreenState.NotLoaded;
            Mode = ScreenMode.Read;

            Initialize();
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Get or set the state.
        /// </summary>
        /// <remarks></remarks>
        public ScreenState State
        {
            get { return _state; }

            protected set { SetProperty(ref _state, value); }
        }

        /// <summary>
        /// Get or set the mode.
        /// </summary>
        /// <remarks></remarks>
        public ScreenMode Mode
        {
            get { return _mode; }

            protected set { SetProperty(ref _mode, value); }
        }

        #endregion Members

        #region Methods

        #region Initialization

        /// <summary>
        /// Initializes in contructor.
        /// </summary>
        private void Initialize()
        {
            InitializeCommands();
            InitializeData();
        }

        /// <summary>
        /// Initializes commands in contrusctor.
        /// </summary>
        protected virtual void InitializeCommands()
        {
        }

        /// <summary>
        /// Initializes data in constructor.
        /// </summary>
        protected virtual void InitializeData()
        {
        }

        #endregion Initialization

        #region Loading data

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
        /// Initialize data asynchronous.
        /// </summary>
        protected virtual void InitializeDataCore()
        {
        }

        /// <summary>
        /// Load Data asynchronous.
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

        #endregion Loading data

        #region Exceptions Management

        /// <summary>
        /// Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnExceptionOccured(Exception e)
        {
            Locator.Logger.Fatal(e);
            Locator.DialogService.ShowErrorDialog(MessageResources.GetDataError);
        }

        /// <summary>
        /// Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBusinessExceptionOccured(BusinessException e)
        {
            Locator.DialogService.ShowErrorPopup(e.Message);
        }

        #endregion Exceptions Management

        #endregion Methods
    }
}