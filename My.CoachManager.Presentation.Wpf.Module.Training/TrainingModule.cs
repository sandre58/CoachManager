using CommonServiceLocator;
using My.CoachManager.Presentation.Wpf.Core;
using My.CoachManager.Presentation.Wpf.Core.Manager;
using My.CoachManager.Presentation.Wpf.Modules.Shared;
using My.CoachManager.Presentation.Wpf.Modules.Shared.Events;
using My.CoachManager.Presentation.Wpf.Modules.Training.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.Training.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Wpf.Modules.Training
{
    public class TrainingModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initialise a new instance of <see cref="TrainingModule"/>.
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="eventAggregator"></param>
        public TrainingModule(IRegionManager regionManager, IEventAggregator eventAggregator)
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
            containerRegistry.RegisterForNavigation<TrainingsListViewModel>();
            containerRegistry.RegisterForNavigation<TrainingViewModel>();

            containerRegistry.Register<TrainingAttendancesEditViewModel>();
            containerRegistry.Register<TrainingEditViewModel>();
            containerRegistry.Register<TrainingsAddViewModel>();
        }

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Register the navigation view
            _regionManager.RegisterViewWithRegion(RegionNames.TopNavigationRegion, () => ServiceLocator.Current.GetInstance<TrainingNavigationView>());
            // Events.
            _eventAggregator.GetEvent<EditTrainingAttendancesRequestEvent>().Subscribe(x => DialogManager.ShowEditDialog<TrainingAttendancesEditViewModel>(x.Id, x.Callback, x.Parameters));
        }
    }
}