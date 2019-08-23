using My.CoachManager.Presentation.Wpf.Core.ViewModels.Base;
using My.CoachManager.Presentation.Wpf.Messages;

namespace My.CoachManager.Presentation.Wpf.ViewModels.Settings
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

            Messenger.Register<ToggleSettingsRequestMessage>(this, e =>
            {
                IsOpen = !IsOpen;
            });
        }

        #endregion Initialisation
    }
}