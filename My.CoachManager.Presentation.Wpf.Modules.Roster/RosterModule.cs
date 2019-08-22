using CommonServiceLocator;
using My.CoachManager.Presentation.Wpf.Core;
using My.CoachManager.Presentation.Wpf.Core.Manager;
using My.CoachManager.Presentation.Wpf.Modules.Roster.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.Roster.Views;
using My.CoachManager.Presentation.Wpf.Modules.Shared;
using My.CoachManager.Presentation.Wpf.Modules.Shared.Events;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Wpf.Modules.Roster
{
    public class RosterModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initialise a new instance of <see cref="RosterModule"/>.
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="eventAggregator"></param>
        public RosterModule(IRegionManager regionManager, IEventAggregator eventAggregator)
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
            containerRegistry.RegisterForNavigation<SquadViewModel>();
            containerRegistry.RegisterForNavigation<RosterPlayerViewModel>();

            containerRegistry.Register<InjuryEditViewModel>();
            containerRegistry.Register<SquadEditViewModel>();
            containerRegistry.Register<RosterPlayerEditViewModel>();
            containerRegistry.Register<SelectRostersViewModel>();
            containerRegistry.Register<SelectSquadsViewModel>();
        }

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void OnInitialized(IContainerProvider containerProvider)
        {
            if (AppManager.Roster != null)
            {
                // Register the navigation view
                if (AppManager.Roster.Squads.Count == 1)
                    _regionManager.RegisterViewWithRegion(RegionNames.TopNavigationRegion,
                        () => ServiceLocator.Current.GetInstance<RosterNavigationView>());
                else if (AppManager.Roster.Squads.Count > 1)
                    _regionManager.RegisterViewWithRegion(RegionNames.TopNavigationRegion,
                        () => ServiceLocator.Current.GetInstance<SquadsNavigationView>());

                // Events.
                _eventAggregator.GetEvent<EditRosterPlayerRequestEvent>().Subscribe(x => DialogManager.ShowEditDialog<RosterPlayerEditViewModel>(x.Id, x.Callback, x.Parameters));
                _eventAggregator.GetEvent<EditSquadRequestEvent>().Subscribe(x => DialogManager.ShowEditDialog<SquadEditViewModel>(x.Id, x.Callback, x.Parameters));
                _eventAggregator.GetEvent<EditInjuryRequestEvent>().Subscribe(x => DialogManager.ShowEditDialog<InjuryEditViewModel>(x.Id, x.Callback, x.Parameters));
                _eventAggregator.GetEvent<SelectSquadsRequestEvent>().Subscribe(x =>
                {
                    var view = ServiceLocator.Current.GetInstance<SelectSquadsViewModel>();

                        view.RosterId = x.RosterId;

                        DialogManager.ShowSelectItemsDialog(view, x.Callback, x.SelectionMode, x.NotSelectetableItems, x.Parameters);
                });
                _eventAggregator.GetEvent<SelectRostersRequestEvent>().Subscribe(x => DialogManager.ShowSelectItemsDialog<SelectRostersViewModel>(x.Callback, x.SelectionMode, x.NotSelectetableItems, x.Parameters));
            }
        }
    }
}