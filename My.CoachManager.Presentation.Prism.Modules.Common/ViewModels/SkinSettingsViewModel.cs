using System.Collections.Generic;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.SkinManager;

namespace My.CoachManager.Presentation.Prism.Modules.Common.ViewModels
{
    public class SkinSettingsViewModel : ScreenViewModel
    {

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
        public Accent SelectedAccent { get; set; }

        /// <summary>
        /// Gets or sets the selected theme.
        /// </summary>
        public Theme SelectedTheme { get; set; }

        #endregion Members

        #region Initialisation

        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all Data.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            Accents = SkinManager.SkinManager.Accents;
            Themes = SkinManager.SkinManager.Themes;

            SelectedAccent = SkinManager.SkinManager.CurrentAccent;
            SelectedTheme = SkinManager.SkinManager.CurrentTheme;
        }

        #endregion Initialisation

        #region PropertyChanged

        /// <summary>
        /// Occurs when SelectedTheme changed.
        /// </summary>
        protected void OnSelectedThemeChanged()
        {
            SkinManager.SkinManager.ApplyTheme(SelectedTheme);
        }

        /// <summary>
        /// Occurs when SelectedAccent changed.
        /// </summary>
        protected void OnSelectedAccentChanged()
        {
            SkinManager.SkinManager.ApplyAccent(SelectedAccent);
        }

        #endregion
    }
}