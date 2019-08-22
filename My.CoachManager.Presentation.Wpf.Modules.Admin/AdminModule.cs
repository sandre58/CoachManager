using CommonServiceLocator;
using My.CoachManager.Presentation.Wpf.Core;
using My.CoachManager.Presentation.Wpf.Core.Manager;
using My.CoachManager.Presentation.Wpf.Modules.Admin.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.Admin.Views;
using My.CoachManager.Presentation.Wpf.Modules.Shared.Events;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Wpf.Modules.Admin
{
    public class AdminModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initialise a new instance of <see cref="AdminModule"/>.
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="eventAggregator"></param>
        public AdminModule(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
        }

        /// <inheritdoc />
        /// <summary>
        /// Used to register types with the container that will be used by your application.
        /// </summary>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CategoriesListViewModel>();
            containerRegistry.RegisterForNavigation<SeasonsListViewModel>();
            containerRegistry.RegisterForNavigation<RostersListViewModel>();
            containerRegistry.RegisterForNavigation<PlayersListViewModel>();

            containerRegistry.Register<CategoryEditViewModel>();
            containerRegistry.Register<SeasonEditViewModel>();
            containerRegistry.Register<RosterEditViewModel>();
            containerRegistry.Register<PlayerEditViewModel>();
            containerRegistry.Register<SelectPlayersViewModel>();
        }

        /// <inheritdoc />
        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Register the navigation view
            _regionManager.RegisterViewWithRegion(RegionNames.BottomNavigationRegion, () => ServiceLocator.Current.GetInstance<AdminNavigationView>());

            // Events.
            _eventAggregator.GetEvent<EditPlayerRequestEvent>().Subscribe(x => DialogManager.ShowEditDialog<PlayerEditViewModel>(x.Id, x.Callback, x.Parameters));
            _eventAggregator.GetEvent<SelectPlayersRequestEvent>().Subscribe(x => DialogManager.ShowSelectItemsDialog<SelectPlayersViewModel>(x.Callback, x.SelectionMode, x.NotSelectetableItems, x.Parameters));
        }
    }
}