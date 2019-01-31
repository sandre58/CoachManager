using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Modules.Administration.Resources;
using My.CoachManager.Presentation.ServiceAgent.CategoryServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PersonServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PositionServiceReference;
using PlayerEditView = My.CoachManager.Presentation.Modules.Shared.Views.PlayerEditView;

namespace My.CoachManager.Presentation.Modules.Administration.ViewModels
{
    public class PlayersListViewModel : ListViewModel<PlayerModel, PlayerEditView, PlayerEditView>
    {
        #region Fields

        private readonly IPersonService _personService;
        private readonly ICategoryService _categoryService;
        private readonly IPositionService _positionService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="CategoriesListViewModel"/>.
        /// </summary>
        public PlayersListViewModel(IPersonService personService, ICategoryService categoryService, IPositionService positionService)
        {
            _personService = personService;
            _categoryService = categoryService;
            _positionService = positionService;
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

            Title = PlayerResources.PlayersTitle;
        }

        #endregion Initialization

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(PlayerModel item) => _personService.RemovePlayer(PlayerFactory.Get(item, CrudStatus.Deleted));

        /// <inheritdoc />
        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            var result = _personService.GetPlayers();

            Items = result.Select(PlayerFactory.Get).ToItemsObservableCollection();
        }

        protected override void InitializeDataCore()
        {
            base.InitializeDataCore();

            var categories = _categoryService.GetCategories().Select(CategoryFactory.Get);
            var countries = _personService.GetCountries().Select(CountryFactory.Get);
            var positions = _positionService.GetPositions().Select(PositionFactory.Get);
            Filters = new PlayersListFiltersViewModel(categories, positions, countries);
            ListParameters = new PlayersListParametersViewModel();
        }

        #endregion Data

        #endregion Methods
    }
}