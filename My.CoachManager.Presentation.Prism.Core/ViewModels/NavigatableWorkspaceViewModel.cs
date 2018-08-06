using System.Windows.Input;
using My.CoachManager.Presentation.Prism.Core.Manager;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class NavigatableWorkspaceViewModel : WorkspaceViewModel, INavigatableWorkspaceViewModel
    {
        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether this instance should be kept-alive upon deactivation.
        /// </summary>
        public virtual bool KeepAlive => true;

        #endregion Members

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all Data.
        /// </summary>
        protected override void InitializeShortcuts()
        {
            base.InitializeShortcuts();

            KeyboardShortcuts.Add(new KeyBinding(RefreshCommand, Key.F5, ModifierKeys.None));
        }

        #endregion Initialization

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            KeyboardManager.RegisterWorkspaceShortcuts(KeyboardShortcuts);
            //if (State == ScreenState.NotLoaded)
            //{
            OnNavigatedToCore(navigationContext);
            //}
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        protected virtual void OnNavigatedToCore(NavigationContext navigationContext)
        {
            RefreshDataAsync();
        }

        /// <inheritdoc />
        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns>True if this instance accepts the navigation request; otherwise, False.</returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <inheritdoc />
        /// <summary>
        /// Called when the implementer is being navigated away from.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            KeyboardManager.RemoveWorkspaceShortcuts(KeyboardShortcuts);
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

        #endregion Methods
    }
}