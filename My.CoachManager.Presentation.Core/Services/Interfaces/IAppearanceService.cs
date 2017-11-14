using System;
using System.Collections.Generic;

namespace My.CoachManager.Presentation.Core.Services.Interfaces
{
    /// <summary>
    /// The appearance service interface.
    /// </summary>
    public interface IAppearanceService
    {
        /// <summary>
        /// Get or Set the accent.
        /// </summary>
        string Accent { get; set; }

        /// <summary>
        /// Get accents.
        /// </summary>
        IEnumerable<string> Accents { get; }

        /// <summary>
        /// Get or Set the theme.
        /// </summary>
        string Theme { get; set; }

        /// <summary>
        /// Get themes.
        /// </summary>
        IEnumerable<string> Themes { get; }

        /// <summary>
        /// Add an accent theme.
        /// </summary>
        /// <param name="uri">The uri of accent file.</param>
        /// <param name="accentName">The name of the accent color.</param>
        void AddAccent(string accentName, string uri);

        /// <summary>
        /// Notify the style change.
        /// </summary>
        /// <returns></returns>
        event EventHandler StyleChanged;
    }
}