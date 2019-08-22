using My.CoachManager.Presentation.Wpf.Core.ViewModels.Base;
using My.CoachManager.Presentation.Wpf.Modules.Shared.Events;

namespace My.CoachManager.Presentation.Wpf.Modules.Settings.ViewModels
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
        protected override void Initialize()
        {
            base.Initialize();

            EventAggregator.GetEvent<ToggleSettingsRequestEvent>().Subscribe(() => IsOpen = !IsOpen);
        }

        #endregion Initialisation
    }
}