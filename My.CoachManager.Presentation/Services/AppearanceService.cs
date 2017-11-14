using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MahApps.Metro;
using My.CoachManager.Presentation.Core.Services.Interfaces;

namespace My.CoachManager.Presentation.Services
{
    /// <summary>
    /// A static class that manages the appearance of the application.
    /// </summary>
    public class AppearanceService : IAppearanceService
    {
        #region Constructors

        public AppearanceService()
        {
            ThemeManager.IsThemeChanged += ThemeManager_IsThemeChanged;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the accent names.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Accents
        {
            get
            {
                return ThemeManager.Accents.Select(a => a.Name).ToList();
            }
        }

        /// <summary>
        /// Gets the themes names.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Themes
        {
            get
            {
                return ThemeManager.AppThemes.Select(a => a.Name).ToList();
            }
        }

        /// <summary>
        /// Gets or set the theme.
        /// </summary>
        /// <returns></returns>
        public string Theme
        {
            get
            {
                var style = ThemeManager.DetectAppStyle(System.Windows.Application.Current);
                return style != null ? style.Item1.Name : null;
            }
            set
            {
                ChangeTheme(value);
            }
        }

        /// <summary>
        /// Gets or set the accent.
        /// </summary>
        /// <returns></returns>
        public string Accent
        {
            get
            {
                var style = ThemeManager.DetectAppStyle(System.Windows.Application.Current);
                return style != null ? style.Item2.Name : null;
            }
            set
            {
                ChangeAccent(value);
            }
        }

        /// <summary>
        /// Changes the accent of the application.
        /// </summary>
        /// <param name="accentName">The name of the accent color.</param>
        private void ChangeAccent(string accentName)
        {
            ChangeStyle(Theme, accentName);
        }

        /// <summary>
        /// Changes the accent of the application.
        /// </summary>
        /// <param name="themeName">The name of the theme.</param>
        private void ChangeTheme(string themeName)
        {
            ChangeStyle(themeName, Accent);
        }

        /// <summary>
        /// Changes the theme of the application.
        /// </summary>
        /// <param name="themeName">The name of the theme.</param>
        /// <param name="accentName">The name of the accent color.</param>
        private void ChangeStyle(string themeName, string accentName)
        {
            var accent = ThemeManager.GetAccent(accentName);
            var theme = ThemeManager.GetAppTheme(themeName);
            ThemeManager.ChangeAppStyle(System.Windows.Application.Current, accent, theme);

            var key = "pack://application:,,,/My.CoachManager.Presentation;component/Resources/Styles/Base/Colors.xaml";
            var oldThemeResource = System.Windows.Application.Current.Resources.MergedDictionaries.Where(x => x.Source != null).FirstOrDefault(d => d.Source.ToString() == key);
            var newResource = new ResourceDictionary
            {
                Source =
                    new Uri(key)
            };
            if (oldThemeResource != null)
            {
                System.Windows.Application.Current.Resources.MergedDictionaries.Remove(oldThemeResource);
                System.Windows.Application.Current.Resources.MergedDictionaries.Add(newResource);
            }
        }

        /// <summary>
        /// Add an accent theme.
        /// </summary>
        /// <param name="uri">The uri of accent file.</param>
        /// <param name="accentName">The name of the accent color.</param>
        public void AddAccent(string accentName, string uri)
        {
            ThemeManager.AddAccent(accentName, new Uri(uri));
        }

        #endregion Properties

        #region Event

        public virtual event EventHandler StyleChanged;

        /// <summary>
        /// Notigy when the style change.
        /// </summary>
        protected virtual void NotifyStyleChanged()
        {
            if (StyleChanged != null)
            {
                StyleChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called when style change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThemeManager_IsThemeChanged(object sender, OnThemeChangedEventArgs e)
        {
            NotifyStyleChanged();
        }

        #endregion Event
    }
}