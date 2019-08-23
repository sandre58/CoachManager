using System;
using System.Collections.Generic;
using CommonServiceLocator;
using My.CoachManager.Presentation.Wpf.Core.Constants;
using My.CoachManager.Presentation.Wpf.Core.Ioc;
using My.CoachManager.Presentation.Wpf.Core.Navigation;
using My.CoachManager.Presentation.Wpf.Core.Services;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Wpf.Core.Manager
{
/// <summary>
    /// Provides methods and properties to navigate between views.
    /// </summary>
    public static class NavigationManager
    {
        #region Fields

        private static INavigationService _navigationService;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets Navigation Service.
        /// </summary>
        private static INavigationService NavigationService => _navigationService ??
                                                              (_navigationService = ServiceLocator.Current.GetInstance<INavigationService>());

        /// <summary>
        /// Gets active view.
        /// </summary>
        public static INavigableWorkspaceViewModel CurrentWorkspace => NavigationService.CurrentWorkspace;

        #endregion Members

        #region Events

        /// <summary>
        /// Raised when the region is about to be navigated to content.
        /// </summary>
        public static event EventHandler<NavigationEventArgs> Navigated
        {
            add => NavigationService.Navigated += value;
            remove => NavigationService.Navigated -= value;
        }

        /// <summary>
        /// Raised when the region is navigated to content.
        /// </summary>
        public static event EventHandler<NavigationEventArgs> Navigating
        {
            add => NavigationService.Navigating += value;
            remove => NavigationService.Navigating -= value;
        }

        /// <summary>
        /// Raised when a navigation request fails.
        /// </summary>
        public static event EventHandler<NavigationFailedEventArgs> NavigationFailed
        {
            add => NavigationService.NavigationFailed += value;
            remove => NavigationService.NavigationFailed -= value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Navigates to specified view.
        /// </summary>
        /// <typeparam name="TViewModel">View type.</typeparam>
        /// <param name="callback">Action when navigation is completed.</param>
        /// <param name="parameters">The optional parameters.</param>
        public static void NavigateTo<TViewModel>(Action<NavigationResult> callback = null, IEnumerable<KeyValuePair<string, object>> parameters = null) where TViewModel : INavigableWorkspaceViewModel
        {
            NavigateTo(typeof(TViewModel), callback, parameters);
        }

        /// <summary>
        /// Navigates to specified view.
        /// </summary>
        /// <typeparam name="TViewModel">View type.</typeparam>
        /// <param name="paramValue">The parameter value.</param>
        /// <param name="paramKey">The parameter key.</param>
        /// <param name="callback">Action when navigation is completed.</param>
        public static void NavigateTo<TViewModel>(object paramValue = null, string paramKey = ParametersConstants.Id, Action<NavigationResult> callback = null) where TViewModel : INavigableWorkspaceViewModel
        {
            NavigateTo(typeof(TViewModel), paramValue, paramKey, callback);
        }

        /// <summary>
        /// Navigates to specified view.
        /// </summary>
        /// <param name="typeViewModel">View type.</param>
        /// <param name="paramValue">The parameter value.</param>
        /// <param name="paramKey">The parameter key.</param>
        /// <param name="callback">Action when navigation is completed.</param>
        public static void NavigateTo(Type typeViewModel, object paramValue, string paramKey = ParametersConstants.Id, Action<NavigationResult> callback = null)
        {
            NavigateTo(typeViewModel, callback, new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>(paramKey, paramValue)
            });
        }

        /// <summary>
        /// Navigates to specified view.
        /// </summary>
        /// <param name="typeViewModel">View type.</param>
        /// <param name="callback">Action when navigation is completed.</param>
        /// <param name="parameters">The optional parameters.</param>
        public static void NavigateTo(Type typeViewModel, Action<NavigationResult> callback = null, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            var provider = ServiceLocator.Current.GetInstance<IViewModelProvider>();
            var viewModel = provider.GetViewModel(typeViewModel) as INavigableWorkspaceViewModel;
            
            NavigationService.NavigateTo(viewModel, callback, ToNavigationParameters(parameters));
        }

        /// <summary>
        /// Converts parameters.
        /// </summary>
        private static NavigationParameters ToNavigationParameters(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            if (parameters == null) return null;

            var res = new NavigationParameters();

            foreach (var parameter in parameters)
            {
                res.Add(parameter.Key, parameter.Value);
            }

            return res;
        }

        /// <summary>
        /// Go previous page.
        /// </summary>
        public static void GoBack()
        {
            NavigationService.GoBack();
        }

        /// <summary>
        /// Can go previous page.
        /// </summary>
        public static bool CanGoBack()
        {
            return NavigationService.CanGoBack();
        }

        /// <summary>
        /// Go next page.
        /// </summary>
        public static void GoForward()
        {
            NavigationService.GoForward();
        }

        /// <summary>
        /// Can go next page.
        /// </summary>
        public static bool CanGoForward()
        {
            return NavigationService.CanGoForward();
        }

        #endregion Methods
    }
}
