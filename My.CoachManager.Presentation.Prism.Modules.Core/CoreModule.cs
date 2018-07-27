using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Core.Views;
using My.CoachManager.Presentation.Prism.Modules.Core.Views;
using Prism.Modularity;

namespace My.CoachManager.Presentation.Prism.Modules.Core
{
    public class CoreModule : IModule
    {
        /// <summary>
        /// Initialise a new instance of <see cref="CoreModule"/>.
        /// </summary>
        public CoreModule()
        {
        }

        /// <summary>
        /// Initializes the module.
        /// </summary>
        public void Initialize()
        {
            // Register ViewModels
            //Locator.RegisterType<IFiltersView, FiltersView>();
        }
    }
}