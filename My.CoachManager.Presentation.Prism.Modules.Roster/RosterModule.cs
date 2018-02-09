using System.Linq;
using Microsoft.Practices.Unity;
using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels;
using My.CoachManager.Presentation.Prism.Modules.Roster.Views;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Modules.Roster
{
    public class RosterModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IRosterService _rosterService;

        /// <summary>
        /// Initialise a new instance of <see cref="RosterModule"/>.
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="rosterService"></param>
        public RosterModule(IRegionManager regionManager, IRosterService rosterService)
        {
            _regionManager = regionManager;
            _rosterService = rosterService;
        }

        /// <summary>
        /// Initializes the module.
        /// </summary>
        public async void Initialize()
        {
            // Register ViewModels
            Locator.RegisterType<IPlayerItemViewModel, PlayerItemViewModel>(new TransientLifetimeManager());
            Locator.RegisterType<ISquadItemViewModel, SquadItemViewModel>(new TransientLifetimeManager());
            Locator.RegisterType<IPlayerEditViewModel, PlayerEditViewModel>();

            // Register Views (for navigation)
            Locator.RegisterTypeForNavigation<SquadItemView>();
            Locator.RegisterTypeForNavigation<PlayerItemView>();

            // Gets squads.
            var squads = await _rosterService.GetSquadsAsync(1);
            var squadsViewModels = squads.ToViewModels<SquadViewModel>().ToList();

            // Register the navigation view
            if (squadsViewModels.Count > 1)
            {
                Locator.RegisterInstance(new SquadsNavigationViewModel(squadsViewModels));

                _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion,
                    Locator.GetInstance<SquadsNavigationView>);
            }
            else
            {
                _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion,
                    Locator.GetInstance<RosterNavigationView>);
            }
        }
    }
}