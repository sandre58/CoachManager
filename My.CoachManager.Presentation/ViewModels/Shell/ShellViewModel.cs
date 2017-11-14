using System.Collections.ObjectModel;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using My.CoachManager.Presentation.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces;
using My.CoachManager.Presentation.Services;
using My.CoachManager.Presentation.ViewModels.Home;

namespace My.CoachManager.Presentation.ViewModels.Shell
{
    /// <summary>
    /// ViewModel for the main window.
    /// </summary>
    public class ShellViewModel : ScreenViewModel
    {
        #region Fields

        private ObservableCollection<IFlyoutViewModel> _flyouts;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructor used by Designer.
        /// </summary>
        public ShellViewModel()
        {
            _flyouts = new ObservableCollection<IFlyoutViewModel>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets an instance of <see cref="SettingsViewModel"/> that caters to the SettingsView.
        /// </summary>
        //public SettingsViewModel SettingsView { get; private set; }

        /// <summary>
        /// Gets or sets the list of <see cref="FlyoutViewModel"/>
        /// </summary>
        public ObservableCollection<IFlyoutViewModel> Flyouts
        {
            get { return _flyouts; }
            set
            {
                if (value == _flyouts)
                    return;
                _flyouts = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Gets an instance of <see cref="SettingsViewModel"/> that caters to the SettingsView.
        /// </summary>
        public IFlyoutViewModel SettingsView { get; private set; }

        /// <summary>
        /// Gets or sets an instance of <see cref="MainViewModel"/> That is shown in the MainWindow.
        /// </summary>
        public IScreen MainView { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Load the main interface.
        /// </summary>
        public void Load(UserViewModel user)
        {
            MainView = new MainViewModel(user);
            NotifyOfPropertyChange(() => MainView);

            ServiceLocator.NavigationService.NavigateToView<HomeViewModel>();
        }

        /// <summary>
        /// Called when it's initialized.
        /// </summary>
        protected override void OnInitialize()
        {
            base.OnInitialize();

            SettingsView = new SettingsViewModel();

            Flyouts.Add(SettingsView);
        }

        /// <summary>
        /// Open the settings flyout.
        /// </summary>
        public void ToggleSettingsVisibility()
        {
            SettingsView.IsOpen = !SettingsView.IsOpen;
        }

        /// <summary>
        /// Open the about modal window.
        /// </summary>
        public void OpenAbout()
        {
            ServiceLocator.DialogService.ShowDialog<AboutViewModel>(new CustomDialogSettings()
            {
                Theme = MetroDialogColorScheme.Accented,
                FullWidth = false,
                Header = Resources.Strings.Screens.ShellResources.ApplicationTitle
            });
        }

        #endregion Methods
    }
}