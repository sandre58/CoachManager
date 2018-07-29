using System;
using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Core.Services;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Wpf.Services
{
    /// <inheritdoc />
    /// <summary>
    /// The implementation of the contract <see cref="T:My.CoachManager.Presentation.Prism.Core.Services.INavigationService" />.
    /// this class has no need on its ownself, hence explicit implementation.
    /// </summary>
    public class NavigationService : INavigationService
    {
        #region Fields

        private readonly IRegionManager _regionManager;
        private IRegionNavigationJournal _journal;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="NavigationService"/>.
        /// </summary>
        /// <param name="regionManager"></param>
        public NavigationService(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _regionManager.Regions.CollectionChanged += OnRegionChanged;
        }

        #endregion Constructors

        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets active view.
        /// </summary>
        public object ActiveView
        {
            get
            {
                if (_regionManager.Regions.ContainsRegionWithName(RegionNames.WorkspaceRegion))
                {
                    return _regionManager.Regions[RegionNames.WorkspaceRegion].ActiveViews.FirstOrDefault();
                }
                return null;
            }
        }

        #endregion Members

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Navigates to specified view.
        /// </summary>
        /// <param name="pagePath">The Uri.</param>
        /// <param name="callback">Action when navigation is completed.</param>
        /// <param name="parameters">The optionals parameters.</param>
        public void NavigateTo(string pagePath, Action<NavigationResult> callback = null, NavigationParameters parameters = null)
        {
            if (!string.IsNullOrEmpty(pagePath))
            {
                var newUri = new Uri(pagePath + parameters, UriKind.Relative);
                var activeUri = _journal?.CurrentEntry?.Uri;
                if (!Equals(newUri, activeUri))
                {
                    _regionManager.RequestNavigate(RegionNames.WorkspaceRegion, newUri, callback);
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Go previous page.
        /// </summary>
        public void GoBack()
        {
            _journal?.GoBack();
        }

        /// <inheritdoc />
        /// <summary>
        /// Can go previous page.
        /// </summary>
        public bool CanGoBack()
        {
            return _journal?.CanGoBack ?? false;
        }

        /// <inheritdoc />
        /// <summary>
        /// Go next page.
        /// </summary>
        public void GoForward()
        {
            _journal?.GoForward();
        }

        /// <inheritdoc />
        /// <summary>
        /// Can go next page.
        /// </summary>
        public bool CanGoForward()
        {
            return _journal?.CanGoBack ?? false;
        }

        /// <summary>
        /// Calls when regions changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRegionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (_journal == null)
            {
                if (_regionManager.Regions.ContainsRegionWithName(RegionNames.WorkspaceRegion))
                {
                    _journal = _regionManager.Regions[RegionNames.WorkspaceRegion].NavigationService.Journal;
                }
            }
        }

        #endregion Methods
    }
}