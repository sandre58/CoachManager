using System.Linq;
using Microsoft.Practices.ServiceLocation;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.Prism.Modules.Core.Views;
using My.CoachManager.Presentation.Prism.Modules.Roster.Resources;
using My.CoachManager.Presentation.Prism.Modules.Roster.Views;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    public class RosterViewModel : ListViewModel<RosterPlayerModel, RosterPlayerEditView, RosterPlayerEditView>
    {
        #region Fields

        private readonly IRosterService _rosterService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="RosterViewModel"/>.
        /// </summary>
        public RosterViewModel(IRosterService rosterService)
        {
            _rosterService = rosterService;
        }

        #endregion Constructors

        #region Methods

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            Title = RosterResources.RosterTitle;
        }

        #endregion Initialization

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(RosterPlayerModel item)
        {
            // _personService.RemovePlayer(PlayerFactory.Get(item, CrudStatus.Deleted));
        }

        /// <inheritdoc />
        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            var result = _rosterService.GetPlayers(1);

            Items = result.Select(RosterFactory.Get).ToObservableCollection();
        }

        protected override void InitializeDataCore()
        {
            base.InitializeDataCore();

            //var categories = _categoryService.GetCategories().Select(CategoryFactory.Get);
            //var countries = _personService.GetCountries().Select(CountryFactory.Get);
            //Filters = new PlayersListFiltersViewModel(categories, countries);
            //Parameters = new PlayersListParametersViewModel();
        }

        #endregion Data

        #region Add

        protected override void Add()
        {
            var view = ServiceLocator.Current.GetInstance<SelectPlayersView>();

            DialogManager.ShowWorkspaceDialog(view, dialog =>
            {
                OnAddCompleted(dialog.Result);
            });
        }

        #endregion

        #endregion Methods

    }
}