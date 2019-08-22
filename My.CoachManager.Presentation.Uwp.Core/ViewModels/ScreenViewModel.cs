using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.Presentation.Uwp.Core.ViewModels.Interfaces;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace My.CoachManager.Presentation.Uwp.Core.ViewModels
{
    public abstract class ScreenViewModel : ViewModelBase, IRefreshable, IScreenViewModel
    {
        #region Fields

        private bool _dataInitialized;

#endregion

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

        /// <inheritdoc />
        /// <summary>
        /// Get or set the state.
        /// </summary>
        /// <remarks></remarks>
        public ScreenState State { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Get or set the mode.
        /// </summary>
        /// <remarks></remarks>
        public ScreenMode Mode { get; set; }

        /// <summary>
        /// Gets or sets the refresh command.
        /// </summary>
        public DelegateCommand RefreshCommand { get; private set; }

        #endregion Members

        #region Initialisation

        /// <inheritdoc />
        /// <summary>
        /// Initialize all events, data, and commands.
        /// </summary>
        public virtual void Initialize()
        {
            InitializeEvents();
            InitializeCommands();
            InitializeData();
        }

        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected virtual void InitializeCommands()
        {
            RefreshCommand = new DelegateCommand(Refresh, CanRefresh);
        }

        /// <summary>
        /// Launch on constructor for initialize all Data.
        /// </summary>
        protected virtual void InitializeData()
        {
        }

        /// <summary>
        /// Launch on constructor for initialize all Event.
        /// </summary>
        protected virtual void InitializeEvents()
        {
        }

        #endregion Initialisation

        #region Loading data

        /// <summary>
        /// Refreshes Data.
        /// </summary>
        protected virtual async void RefreshDataCore()
        {
                State = ScreenState.Loading;

            try
            {
                OnLoadDataRequested();

                if (!_dataInitialized)
                {
                    InitializeDataCore();
                    _dataInitialized = true;
                }

                await LoadDataCoreAsync();

                    OnLoadDataCompleted();
                    State = ScreenState.Ready;

            }
            catch (Exception exception)
            {
                State = ScreenState.Ready;
                if (exception.InnerException is BusinessException businessException)
                {
                    //OnBusinessExceptionOccured(businessException);
                }
                else
                {
                    //OnExceptionOccured(exception.InnerException ?? exception);
                }
            }
        }

        /// <summary>
        /// Refreshes Data.
        /// </summary>
        protected abstract Task LoadDataCoreAsync();

        /// <summary>
        /// Initialize data asynchronous.
        /// </summary>
        protected virtual void InitializeDataCore()
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

        #region Navigation

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            Refresh();
        }

        #endregion

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
