using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Modules.Shared.ViewModels;
using My.CoachManager.Presentation.ServiceAgent.AddressServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PersonServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PositionServiceReference;

namespace My.CoachManager.Presentation.Modules.Administration.ViewModels
{
    public class PlayerEditViewModel : PlayerEditViewModelBase<PlayerModel>
    {
        #region Fields

        private readonly IPersonService _personService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PlayerEditViewModel"/>.
        /// </summary>
        public PlayerEditViewModel(IPersonService personService, IAddressService addressService, IPositionService positionService) : base(personService, addressService, positionService)
        {
            _personService = personService;
        }

        #endregion Constructors

        #region Methods

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override PlayerModel LoadItemCore(int id)
        {
            return PlayerFactory.Get(_personService.GetPlayerById(id));
        }

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override int SaveItemCore()
        {
            return _personService.SavePlayer(PlayerFactory.Get(Item, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
        }

        #endregion Data

        #endregion Methods
    }
}