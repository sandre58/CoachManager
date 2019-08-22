using My.CoachManager.Presentation.Wpf.Core.Dialog;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Base;

namespace My.CoachManager.Presentation.Wpf.ViewModels.Dialogs
{
    /// <summary>
    /// ViewModel for the login window.
    /// </summary>
    public class MessageViewModel : DialogViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public MessageDialogButtons Buttons { get; set; }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        public MessageDialogType Type { get; set; }

        #endregion Properties

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Load data.
        /// </summary>
        protected override void LoadDataCore()
        {
        }

        #endregion Data
    }
}