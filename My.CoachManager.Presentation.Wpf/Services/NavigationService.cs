using System;
using System.Diagnostics;
using System.Linq;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Wpf.Core;
using My.CoachManager.Presentation.Wpf.Core.Services;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;
using Prism.Regions;

namespace My.CoachManager.Presentation.Wpf.Services
{
    /// <inheritdoc />
    /// <summary>
    /// The implementation of the contract <see cref="T:My.CoachManager.Presentation.Core.Services.INavigationService" />.
    /// this class has no need on its ownself, hence explicit implementation.
    /// </summary>
    public class NavigationService : INavigationService
    {
        #region Fields

        private readonly IRegionManager _regionManager;
        private IRegionNavigationService _regionNavigationService;
        private Stopwatch _watch;

        #endregion Fields

        #region Events

        /// <inheritdoc />
        /// <summary>
        /// Raised when the region is about to be navigated to content.
        /// </summary>
        public event EventHandler<RegionNavigationEventArgs> Navigating;

        /// <inheritdoc />
        /// <summary>
        /// Raised when the region is navigated to content.
        /// </summary>
        public event EventHandler<RegionNavigationEventArgs> Navigated;

        /// <inheritdoc />
        /// <summary>
        /// Raised when a navigation request fails.
        /// </summary>
        public event EventHandler<RegionNavigationFailedEventArgs> NavigationFailed;

        #endregion

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
        public INavigableWorkspaceViewModel ActiveView
        {
            get
            {
                if (_regionManager.Regions.ContainsRegionWithName(RegionNames.WorkspaceRegion))
                {
                    return (INavigableWorkspaceViewModel)_regionManager.Regions[RegionNames.WorkspaceRegion].ActiveViews.FirstOrDefault();
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
        /// <param name="parameters">The optional parameters.</param>
        public void NavigateTo(string pagePath, Action<NavigationResult> callback = null, NavigationParameters parameters = null)
        {
            if (!string.IsNullOrEmpty(pagePath))
            {
                var newUri = new Uri(pagePath + parameters, UriKind.Relative);
                var activeUri = _regionNavigationService?.Journal?.CurrentEntry?.Uri;
                if (!Equals(newUri, activeUri))
                {
                    _regionManager.RequestNavigate(RegionNames.WorkspaceRegion, newUri, e =>
                        {
                            callback?.Invoke(e);
                        });
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Go previous page.
        /// </summary>
        public void GoBack()
        {
            _regionNavigationService?.Journal?.GoBack();
        }

        /// <inheritdoc />
        /// <summary>
        /// Can go previous page.
        /// </summary>
        public bool CanGoBack()
        {
            return _regionNavigationService?.Journal?.CanGoBack ?? false;
        }

        /// <inheritdoc />
        /// <summary>
        /// Go next page.
        /// </summary>
        public void GoForward()
        {
            _regionNavigationService?.Journal?.GoForward();
        }

        /// <inheritdoc />
        /// <summary>
        /// Can go next page.
        /// </summary>
        public bool CanGoForward()
        {
            return _regionNavigationService?.Journal?.CanGoForward ?? false;
        }

        /// <summary>
        /// Calls when regions changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRegionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (_regionNavigationService == null)
            {
                if (_regionManager.Regions.ContainsRegionWithName(RegionNames.WorkspaceRegion))
                {
                    _regionNavigationService = _regionManager.Regions[RegionNames.WorkspaceRegion].NavigationService;
                    _regionNavigationService.Navigating += delegate (object o, RegionNavigationEventArgs args)
                    {
                        _watch = new Stopwatch();
                        _watch.Start();
                        LogManager.Trace($"Navigating to {args.Uri}");
                        Navigating?.Invoke(o, args);
                    };
                    _regionNavigationService.Navigated += delegate (object o, RegionNavigationEventArgs args)
                    {
                        _watch?.Stop();
                        LogManager.Trace($"Navigated to {args.Uri} : {_watch?.Elapsed}");
                        Navigated?.Invoke(o, args);
                    };
                    _regionNavigationService.NavigationFailed += delegate (object o, RegionNavigationFailedEventArgs args)
                    {
                        _watch?.Stop();
                        LogManager.Trace($"Fail to navigate to {args.Uri} : {args.Error.Message}");
                        NavigationFailed?.Invoke(o, args);
                    };
                }
            }
        }

        #endregion Methods
    }
}