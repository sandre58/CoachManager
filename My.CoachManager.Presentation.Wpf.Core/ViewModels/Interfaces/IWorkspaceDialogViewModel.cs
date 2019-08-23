namespace My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces
{
    public interface IWorkspaceDialogViewModel : IDialogViewModel
    {
        /// <summary>
        /// Get or set the mode.
        /// </summary>
        /// <remarks></remarks>
        ScreenMode Mode { get; }
    }
}