using My.CoachManager.Presentation.Prism.Core.Interactivity;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public interface IDialogViewModel : IScreenViewModel, ICloseable
    {
        /// <summary>
        /// Gets or sets the dialog result.
        /// </summary>
        DialogResult DialogResult { get; set; }
    }
}