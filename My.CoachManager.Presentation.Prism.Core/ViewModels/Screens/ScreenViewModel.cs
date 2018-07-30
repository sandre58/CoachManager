using System;
using System.Threading.Tasks;
using My.CoachManager.CrossCutting.Core.Exceptions;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class ScreenViewModel : ViewModelBase, IScreenViewModel
    {
        #region Fields

        private bool _dataInitialized;

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
        protected virtual async void RefreshDataAsync()
        {
            await LoadDataAsync();
        }

        /// <summary>
        /// Load data.
        /// </summary>
        /// <returns></returns>
        private async Task LoadDataAsync()
        {
            var task = Task.Run(() =>
            {
                if (!_dataInitialized)
                {
                    InitializeDataCore();
                    _dataInitialized = true;
                }

                LoadDataCore();
            });

            State = ScreenState.Loading;

            OnLoadDataRequested();

            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception)
            {
                // ignored
            }

            State = ScreenState.Ready;

            // Is Cancelled
            if (task.IsCanceled)
            {
            }

            // Exception
            else if (task.IsFaulted)
            {
                if (task.Exception != null)
                {
                    var exception = task.Exception.InnerException as BusinessException;
                    if (exception != null)
                    {
                        OnBusinessExceptionOccured(exception);
                    }
                    else
                    {
                        OnExceptionOccured(task.Exception.InnerException);
                    }
                }
            }

            // Is Completed
            else if (task.IsCompleted)
            {
                OnLoadDataCompleted();
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
        protected virtual void Refresh()
        {
            RefreshDataAsync();
        }

        /// <summary>
        /// Can refresh item.
        /// </summary>
        protected virtual bool CanRefresh()
        {
            return Mode == ScreenMode.Read;
        }

        #endregion Refresh
    }
}