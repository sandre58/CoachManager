using System;
using System.Linq;
using My.CoachManager.Presentation.Core.Constants;
using My.CoachManager.Presentation.Core.ViewModels.Interfaces;
using Prism.Commands;
using Prism.Regions;

namespace My.CoachManager.Presentation.Core.ViewModels
{
    public abstract class NavigatableWorkspaceViewModel : WorkspaceViewModel, INavigatableWorkspaceViewModel
    {

        #region Fields

        /// <summary>
        /// Gets navigation parameters.
        /// </summary>
        private NavigationParameters _navigationParameters;

        #endregion

        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether this instance should be kept-alive upon deactivation.
        /// </summary>
        public virtual bool KeepAlive => true;

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
        public DelegateCommand<object> GotToTabCommand { get; set; }

        #endregion Members

        #region Methods

        #region Initialisation

        /// <inheritdoc />
        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            GotToTabCommand = new DelegateCommand<object>(GotToTab, CanGotToTab);
        }

        #endregion Initialisation

        #region GotToTab

        /// <summary>
        /// Go to tab.
        /// </summary>
        /// <param name="tab"></param>
        protected void GotToTab(object tab)
        {
            if (tab != null) SelectedTabIndex = int.Parse(tab.ToString());
        }

        /// <summary>
        /// Can Got To Tab ?
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        protected bool CanGotToTab(object tab)
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
            _navigationParameters = navigationContext.Parameters;

            NavigationId = GetParameter(ParametersConstants.Id, NavigationId);
            SelectedTabIndex = GetParameter(ParametersConstants.Tab, SelectedTabIndex);

            OnNavigatedToCore(_navigationParameters);
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
            return _navigationParameters.ToString().Equals(navigationContext.Parameters.ToString());
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

        /// <summary>
        /// Return a parameters;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetParameter<T>(string key, T defaultValue = default(T))
        {
            if (_navigationParameters.Any(x => x.Key == key))
            {
                Type t = typeof(T);
                if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                    t = t.GetGenericArguments()[0];

                return (T)Convert.ChangeType(_navigationParameters[key], t);
            }

            return defaultValue != null ? defaultValue : default(T);
        }

        #endregion Methods
    }
}