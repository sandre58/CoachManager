namespace My.CoachManager.Presentation.Mvvm.Core.Services
{
    /// <summary>
    /// Provides methods to display toast notifications.
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Save a setting.
        /// </summary>
        void Set<T>(string key, T value);

        /// <summary>
        /// Get a setting.
        /// </summary>
        T Get<T>(string key);
    }
}
