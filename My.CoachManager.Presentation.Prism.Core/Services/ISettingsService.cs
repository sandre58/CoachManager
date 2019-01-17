using System;

namespace My.CoachManager.Presentation.Prism.Core.Services
{
    /// <summary>
    /// Interface abstracting the interaction with toast notification.
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Save Skin
        /// </summary>
        void SaveSkin();

        /// <summary>
        /// Load Skin
        /// </summary>
        void LoadSkin();

        /// <summary>
        /// Save Roster Id.
        /// </summary>
        void SaveRoster(int rosterId);

        /// <summary>
        /// Save Roster Id.
        /// </summary>
        int GetRosterId();

        /// <summary>
        /// Save Roster Id.
        /// </summary>
       TimeSpan GetDefaultTrainingStartTime();

        /// <summary>
        /// Save Roster Id.
        /// </summary>
        TimeSpan GetDefaultTrainingDuration();
    }
}