using System.Windows.Input;
using My.CoachManager.Presentation.Prism.Core.Commands;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Modules.Common.ViewModels
{
    public class SettingsViewModel : ScreenViewModel
    {
        #region Members

        /// <summary>
        /// Gets or sets IsOpen Value.
        /// </summary>
        public bool IsOpen { get; set; }

        #endregion Members

        #region Initialisation

        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all commands.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            var toggleSettingsCommand = new DelegateCommand(() => IsOpen = !IsOpen);

            GlobalCommands.ToggleSettingsCommand.RegisterCommand(toggleSettingsCommand);
            KeyboardManager.RegisterGlobalShortcut(new KeyBinding(toggleSettingsCommand, Key.F4, ModifierKeys.None));
        }

        #endregion Initialisation
    }
}