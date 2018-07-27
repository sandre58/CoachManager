using System;
using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.ServiceAgent.CategoryServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PersonServiceReference;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    public class SquadItemViewModel : ItemViewModel<SquadViewModel>, ISquadItemViewModel
    {
        #region Fields

        private readonly IRosterService _rosterService;
        private readonly IPersonService _personService;
        private readonly ICategoryService _categoryService;
        private PlayersListViewModel _players;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SquadItemViewModel"/>.
        /// </summary>
        public SquadItemViewModel(IRosterService rosterService, IPersonService personService, ICategoryService categoryService)
        {
            _rosterService = rosterService;
            _personService = personService;
            _categoryService = categoryService;
        }

        /// <summary>
        /// Constructor used by Design Mode.
        /// </summary>
        public SquadItemViewModel()
        {
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets or sets all players.
        /// </summary>
        public PlayersListViewModel Players
        {
            get { return _players; }
            private set { SetProperty(ref _players, value); }
        }

        #endregion Members

        #region Methods

        protected override Type GetEditViewType()
        {
            return null;
        }

        #region Data

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override SquadViewModel LoadItemCore(int id)
        {
            var squad = new SquadViewModel();

            var categories = new List<CategoryViewModel>();
            var countries = new List<CountryViewModel>();

            //if (_categoryService != null)
            //{
            //    categories = _categoryService.GetLabels().ToViewModels<CategoryViewModel>().ToList();
            //}

            //if (_categoryService != null)
            //{
            //    countries = _personService.GetCountries().ToViewModels<CountryViewModel>().ToList();
            //}

            //if (_rosterService != null)
            //{
            //    squad = _rosterService.GetSquad(id).ToViewModel<SquadViewModel>();
            //    Players = new PlayersListViewModel(squad.Players, categories, countries);
            //}
            return squad;
        }

        /// <summary>
        /// Call after load data.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            base.OnLoadDataCompleted();
            Title = Item.Name;
        }

        #endregion Data

        #endregion Methods
    }
}