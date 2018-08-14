using My.CoachManager.Presentation.Prism.Core.Dialog;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces
{
    public interface IDialogViewModel : ICloseable, IScreenViewModel
    {
        /// <summary>
        /// Gets or sets title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the dialog result.
        /// </summary>
        DialogResult DialogResult { get; set; }
    }
}