using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public interface INavigatableWorkspaceViewModel : IWorkspaceViewModel, INavigationAware, IRegionMemberLifetime
    {
    }
}