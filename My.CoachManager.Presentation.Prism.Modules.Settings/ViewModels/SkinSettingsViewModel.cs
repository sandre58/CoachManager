using System.Collections.Generic;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.SkinManager;

namespace My.CoachManager.Presentation.Prism.Modules.Settings.ViewModels
{
    public class SkinSettingsViewModel : ScreenViewModel, ISkinSettingsViewModel
    {
        #region Fields

        private Theme _selectedTheme;
        private Accent _selectedAccent;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets the list of the accent color names.
        /// </summary>
        public IList<Accent> Accents { get; private set; }

        /// <summary>
        /// Gets the name of the base themes.
        /// </summary>
        public IList<Theme> Themes { get; private set; }

        /// <summary>
        /// Gets or sets the selected accent color.
        /// </summary>
        public Accent SelectedAccent
        {
            get { return _selectedAccent; }
            set
            {
                SetProperty(ref _selectedAccent, value, () =>
                {
                    SkinManager.SkinManager.ApplyAccent(value);
                });
            }
        }

        /// <summary>
        /// Gets or sets the selected theme.
        /// </summary>
        public Theme SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                SetProperty(ref _selectedTheme, value, () =>
                {
                    SkinManager.SkinManager.ApplyTheme(value);
                });
            }
        }

        #endregion Members

        #region Constructor

        /// <summary>
        /// Initializes an instance of <see cref="SkinSettingsViewModel"/>.
        /// </summary>
        public SkinSettingsViewModel()
        {
            Accents = SkinManager.SkinManager.Accents;
            Themes = SkinManager.SkinManager.Themes;

            SelectedAccent = SkinManager.SkinManager.CurrentAccent;
            SelectedTheme = SkinManager.SkinManager.CurrentTheme;
        }

        #endregion Constructor
    }
}