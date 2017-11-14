using My.CoachManager.Presentation.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Enums;

namespace My.CoachManager.Presentation.ViewModels.Dialogs
{
    public class MessagePopupViewModel : ScreenViewModel
    {
        #region Fields

        private string _message;

        private MessagePopupStyle _style;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                if (_message == value) return;
                _message = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        public MessagePopupStyle Style
        {
            get { return _style; }
            set
            {
                if (_style == value) return;
                _style = value;
                NotifyOfPropertyChange();
            }
        }

        #endregion Properties
    }
}