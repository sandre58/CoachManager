using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.ServiceAgent.PersonServiceReference;
using System.Linq;

namespace My.CoachManager.Presentation.Modules.Shared.ViewModels
{
    public class SelectPlayersViewModel : SelectItemsViewModel<PlayerModel>
    {
        #region Fields

        private readonly IPersonService _personService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SelectPlayersViewModel"/>.
        /// </summary>
        public SelectPlayersViewModel(IPersonService personService)
        {
            _personService = personService;
        }

        #endregion Constructors

        #region Methods

        #region Data

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override ObservableItemsCollection<PlayerModel> LoadItems()
        {
            var result = _personService.GetPlayers();

            return result.Select(PlayerFactory.Get).ToItemsObservableCollection();
        }

        /// <summary>
        /// Initialise data.
        /// </summary>
        protected override void InitializeDataCore()
        {
            base.InitializeDataCore();

            var countries = _personService.GetCountries().Select(CountryFactory.Get);
            Filters = new SelectPlayersFiltersViewModel(countries);
        }

        #endregion Data

        #endregion Methods
    }
}