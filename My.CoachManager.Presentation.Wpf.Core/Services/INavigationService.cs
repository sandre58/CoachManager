﻿using System;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;
using Prism.Regions;

namespace My.CoachManager.Presentation.Wpf.Core.Services
{
    /// <summary>
    /// Interface abstracting the interaction between view models and views when it comes to
    /// opening page using the MVVM pattern in WPF.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Raised when the region is about to be navigated to content.
        /// </summary>
        event EventHandler<RegionNavigationEventArgs> Navigating;

        /// <summary>
        /// Raised when the region is navigated to content.
        /// </summary>
        event EventHandler<RegionNavigationEventArgs> Navigated;

        /// <summary>
        /// Raised when a navigation request fails.
        /// </summary>
        event EventHandler<RegionNavigationFailedEventArgs> NavigationFailed;

        /// <summary>
        /// Gets active view.
        /// </summary>
        INavigableWorkspaceViewModel ActiveView { get; }

        /// <summary>
        /// Navigates to specified view.
        /// </summary>
        /// <param name="pagePath">The Uri.</param>
        /// <param name="parameters">The optionals parameters.</param>
        /// <param name="callback">Action when navigation is completed.</param>
        void NavigateTo(string pagePath, Action<NavigationResult> callback = null, NavigationParameters parameters = null);

        /// <summary>
        /// Go previous page.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Can go previous page.
        /// </summary>
        bool CanGoBack();

        /// <summary>
        /// Go next page.
        /// </summary>
        void GoForward();

        /// <summary>
        /// Can go next page.
        /// </summary>
        bool CanGoForward();
    }
}