using My.CoachManager.Presentation.Wpf.Core.Services;
using My.CoachManager.Presentation.Wpf.Properties;

namespace My.CoachManager.Presentation.Wpf.Services
{
    /// <inheritdoc />
    /// <summary>
    /// Provides methods to display toast notifications.
    /// </summary>
    public class SettingsService : ISettingsService
    {
        /// <inheritdoc />
        /// <summary>
        /// Save a setting.
        /// </summary>
        public void Set<T>(string key, T value)
        {
            Settings.Default[key] = value;

            Settings.Default.Save();
        }

        /// <inheritdoc />
        /// <summary>
        /// Get a setting.
        /// </summary>
        public T Get<T>(string key)
        {
            return (T)Settings.Default[key];
        }
    }
}
