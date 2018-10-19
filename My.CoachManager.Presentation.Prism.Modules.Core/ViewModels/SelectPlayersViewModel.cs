using System.Linq;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.ServiceAgent.CategoryServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PersonServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Core.ViewModels
{
    public class SelectPlayersViewModel : SelectItemsViewModel<PlayerModel>
    {
        #region Fields

        private readonly IPersonService _personService;
        private readonly ICategoryService _categoryService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SelectPlayersViewModel"/>.
        /// </summary>
        public SelectPlayersViewModel(IPersonService personService, ICategoryService categoryService)
        {
            _personService = personService;
            _categoryService = categoryService;
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

            //Title = PlayerResources.PlayersTitle;
        }

        #endregion Initialization

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            var result = _personService.GetPlayers();

            Items = result.Select(PlayerFactory.Get).ToObservableCollection();
        }

        protected override void InitializeDataCore()
        {
            base.InitializeDataCore();

            var categories = _categoryService.GetCategories().Select(CategoryFactory.Get);
            var countries = _personService.GetCountries().Select(CountryFactory.Get);
            Filters = new SelectPlayersFiltersViewModel(categories, countries);
        }

        #endregion Data

        #endregion Methods
    }
}