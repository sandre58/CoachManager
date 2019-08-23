using System;
using System.Collections.Generic;
using System.Windows.Controls;
using My.CoachManager.Presentation.Wpf.Core.Constants;
using My.CoachManager.Presentation.Wpf.Core.Helpers;
using My.CoachManager.Presentation.Wpf.Core.Ioc;
using My.CoachManager.Presentation.Wpf.Core.Navigation;
using My.CoachManager.Presentation.Wpf.Core.Services;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Wpf.Services
{
    /// <inheritdoc />
    /// <summary>
    ///     The implementation of the contract <see cref="T:My.CoachManager.Presentation.Core.Services.INavigationService" />.
    ///     this class has no need on its own-self, hence explicit implementation.
    /// </summary>
    public class NavigationService : INavigationService
    {
        #region Fields

        private Frame _frame;
        private NavigationContext _currentNavigationContext;
        private readonly IViewModelTypeLocator _viewTypeLocator;
        private readonly IViewModelProvider _viewModelProvider;
        private readonly Dictionary<string, object> _pagesByKey = new Dictionary<string, object>(); 

        #endregion

        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets current workspace.
        /// </summary>
        public INavigableWorkspaceViewModel CurrentWorkspace { get; private set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the key corresponding to the currently displayed page.
        /// </summary>
        /// <value>
        ///     The current page key.
        /// </value>
        public string CurrentPageKey { get; private set; }

        #endregion

        #region Events

        /// <inheritdoc />
        /// <summary>
        /// Raised when the region is about to be navigated to content.
        /// </summary>
        public event EventHandler<NavigationEventArgs> Navigating;

        private void RaiseNavigating(NavigationContext navigationContext)
        {
            Navigating?.Invoke(this, new NavigationEventArgs(navigationContext));
        }

        /// <inheritdoc />
        /// <summary>
        /// Raised when the region is navigated to content.
        /// </summary>
        public event EventHandler<NavigationEventArgs> Navigated;

        private void RaiseNavigated(NavigationContext navigationContext)
        {
            Navigated?.Invoke(this, new NavigationEventArgs(navigationContext));
        }

        /// <inheritdoc />
        /// <summary>
        /// Raised when a navigation request fails.
        /// </summary>
        public event EventHandler<NavigationFailedEventArgs> NavigationFailed;

        private void RaiseNavigationFailed(NavigationContext navigationContext, Exception error)
        {
            NavigationFailed?.Invoke(this, new NavigationFailedEventArgs(navigationContext, error));
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DialogService" /> class.
        /// </summary>
        public NavigationService(IViewModelTypeLocator viewTypeLocator = null, IViewModelProvider viewModelProvider = null)
        {
            _viewTypeLocator = viewTypeLocator ?? new NamingConventionDialogTypeLocator();
            _viewModelProvider = viewModelProvider ?? new ViewModelProvider();
        }

        #endregion
        
        #region INavigationService

        /// <inheritdoc />
        /// <summary>
        ///     Go previous page.
        /// </summary>
        public void GoBack()
        {
            EnsureFrame();

            if (CanGoBack()) _frame.GoBack();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Can go previous page.
        /// </summary>
        public bool CanGoBack()
        {
            EnsureFrame();

            return _frame.CanGoBack;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Go next page.
        /// </summary>
        public void GoForward()
        {
            EnsureFrame();

            if (CanGoForward()) _frame.GoForward();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Can go next page.
        /// </summary>
        public bool CanGoForward()
        {
            EnsureFrame();

            return _frame.CanGoForward;
        }

        /// <inheritdoc />
        /// <summary>
        ///     The navigate to.
        /// </summary>
        /// <param name="pageKey">
        ///     The page key.
        /// </param>
        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        /// <inheritdoc />
        public virtual void NavigateTo(string pageKey, object parameter)
        {
            var type = Type.GetType(pageKey);
            var parameters = new NavigationParameters {{ParametersConstants.Id, parameter}};

            NavigateTo(_viewModelProvider.GetViewModel(type) as INavigableWorkspaceViewModel, null, parameters);
        }

        /// <summary>
        /// Initiates navigation to the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="navigationCallback">A callback to execute when the navigation request is completed.</param>
        /// <param name="navigationParameters">The navigation parameters specific to the navigation request.</param>
        public void NavigateTo(INavigableWorkspaceViewModel target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
        {
            if (target == null) return;

                DoNavigate(target, navigationCallback, navigationParameters);
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Navigate.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="navigationCallback"></param>
        /// <param name="navigationParameters"></param>
        private void DoNavigate(INavigableWorkspaceViewModel viewModel, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
            
            EnsureFrame();

            var viewType = _viewTypeLocator.LocateView(viewModel.GetType());
            var view = GetView(viewType);

            // Navigate in Frame
            _frame.Navigate(view, navigationParameters);

            var navigationContext = new NavigationContext(viewModel, navigationParameters);
            navigationCallback?.Invoke(new NavigationResult(navigationContext, true));

        }

        /// <inheritdoc />
        /// <summary>
        ///     Configure Frame.
        /// </summary>
        /// <param name="frame"></param>
        public void ConfigureFrame(Frame frame)
        {
            _frame = null;
            _frame = frame;

            _frame.Navigating += OnNavigating;
            _frame.Navigated += OnNavigated;
            _frame.NavigationFailed += OnNavigationFailed;
        }
        
        /// <summary>
        /// Calls before navigation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNavigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            
            var navigable = ViewModelHelper.GetImplementerFromViewOrViewModel<INavigableWorkspaceViewModel>(e.Content);

            var navigationContext = new NavigationContext(navigable, (NavigationParameters)e.ExtraData);

            if (_currentNavigationContext != navigationContext)
            {
                _currentNavigationContext = new NavigationContext(navigable, (NavigationParameters)e.ExtraData);
                var oldNavigable = ViewModelHelper.GetImplementerFromViewOrViewModel<INavigableWorkspaceViewModel>(_frame.Content);
                
                ViewModelHelper.ViewAndViewModelAction<INavigableWorkspaceViewModel>(oldNavigable, n => n.OnNavigatedFrom(navigationContext));

                RaiseNavigating(navigationContext);

            } else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Calls after navigation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            CurrentPageKey = e.Content?.ToString();

            CurrentWorkspace = ViewModelHelper.GetImplementerFromViewOrViewModel<INavigableWorkspaceViewModel>(e.Content);
                
            ViewModelHelper.ViewAndViewModelAction<INavigableWorkspaceViewModel>(CurrentWorkspace, n => n.OnNavigatedTo(_currentNavigationContext));

            RaiseNavigated(_currentNavigationContext);
        }

        /// <summary>
        /// Calls when navigation failed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNavigationFailed(object sender, System.Windows.Navigation.NavigationFailedEventArgs e)
        {
            RaiseNavigationFailed(_currentNavigationContext, e.Exception);
        }

        /// <summary>
        ///     Ensure Frame member.
        /// </summary>
        private void EnsureFrame()
        {
            if (_frame == null)
                throw new ArgumentException(@"The frame is not defined. Have you called ConfigureFrame(Frame frame) ?");
        }

        /// <summary>
        /// Get specified view.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private object GetView(Type type)
        {
            var name = type.Name;

            if (!_pagesByKey.ContainsKey(name))
            {
                _pagesByKey.Add(name, Activator.CreateInstance(type));
            }

            return _pagesByKey[name];
        }

        #endregion
    }
}