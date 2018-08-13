using System;
using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Services.AddressModule;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.Domain.AddressModule.Aggregate;
using My.CoachManager.Domain.AppModule.Services;
using My.CoachManager.Domain.CategoryModule.Aggregate;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.PersonModule.Aggregate;
using My.CoachManager.Domain.PersonModule.Services;

namespace My.CoachManager.Application.Services.PersonModule
{
    /// <summary>
    /// Implementation of the IPlayerAppService class.
    /// </summary>
    public class PlayerAppService : IPlayerAppService
    {
        #region ---- Fields ----

        private readonly IRepository<Player> _playerRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IAddressAppService _addressAppService;
        private readonly IPlayerDomainService _playerDomainService;
        private readonly ICrudDomainService<Player, PlayerDto> _crudDomainService;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerAppService"/> class.
        /// </summary>
        public PlayerAppService(IRepository<Player> playerRepository, IRepository<Category> categoryRepository,
            IAddressAppService addressAppService,
            IPlayerDomainService playerDomainService,
            ICrudDomainService<Player, PlayerDto> crudDomainService)
        {
            _playerRepository = playerRepository;
            _playerDomainService = playerDomainService;
            _categoryRepository = categoryRepository;
            _crudDomainService = crudDomainService;
            _addressAppService = addressAppService;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IList<PlayerDto> GetPlayers()
        {
            return _playerRepository.GetAll(PlayerSelectBuilder.SelectPlayerDetails(), x => x.LastName).ToList();
        }


        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        public PlayerDto GetPlayerById(int playerId)
        {
            var player = _playerRepository.GetEntity(playerId,
                x => x.Address,
                x => x.Contacts);

            return PlayerFactory.Get(player);
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public PlayerDto SavePlayer(PlayerDto dto)
        {
            if (dto.CrudStatus == CrudStatus.Updated)
            {
                // Create or update address
                if (!string.IsNullOrEmpty(dto.Address) || !string.IsNullOrEmpty(dto.PostalCode) || !string.IsNullOrEmpty(dto.City))
                {
                    var adressDto = AddressFactory.GetDto(dto.AddressId ?? 0, dto.Address, dto.PostalCode, dto.City, dto.AddressId.HasValue ? CrudStatus.Updated : CrudStatus.Created);
                    dto.AddressId = _addressAppService.SaveAddress(adressDto).Id;
                }

                // Remove address
                else if (dto.AddressId.HasValue && string.IsNullOrEmpty(dto.Address) && string.IsNullOrEmpty(dto.PostalCode) && string.IsNullOrEmpty(dto.City))
                {
                    _addressAppService.RemoveAddress(dto.AddressId.Value);
                    dto.AddressId = null;
                }
            }

            return _crudDomainService.Save(dto, PlayerFactory.CreateEntity, PlayerFactory.UpdateEntity, x => _playerDomainService.Validate(x));

            // var address = new AddressDto()
            // {
            //     Id = dto.AddressId ?? 0,
            //     Row1 = dto.Address,
            //     PostalCode = dto.PostalCode,
            //     City = dto.City
            // };





            // // Add player
            //// _playerRepository.AddOrModify(entity);

            // // Commit changes
            // _playerRepository.UnitOfWork.Commit();

            // // Remove address entity
            // if (!entity.AddressId.HasValue && address.Id != 0)
            // {
            //     _addressAppService.Remove(address);
            // }

            // return PlayerFactory.CreatePlayerDto(entity);

        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void RemovePlayer(PlayerDto dto)
        {
            if (_playerDomainService.IsUsed(dto.Id))
            {
                throw new IsUsedException(dto.FirstName + " " + dto.LastName);
            }

            if (dto.AddressId != null) _addressAppService.RemoveAddress(dto.AddressId.Value);
            _crudDomainService.Remove(dto);
        }

        /// <summary>
        /// Get category from birthdate.
        /// </summary>
        /// <returns></returns>
        public CategoryDto GetCategoryFromBirthdate(DateTime date)
        {
            return _categoryRepository.GetByFilter(x => x.Year >= date.Year, x => x.Year, true).ToArray().Select(CategoryFactory.Get).FirstOrDefault();
        }

        #endregion Methods
    }
}