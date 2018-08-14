using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces
{
    public interface INavigatableWorkspaceViewModel : IWorkspaceViewModel, INavigationAware, IRegionMemberLifetime
    {
    }
}