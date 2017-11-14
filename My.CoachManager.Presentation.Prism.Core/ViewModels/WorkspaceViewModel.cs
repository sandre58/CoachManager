using System.Windows.Input;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Interactivity;
using My.CoachManager.Presentation.Prism.Core.Services;
using Prism.Commands;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class WorkspaceViewModel : ScreenViewModel, IWorkspaceViewModel, INavigationAware, IRegionMemberLifetime
    {
        #region Fields

        private string _title;
        private IRegionNavigationJournal _journal;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ScreenViewModel"/>.
        /// </summary>
        public WorkspaceViewModel(IDialogService dialogService, ILogger logger)
            : base(dialogService, logger)
        {
            GoBackCommand = new DelegateCommand(GoBack, CanGoBack);
            GoForwardCommand = new DelegateCommand(GoForward, CanGoForward);
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets or sets the title screen.
        /// </summary>
        public virtual string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance should be kept-alive upon deactivation.
        /// </summary>
        public virtual bool KeepAlive { get { return false; } }

        /// <summary>
        /// Gets or sets the go back command.
        /// </summary>
        public ICommand GoBackCommand { get; set; }

        /// <summary>
        /// Gets or sets the go forward command.
        /// </summary>
        public ICommand GoForwardCommand { get; set; }

        #endregion Members

        #region Methods

        #region GoBack

        /// <summary>
        /// Go back.
        /// </summary>
        public void GoBack()
        {
            if (_journal != null)
            {
                _journal.GoBack();
            }
        }

        /// <summary>
        /// Can go back.
        /// </summary>
        /// <returns></returns>
        public bool CanGoBack()
        {
            return _journal != null ? _journal.CanGoBack : false;
        }

        #endregion GoBack

        #region GoForward

        /// <summary>
        /// Go back.
        /// </summary>
        public void GoForward()
        {
            if (_journal != null)
            {
                _journal.GoForward();
            }
        }

        /// <summary>
        /// Can go back.
        /// </summary>
        /// <returns></returns>
        public bool CanGoForward()
        {
            return _journal != null ? _journal.CanGoForward : false;
        }

        #endregion GoForward

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
            if (State == ScreenState.NotLoaded)
            {
                RefreshData(true);
            }
        }

        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns>True if this instance accepts the navigation request; otherwise, False.</returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// Called when the implementer is being navigated away from.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            if (Mode == ScreenMode.Creation || Mode == ScreenMode.Edition)
            {
                DialogService.ShowQuestionDialog("dfjedfod", dialog =>
                {
                    if (dialog.Result != DialogResult.Yes)
                    {
                    }
                });
            }
        }

        #endregion Methods
    }
}