namespace My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces
{
    /// <summary>
    /// A view model representing a workspace.
    /// </summary>
    public interface IFlyoutViewModel : IWorkspaceViewModel
    {
        bool IsOpen { get; set; }
    }
}