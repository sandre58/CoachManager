using System;
using CommonServiceLocator;
using My.CoachManager.CrossCutting.Core.Configurations;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Wpf.Core.Services;

namespace My.CoachManager.Presentation.Wpf.Modules.Shared
{
    /// <inheritdoc />
    /// <summary>
    /// Database Configuration Keys.
    /// </summary>
    public class AppManager : ConfigurationManagerBase
    {
        #region Fields

        private static ISettingsService _settingsService;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets Settings Service.
        /// </summary>
        private static ISettingsService SettingsService => _settingsService ??
                                                               (_settingsService = ServiceLocator.Current.GetInstance<ISettingsService>());

        /// <summary>
        /// Gets the club logo.
        /// </summary>
        public static string ClubLogo => SettingsService.Get<string>(nameof(ClubLogo));

        /// <summary>
        /// Gets the club logo.
        /// </summary>
        public static string ClubName => SettingsService.Get<string>(nameof(ClubName));

        /// <summary>
        /// Gets the default country.
        /// </summary>
        public static string DefaultCountry => SettingsService.Get<string>(nameof(DefaultCountry));

        /// <summary>
        /// Gets the default country.
        /// </summary>
        public static TimeSpan DefaultTrainingStartTime => SettingsService.Get<TimeSpan>(nameof(DefaultTrainingStartTime));

        /// <summary>
        /// Gets the default country.
        /// </summary>
        public static TimeSpan DefaultTrainingDuration => SettingsService.Get<TimeSpan>(nameof(DefaultTrainingDuration));

        /// <summary>
        /// Gets items per page.
        /// </summary>
        public static int ItemsPerPage => SettingsService.Get<int>(nameof(ItemsPerPage));

        /// <summary>
        /// Gets the roster.
        /// </summary>
        public static RosterModel Roster { get; private set; }

        #endregion

        #region Fields

        public static void InitializeRoster(RosterModel roster)
        {
            Roster = roster;
        }

        #endregion
    }
}