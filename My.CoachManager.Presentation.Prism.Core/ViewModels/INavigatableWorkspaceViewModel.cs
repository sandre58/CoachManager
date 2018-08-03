using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public interface INavigatableWorkspaceViewModel : IWorkspaceViewModel, INavigationAware, IRegionMemberLifetime
    {
    }
}