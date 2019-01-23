using System;
using System.ComponentModel;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.Presentation.Prism.Core.ComponentModel;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class ScreenViewModel : ViewModelBase, IRefreshable
    {
        #region Fields

        private bool _dataInitialized;

        /// <summary>
        /// The laod data background worker.
        /// </summary>
        private AbortableBackgroundWorker _loadDataBackgroundWorker;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ScreenViewModel"/>.
        /// </summary>
        protected ScreenViewModel()
        {
            State = ScreenState.NotLoaded;
            Mode = ScreenMode.Read;
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Get or set the state.
        /// </summary>
        /// <remarks></remarks>
        public ScreenState State { get; set; }

        /// <summary>
        /// Get or set the mode.
        /// </summary>
        /// <remarks></remarks>
        public ScreenMode Mode { get; set; }

        /// <summary>
        /// Gets or sets the refresh command.
        /// </summary>
        public DelegateCommand RefreshCommand { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets if we can refresh after initialisation.
        /// </summary>
        public virtual bool RefreshOnInit => false;

        /// <summary>
        /// Gets or sets parameters.
        /// </summary>
        public IScreenParameters Parameters { get; set; }

        #endregion Members

        #region Initialisation

        /// <inheritdoc />
        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            RefreshCommand = new DelegateCommand(Refresh, CanRefresh);
        }

        #endregion Initialisation

        #region Loading data

        /// <summary>
        /// Refreshes Data.
        /// </summary>
        private void RefreshDataCore()
        {
            State = ScreenState.Loading;

            OnLoadDataRequested();

            _loadDataBackgroundWorker = new AbortableBackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            _loadDataBackgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
            _loadDataBackgroundWorker.DoWork += OnBackgroundWorkerOnDoWork;

            // Start the background worker
            _loadDataBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Called when [background worker run worker completed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnLoadDataCompleted();
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
                if (!_dataInitialized)
                {
                    InitializeDataCore();
                    _dataInitialized = true;
                }

                LoadDataCore();
            }
            catch (Exception exception)
            {
                if (exception.InnerException is BusinessException businessException)
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

        #region Refresh

        /// <summary>
        /// Refresh Items.
        /// </summary>
        public virtual void Refresh()
        {
            RefreshDataCore();
        }

        /// <summary>
        /// Can refresh item.
        /// </summary>
        public virtual bool CanRefresh()
        {
            return true;
        }

        #endregion Refresh

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