using My.CoachManager.Presentation.Prism.Core.Interactivity;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.Wpf.ViewModels
{
    public interface IMessageViewModel : IDialogViewModel
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        MessageDialogType Type { get; set; }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        MessageDialogStyle Style { get; set; }
    }
}