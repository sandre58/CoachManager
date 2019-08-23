using My.CoachManager.Presentation.Wpf.Core.Navigation;

namespace My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces
{
    public interface INavigableWorkspaceViewModel : IWorkspaceViewModel
    {
        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        void OnNavigatedTo(NavigationContext navigationContext);

        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns>True if this instance accepts the navigation request; otherwise, False.</returns>
        bool IsNavigationTarget(NavigationContext navigationContext);

        /// <summary>
        /// Called when the implementer is being navigated away from.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        void OnNavigatedFrom(NavigationContext navigationContext);

        /// <summary>
        /// Return a parameters;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        T GetParameter<T>(string key, T defaultValue = default(T));
    }
}