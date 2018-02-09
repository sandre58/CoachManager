using System;
using System.Threading.Tasks;
using System.Windows.Input;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using Prism.Commands;

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

        /// <summary>
        /// Gets or sets the keyboard action command.
        /// </summary>
        public DelegateCommand<KeyEventArgs> KeyboardActionCommand { get; set; }

        /// <summary>
        /// Gets or sets the keyboard action command.
        /// </summary>
        public DelegateCommand EnterCommand { get; set; }

        /// <summary>
        /// Gets or sets the keyboard action command.
        /// </summary>
        public DelegateCommand EscapeCommand { get; set; }

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
            KeyboardActionCommand = new DelegateCommand<KeyEventArgs>(KeyboardAction);
            EnterCommand = new DelegateCommand(Enter);
            EscapeCommand = new DelegateCommand(Escape);
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

            ResetModified();
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
                    if (task.Exception.InnerException is BusinessException)
                    {
                        OnBusinessExceptionOccured((BusinessException)task.Exception.InnerException);
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

        #region Keyboard

        /// <summary>
        /// Do action by keyboard trigger.
        /// </summary>
        protected virtual void KeyboardAction(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    Enter();
                    e.Handled = true;
                    break;

                case Key.Escape:
                    Escape();
                    e.Handled = true;
                    break;
            }
        }

        /// <summary>
        /// Action when "Enter" key is down.
        /// </summary>
        protected virtual void Enter()
        {
        }

        /// <summary>
        /// Action when "Escape" key is down.
        /// </summary>
        protected virtual void Escape()
        {
        }

        #endregion Keyboard

        #endregion Methods
    }
}