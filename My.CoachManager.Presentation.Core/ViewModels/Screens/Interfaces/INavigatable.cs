using Caliburn.Micro;

namespace My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces
{
    /// <summary>
    /// A view model representing a workspace.
    /// </summary>
    public interface INavigatable : IWorkspaceViewModel
    {
        /// <summary>
        /// Get or set a value indicates if we can go back.
        /// </summary>
        bool CanGoBack { get; set; }

        /// <summary>
        /// Get or set a value indicates if we can go back.
        /// </summary>
        bool CanNavigate { get; set; }
    }
}