using My.CoachManager.Presentation.Core.Events;
using My.CoachManager.Presentation.Core.ViewModels;

namespace My.CoachManager.Presentation.Modules.Settings.ViewModels
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

            EventAggregator.GetEvent<ToggleSettingsRequestEvent>().Subscribe(() => IsOpen = !IsOpen);
        }

        #endregion Initialisation
    }
}