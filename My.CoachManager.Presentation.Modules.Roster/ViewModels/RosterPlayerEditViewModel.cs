using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Modules.Shared.ViewModels;
using My.CoachManager.Presentation.ServiceAgent.AddressServiceReference;
using My.CoachManager.Presentation.ServiceAgent.CategoryServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PersonServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PositionServiceReference;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;

namespace My.CoachManager.Presentation.Modules.Roster.ViewModels
{
    public class RosterPlayerEditViewModel : PlayerEditViewModel<RosterPlayerModel>
    {
        #region Fields

        private readonly IRosterService _rosterService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SquadViewModel"/>.
        /// </summary>
        public RosterPlayerEditViewModel(IRosterService rosterService, ICategoryService categoryService, IAddressService addressService, IPositionService positionService, IPersonService personService)
            : base(personService, categoryService, addressService, positionService)
        {
            _rosterService = rosterService;
        }

        #endregion Constructors

        #region Methods

        #region Data

        protected override int SaveItemCore()
        {
            return _rosterService.UpdatePlayer(RosterFactory.Get(Item, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
        }

        protected override RosterPlayerModel LoadItemCore(int id)
        {
            return RosterFactory.Get(_rosterService.GetRosterPlayerById(id));
        }

        #endregion Data

        #endregion Methods
    }
}