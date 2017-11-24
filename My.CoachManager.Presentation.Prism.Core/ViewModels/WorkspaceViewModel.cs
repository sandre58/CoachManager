using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.EventAggregator;
using My.CoachManager.Presentation.Prism.Core.Interactivity;
using My.CoachManager.Presentation.Prism.Core.Services;
using Prism.Events;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class WorkspaceViewModel : ScreenViewModel, IWorkspaceViewModel, INavigationAware, IRegionMemberLifetime
    {
        #region Fields

        private string _title;
        private readonly IEventAggregator _eventAggregator;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ScreenViewModel"/>.
        /// </summary>
        public WorkspaceViewModel(IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger)
            : base(dialogService, logger)
        {
            _eventAggregator = eventAggregator;
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

        #endregion Members

        #region Methods

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            _eventAggregator.GetEvent<NotifyNavigationCompletedEvent>().Publish(new NavigationCompletedEventArgs(this, navigationContext));

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
            }
        }

        #endregion Methods
    }
}