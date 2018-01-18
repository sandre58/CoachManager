using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;

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