namespace My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces
{
    public interface IWorkspaceViewModel : IScreenViewModel
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets show header.
        /// </summary>
        bool ShowHeader { get; set; }
    }
}