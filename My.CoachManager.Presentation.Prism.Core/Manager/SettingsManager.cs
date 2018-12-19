using My.CoachManager.Presentation.Prism.Core.Services;
using Microsoft.Practices.ServiceLocation;

namespace My.CoachManager.Presentation.Prism.Core.Manager
{
    /// <summary>
    /// Provides methods and properties to display toast notification.
    /// </summary>
    public static class SettingsManager
    {
        #region Fields

        private static ISettingsService _settingsService;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets Notification Service.
        /// </summary>
        private static ISettingsService SettingsService => _settingsService ??
                                                              (_settingsService = ServiceLocator.Current.GetInstance<ISettingsService>());

        #endregion Members

        #region Methods

        /// <summary>
        /// Save Skin
        /// </summary>
       public static void SaveSkin()
        {
            SettingsService.SaveSkin();
        }

        /// <summary>
        /// Load Skin
        /// </summary>
        public static void LoadSkin()
        {
            SettingsService.LoadSkin();
        }

        /// <summary>
        /// Save Roster Id.
        /// </summary>
        public static void SaveRoster(int rosterId)
        {
            SettingsService.SaveRoster(rosterId);
        }

        /// <summary>
        /// Save Roster Id.
        /// </summary>
        public static int GetRosterId()
        {
            return SettingsService.GetRosterId();
        }

        #endregion Methods
    }
}