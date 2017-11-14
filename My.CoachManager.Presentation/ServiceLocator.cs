using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.CrossCutting.Unity;
using My.CoachManager.Presentation.Core.Services.Interfaces;
using My.CoachManager.Presentation.Services;

namespace My.CoachManager.Presentation
{
    public static class ServiceLocator
    {
        #region Properties

        /// <summary>
        /// The dialog service.
        /// </summary>
        public static IDialogService<CustomDialogSettings> DialogService
        {
            get { return UnityFactory.Resolve<IDialogService<CustomDialogSettings>>(); }
        }

        /// <summary>
        /// The appearance service.
        /// </summary>
        public static IAppearanceService AppearanceService
        {
            get { return UnityFactory.Resolve<IAppearanceService>(); }
        }

        /// <summary>
        /// The navigation service.
        /// </summary>
        public static IMenuNavigationService NavigationService
        {
            get { return UnityFactory.Resolve<IMenuNavigationService>(); }
        }

        /// <summary>
        /// The authentification service.
        /// </summary>
        public static IAuthenticationService AuthenticationService
        {
            get { return UnityFactory.Resolve<IAuthenticationService>(); }
        }

        /// <summary>
        /// The navigation service.
        /// </summary>
        public static ILogger Logger
        {
            get { return UnityFactory.Resolve<ILogger>(); }
        }

        #endregion Properties
    }
}