using System.Linq;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class NavigatableWorkspaceViewModel : WorkspaceViewModel, INavigatableWorkspaceViewModel
    {
        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether this instance should be kept-alive upon deactivation.
        /// </summary>
        public virtual bool KeepAlive => true;

        /// <summary>
        /// Gets navigation parameters.
        /// </summary>
        public NavigationParameters NavigationParameters { get; private set; }

        /// <summary>
        /// Gets navigation id.
        /// </summary>
        public int NavigationId { get; private set; }

        #endregion Members

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            NavigationParameters = navigationContext.Parameters;
            if (NavigationParameters.Any(x => x.Key == "Id"))
            {
                NavigationId = int.Parse(NavigationParameters["Id"].ToString());
            }
            OnNavigatedToCore(NavigationParameters);
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationParameters">The navigation context.</param>
        protected virtual void OnNavigatedToCore(NavigationParameters navigationParameters)
        {
            Refresh();
        }

        /// <inheritdoc />
        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns>True if this instance accepts the navigation request; otherwise, False.</returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return NavigationParameters.ToString().Equals(navigationContext.Parameters.ToString());
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
                OnNavigatedFromCore(navigationContext.Parameters);
            }
        }

        /// <summary>
        /// Called when the implementer is being navigated away from.
        /// </summary>
        /// <param name="navigationParameters">The navigation context.</param>
        protected virtual void OnNavigatedFromCore(NavigationParameters navigationParameters)
        {
        }

        #endregion Methods
    }
}