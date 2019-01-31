using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using My.CoachManager.Presentation.Core;
using My.CoachManager.Presentation.Core.Commands;
using My.CoachManager.Presentation.Core.Manager;
using My.CoachManager.Presentation.Modules.Roster.Views;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;
using Prism.Commands;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace My.CoachManager.Presentation.Modules.Roster
{
    public class RosterModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;
        private readonly IRosterService _rosterService;

        public RosterModule(IRegionManager regionManager, IUnityContainer container, IRosterService rosterService)
        {
            _regionManager = regionManager;
            _container = container;
            _rosterService = rosterService;
        }

        public void Initialize()
        {
            PlayerCommands.NavigateToPlayerCommand.RegisterCommand(new DelegateCommand<int?>(x =>
            {
                if (x != null) NavigationManager.NavigateTo<RosterPlayerView>(x.Value);
            }));

            var squads = _rosterService.GetSquads(SettingsManager.GetRosterId());

            // Register the navigation view
            if(squads.Length == 1)
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, () => ServiceLocator.Current.GetInstance<RosterNavigationView>());
            else if(squads.Length > 1)
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, () => ServiceLocator.Current.GetInstance<SquadsNavigationView>());

            // Register the workspace view
            _container.RegisterTypeForNavigation<SquadView>();
            _container.RegisterTypeForNavigation<RosterPlayerView>();
        }
    }
}