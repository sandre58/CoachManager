using My.CoachManager.Presentation.Prism.Core.Dialog;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public interface IDialogViewModel : IScreenViewModel, ICloseable
    {
        /// <summary>
        /// Gets or sets the dialog result.
        /// </summary>
        DialogResult DialogResult { get; set; }
    }
}