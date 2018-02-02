﻿using My.CoachManager.Presentation.Prism.Core.Interactivity;
using My.CoachManager.Presentation.Prism.Core.Navigation;
using Prism.Events;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class NavigatableWorkspaceViewModel : WorkspaceViewModel, INavigatableWorkspaceViewModel
    {
        #region Members

        /// <summary>
        /// Gets a value indicating whether this instance should be kept-alive upon deactivation.
        /// </summary>
        public virtual bool KeepAlive { get { return true; } }

        #endregion Members

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            Locator.GetInstance<IEventAggregator>().GetEvent<NotifyNavigationCompletedEvent>().Publish(new NavigationCompletedEventArgs(this, navigationContext));

            if (State == ScreenState.NotLoaded)
            {
                RefreshData();
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns>True if this instance accepts the navigation request; otherwise, False.</returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <inheritdoc />
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