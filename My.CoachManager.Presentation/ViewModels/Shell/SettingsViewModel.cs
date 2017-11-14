using System.Collections.Generic;
using My.CoachManager.Presentation.Core.Services.Interfaces;
using My.CoachManager.Presentation.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Enums;
using My.CoachManager.Presentation.Resources.Strings.Screens;

namespace My.CoachManager.Presentation.ViewModels.Shell
{
    /// <summary>
    /// ViewModel for the settings window.
    /// </summary>
    public class SettingsViewModel : FlyoutViewModel
    {
        #region Fields

        private readonly IAppearanceService _appearanceService;
        private string _selectedAccent;
        private string _selectedTheme;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the list of the accent color names.
        /// </summary>
        public IList<string> AccentColorlist { get; private set; }

        /// <summary>
        /// Gets the name of the base themes.
        /// </summary>
        public IList<string> ThemeColorlist { get; private set; }

        /// <summary>
        /// Gets or sets the selected accent color.
        /// </summary>
        public string SelectedAccent
        {
            get { return _selectedAccent; }
            set
            {
                if (value == _selectedAccent)
                    return;
                _selectedAccent = value;
                NotifyOfPropertyChange();
                AccentChangeRequested();
            }
        }

        /// <summary>
        /// Gets or sets the selected theme.
        /// </summary>
        public string SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                if (value == _selectedTheme)
                    return;
                _selectedTheme = value;
                NotifyOfPropertyChange();
                ThemeChangeRequested();
            }
        }

        /// <summary>
        /// Get the display Name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return ShellResources.Settings;
            }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes an instance of <see cref="SettingsViewModel"/>.
        /// </summary>
        public SettingsViewModel()
        {
            _appearanceService = ServiceLocator.AppearanceService;

            if (_appearanceService != null)
            {
                _appearanceService.StyleChanged += OnAppearanceServiceStyleChanged;

                AccentColorlist = new List<string>(_appearanceService.Accents);
                ThemeColorlist = new List<string>(_appearanceService.Themes);
                SelectedAccent = _appearanceService.Accent;
                SelectedTheme = _appearanceService.Theme;

                Position = FlyoutVisibilityPosition.Right;
                Theme = FlyoutTheme.AccentedTheme;
            }
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Sets the requested theme to the application's appearance.
        /// </summary>
        private void ThemeChangeRequested()
        {
            if (_appearanceService != null)
                _appearanceService.Theme = SelectedTheme;
        }

        /// <summary>
        /// Sets the requested accent to the application's appearance.
        /// </summary>
        private void AccentChangeRequested()
        {
            if (_appearanceService != null)
                _appearanceService.Accent = SelectedAccent;
        }

        /// <summary>
        /// Called when style change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAppearanceServiceStyleChanged(object sender, System.EventArgs e)
        {
            if (_appearanceService != null)
            {
                SelectedAccent = _appearanceService.Accent;
                SelectedTheme = _appearanceService.Theme;
            }
        }

        #endregion Methods
    }
}