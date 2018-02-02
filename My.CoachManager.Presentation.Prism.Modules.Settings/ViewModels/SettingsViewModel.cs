using My.CoachManager.Presentation.Prism.Core.Global;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Modules.Settings.ViewModels
{
    public class SettingsViewModel : ScreenViewModel, ISettingsViewModel
    {
        #region Fields

        private bool _isOpen;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets IsOpen Value.
        /// </summary>
        public bool IsOpen
        {
            get { return _isOpen; }
            set { SetProperty(ref _isOpen, value); }
        }

        #endregion Members

        #region Constructor

        /// <summary>
        /// Initializes an instance of <see cref="SkinSettingsViewModel"/>.
        /// </summary>
        public SettingsViewModel()
        {
            var toggleSettingsCommand = new DelegateCommand(() => IsOpen = !IsOpen);

            GlobalCommands.ToggleSettingsCommand.RegisterCommand(toggleSettingsCommand);
        }

        #endregion Constructor
    }
}