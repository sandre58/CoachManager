using System;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using My.CoachManager.Presentation.Wpf.Core.Constants;
using My.CoachManager.Presentation.Wpf.Core.Navigation;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Wpf.Core.ViewModels.Base
{
    public abstract class NavigableWorkspaceViewModel : WorkspaceViewModel, INavigableWorkspaceViewModel
    {

        #region Fields

        /// <summary>
        /// Gets navigation parameters.
        /// </summary>
        private NavigationParameters _navigationParameters;

        #endregion

        #region Members

        /// <summary>
        /// Gets navigation id.
        /// </summary>
        public int NavigationId { get; private set; }

        /// <summary>
        /// Gets selectedTabIndex.
        /// </summary>
        public int SelectedTabIndex { get; set; }

        /// <summary>
        /// Gets or sets goto tab command.
        /// </summary>
        public RelayCommand<object> GotToTabCommand { get; set; }

        #endregion Members

        #region Methods

        #region Initialisation

        /// <inheritdoc />
        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            GotToTabCommand = new RelayCommand<object>(GotToTab, CanGotToTab);
        }

        #endregion Initialisation

        #region GotToTab

        /// <summary>
        /// Go to tab.
        /// </summary>
        /// <param name="tab"></param>
        protected virtual void GotToTab(object tab)
        {
            if (tab != null) SelectedTabIndex = int.Parse(tab.ToString());
        }

        /// <summary>
        /// Can Got To Tab ?
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        protected virtual bool CanGotToTab(object tab)
        {
            return true;
        }

        #endregion

        /// <inheritdoc />
        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if(navigationContext.Parameters != null) _navigationParameters = navigationContext.Parameters;

            NavigationId = GetParameter(ParametersConstants.Id, NavigationId);
            SelectedTabIndex = GetParameter(ParametersConstants.Tab, SelectedTabIndex);

            OnNavigatedToCore(navigationContext);
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        protected virtual void OnNavigatedToCore(NavigationContext navigationContext)
        {
            if(State == ScreenState.NotLoaded) Refresh();
        }

        /// <inheritdoc />
        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns>True if this instance accepts the navigation request; otherwise, False.</returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return _navigationParameters != null && _navigationParameters.ToString().Equals(navigationContext.Parameters.ToString());
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

        /// <inheritdoc />
        /// <summary>
        /// Return a parameters;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetParameter<T>(string key, T defaultValue = default)
        {
            if (_navigationParameters != null && _navigationParameters.Any(x => x.Key == key))
            {
                var t = typeof(T);
                if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                    t = t.GetGenericArguments()[0];

                return (T)Convert.ChangeType(_navigationParameters[key], t);
            }

            return defaultValue != null ? defaultValue : default;
        }

        #endregion Methods
    }
}