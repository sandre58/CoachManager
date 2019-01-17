using System;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Wpf.Properties;

namespace My.CoachManager.Presentation.Prism.Wpf.Services
{
    /// <summary>
    /// Provides methods to display toast notifications.
    /// </summary>
    public class SettingsService : ISettingsService
    {

        #region ISettingsService

        /// <summary>
        /// Save Skin
        /// </summary>
        public void SaveSkin()
        {
            Settings.Default.DefaultTheme = SkinManager.SkinManager.CurrentTheme.Name;
            Settings.Default.DefaultAccent = SkinManager.SkinManager.CurrentAccent.Name;
            Settings.Default.DefaultMenu = SkinManager.SkinManager.CurrentMenu.Name;

            Settings.Default.Save();
        }

        /// <summary>
        /// Load Skin
        /// </summary>
        public void LoadSkin()
        {
            SkinManager.SkinManager.ApplyTheme(Settings.Default.DefaultTheme);
            SkinManager.SkinManager.ApplyAccent(Settings.Default.DefaultAccent);
            SkinManager.SkinManager.ApplyMenu(Settings.Default.DefaultMenu);
        }

        /// <summary>
        /// Save Roster Id.
        /// </summary>
        public void SaveRoster(int rosterId)
        {
            Settings.Default.RosterId = rosterId;

            Settings.Default.Save();
        }

        /// <summary>
        /// Save Roster Id.
        /// </summary>
        public int GetRosterId()
        {
            return Settings.Default.RosterId;
        }

        /// <summary>
        /// Save Roster Id.
        /// </summary>
        public TimeSpan GetDefaultTrainingStartTime()
        {
            return TimeSpan.Parse(Settings.Default.DefaultTrainingStartTime);
        }

        /// <summary>
        /// Save Roster Id.
        /// </summary>
        public TimeSpan GetDefaultTrainingDuration()
        {
            return TimeSpan.Parse(Settings.Default.DefaultTrainingDuration);
        }

        #endregion

    }
}