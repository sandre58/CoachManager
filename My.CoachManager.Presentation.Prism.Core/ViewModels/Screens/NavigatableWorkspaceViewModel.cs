using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class NavigatableWorkspaceViewModel : WorkspaceViewModel, INavigatableWorkspaceViewModel
    {
        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether this instance should be kept-alive upon deactivation.
        /// </summary>
        public virtual bool KeepAlive => true;

        #endregion Members

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //if (State == ScreenState.NotLoaded)
            //{
            OnNavigatedToCore(navigationContext);
            //}
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        protected virtual void OnNavigatedToCore(NavigationContext navigationContext)
        {
            RefreshDataAsync();
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
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            if (Mode == ScreenMode.Creation || Mode == ScreenMode.Edition)
            {
                OnNavigatedFromCore(navigationContext);
            }
        }

        /// <summary>
        /// Called when the implementer is being navigated away from.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        protected virtual void OnNavigatedFromCore(NavigationContext navigationContext)
        {
        }

        #endregion Methods
    }
}