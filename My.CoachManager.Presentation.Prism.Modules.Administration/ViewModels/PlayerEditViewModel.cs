using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.Prism.Modules.Core.ViewModels;
using My.CoachManager.Presentation.ServiceAgent.AddressServiceReference;
using My.CoachManager.Presentation.ServiceAgent.CategoryServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PersonServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PositionServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public class PlayerEditViewModel : PlayerEditViewModel<PlayerModel>
    {

        #region Fields

        private readonly IPersonService _personService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PlayerEditViewModel"/>.
        /// </summary>
        public PlayerEditViewModel(IPersonService personService, ICategoryService categoryService, IAddressService addressService, IPositionService positionService) : base(personService, categoryService, addressService, positionService)
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
        protected override bool SaveItemCore()
        {
            return _personService.SavePlayer(PlayerFactory.Get(Item, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated)) > 0;
        }

        #endregion Data

        #endregion Methods

    }
}