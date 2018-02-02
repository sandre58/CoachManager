using System;
using System.Collections.Generic;
using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Core.Services;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Wpf.Services
{
    /// <summary>
    /// The implementation of the contract <see cref="INavigationService"/>.
    /// this class has no need on its ownself, hence explicit implementation.
    /// </summary>
    public class NavigationService : INavigationService
    {
        #region Fields

        private readonly IRegionManager _regionManager;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="NavigationService"/>.
        /// </summary>
        /// <param name="regionManager"></param>
        public NavigationService(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Navigates to specified view.
        /// </summary>
        /// <typeparam name="TView">View type.</typeparam>
        /// <param name="parameters">Optional parameters.</param>
        public void NavigateTo<TView>(IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            NavigateTo(typeof(TView), parameters);
        }

        /// <summary>
        /// Navigates to specified view.
        /// </summary>
        /// <param name="typeView"></param>
        /// <param name="parameters">Optional parameters.</param>
        public void NavigateTo(Type typeView, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            var path = typeView.Name.ToLower();
            NavigateTo(path, parameters);
        }

        public void NavigateTo(string pagePath, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            if (!string.IsNullOrEmpty(pagePath))
            {
                var parametersStr = parameters != null ? ToNavigationParameters(parameters).ToString() : "";
                var newUri = new Uri(pagePath + parametersStr, UriKind.Relative);
                var currentEntry = _regionManager.Regions[RegionNames.WorkspaceRegion].NavigationService.Journal
                    .CurrentEntry;
                var activeUri = currentEntry != null ? currentEntry.Uri : null;
                if (!Equals(newUri, activeUri))
                    _regionManager.RequestNavigate(RegionNames.WorkspaceRegion, newUri);
            }
        }

        public void NavigateTo(string pagePath, object paramValue, string paramKey = "Id")
        {
            NavigateTo(pagePath, new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>(paramKey, paramValue)
            });
        }

        public void NavigateTo<TView>(object paramValue = null, string paramKey = "Id")
        {
            NavigateTo(typeof(TView), paramValue, paramKey);
        }

        public void NavigateTo(Type typeView, object paramValue, string paramKey = "Id")
        {
            NavigateTo(typeView, new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>(paramKey, paramValue)
            });
        }

        /// <summary>
        /// Gets Navigation Parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected NavigationParameters ToNavigationParameters(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var res = new NavigationParameters();

            foreach (var parameter in parameters)
            {
                res.Add(parameter.Key, parameter.Value);
            }

            return res;
        }

        #endregion Methods
    }
}