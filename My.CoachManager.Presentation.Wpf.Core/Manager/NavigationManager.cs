using System;
using System.Collections.Generic;
using System.Windows.Input;
using CommonServiceLocator;
using My.CoachManager.Presentation.Wpf.Core.Constants;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;
using Prism.Commands;
using Prism.Regions;
using INavigationService = My.CoachManager.Presentation.Wpf.Core.Services.INavigationService;

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
        public static INavigableWorkspaceViewModel ActiveView => NavigationService.ActiveView;

        /// <summary>
        /// Gets Global Navigate command.
        /// </summary>
        public static ICommand NavigateCommand => new DelegateCommand<string>(s =>
        {
                Navigate(s);
        });

        #endregion Members

        #region Events

        /// <summary>
        /// Raised when the region is about to be navigated to content.
        /// </summary>
        public static event EventHandler<RegionNavigationEventArgs> Navigated
        {
            add => NavigationService.Navigated += value;
            remove => NavigationService.Navigated -= value;
        }

        /// <summary>
        /// Raised when the region is navigated to content.
        /// </summary>
        public static event EventHandler<RegionNavigationEventArgs> Navigating
        {
            add => NavigationService.Navigating += value;
            remove => NavigationService.Navigating -= value;
        }

        /// <summary>
        /// Raised when a navigation request fails.
        /// </summary>
        public static event EventHandler<RegionNavigationFailedEventArgs> NavigationFailed
        {
            add => NavigationService.NavigationFailed += value;
            remove => NavigationService.NavigationFailed -= value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Navigates to specified view.
        /// </summary>
        /// <typeparam name="TView">View type.</typeparam>
        /// <param name="callback">Action when navigation is completed.</param>
        /// <param name="parameters">The optional parameters.</param>
        public static void NavigateTo<TView>(Action<NavigationResult> callback = null, IEnumerable<KeyValuePair<string, object>> parameters = null) where TView : INavigableWorkspaceViewModel
        {
            NavigateTo(typeof(TView), callback, parameters);
        }

        /// <summary>
        /// Navigates to specified view.
        /// </summary>
        /// <param name="typeView">View type.</param>
        /// <param name="callback">Action when navigation is completed.</param>
        /// <param name="parameters">The optional parameters.</param>
        public static void NavigateTo(Type typeView, Action<NavigationResult> callback = null, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            var path = typeView.Name;
            NavigateTo(path, callback, parameters);
        }

        /// <summary>
        /// Navigates to specified view.
        /// </summary>
        /// <param name="pagePath">The Uri.</param>
        /// <param name="callback">Action when navigation is completed.</param>
        /// <param name="parameters">The optional parameters.</param>
        public static void NavigateTo(string pagePath, Action<NavigationResult> callback = null, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            NavigationService.NavigateTo(pagePath, callback, ToNavigationParameters(parameters));
        }

        /// <summary>
        /// Navigates to specified view.
        /// </summary>
        /// <param name="navigatePath">Full Uri (with parameters).</param>
        /// <param name="callback">Action when navigation is completed.</param>
        public static void Navigate(string navigatePath, Action<NavigationResult> callback = null)
        {
            var splitPath = navigatePath.Split('?');
            var path = splitPath[0];
            var parameters = splitPath.Length > 1 ? new NavigationParameters(splitPath[1]) : null;

            NavigationService.NavigateTo(path, callback, parameters);
        }

        /// <summary>
        /// Navigates to specified view.
        /// </summary>
        /// <param name="pagePath">The Uri.</param>
        /// <param name="paramValue">The parameter value.</param>
        /// <param name="paramKey">The parameter key.</param>
        /// <param name="callback">Action when navigation is completed.</param>
        public static void NavigateTo(string pagePath, object paramValue, string paramKey = ParametersConstants.Id, Action<NavigationResult> callback = null)
        {
            NavigateTo(pagePath, callback, new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>(paramKey, paramValue)
            });
        }

        /// <summary>
        /// Navigates to specified view.
        /// </summary>
        /// <typeparam name="TView">View type.</typeparam>
        /// <param name="paramValue">The parameter value.</param>
        /// <param name="paramKey">The parameter key.</param>
        /// <param name="callback">Action when navigation is completed.</param>
        public static void NavigateTo<TView>(object paramValue = null, string paramKey = ParametersConstants.Id, Action<NavigationResult> callback = null) where TView : INavigableWorkspaceViewModel
        {
            NavigateTo(typeof(TView), paramValue, paramKey, callback);
        }

        /// <summary>
        /// Navigates to specified view.
        /// </summary>
        /// <param name="typeView">View type.</param>
        /// <param name="paramValue">The parameter value.</param>
        /// <param name="paramKey">The parameter key.</param>
        /// <param name="callback">Action when navigation is completed.</param>
        public static void NavigateTo(Type typeView, object paramValue, string paramKey = ParametersConstants.Id, Action<NavigationResult> callback = null)
        {
            NavigateTo(typeView, callback, new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>(paramKey, paramValue)
            });
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