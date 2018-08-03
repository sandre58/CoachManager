using My.CoachManager.Presentation.Prism.Core.Dialog;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public interface IDialogViewModel : ICloseable
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