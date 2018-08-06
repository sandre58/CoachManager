﻿using System;
using System.Linq;
using My.CoachManager.Application.Dtos.Category;
using My.CoachManager.Application.Dtos.Person;
using My.CoachManager.Application.Services.Addresses;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.CategoryModule.Aggregate;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.PersonModule.Aggregate;
using My.CoachManager.Domain.PersonModule.Services;

namespace My.CoachManager.Application.Services.Persons
{
    /// <summary>
    /// Implementation of the IPlayerAppService class.
    /// </summary>
    public class PlayerAppService : IPlayerAppService
    {
        #region ---- Fields ----

        private readonly IRepository<Player> _playerRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IPlayerDomainService _playerDomainService;
        private readonly IAddressAppService _addressAppService;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerAppService"/> class.
        /// </summary>
        public PlayerAppService(ILogger logger, IRepository<Player> playerRepository, IRepository<Category> categoryRepository, IPlayerDomainService playerDomainService, IAddressAppService addressAppService)
        {
            _playerRepository = playerRepository;
            _playerDomainService = playerDomainService;
            _categoryRepository = categoryRepository;
            _addressAppService = addressAppService;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        public PlayerDetailsDto GetPlayerDetails(int playerId)
        {
            var player = _playerRepository.GetEntity(playerId, x => x.Category,
                x => x.Address,
                x => x.Country,
                x => x.Contacts);

            return PlayerFactory.CreatePlayerDetailsDto(player);
        }

        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        public PlayerDto GetPlayer(int playerId)
        {
            var player = _playerRepository.GetEntity(playerId,
                x => x.Address,
                x => x.Contacts);

            return PlayerFactory.CreatePlayerDto(player);
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public PlayerDto CreateOrUpdate(PlayerDto dto)
        {
            // var entity = dto.ToEntity<Player>();

            // if (!_playerDomainService.IsValid(entity))
            // {
            //     throw new BusinessException(ValidationMessageResources.NotValidMessage);
            // }

            // var address = new AddressDto()
            // {
            //     Id = dto.AddressId ?? 0,
            //     Row1 = dto.Address,
            //     PostalCode = dto.PostalCode,
            //     City = dto.City
            // };

            // // Create or update address
            // if (!string.IsNullOrEmpty(dto.Address) || !string.IsNullOrEmpty(dto.PostalCode) || !string.IsNullOrEmpty(dto.City))
            // {
            //     entity.AddressId = _addressAppService.CreateOrUpdate(address).Id;
            // }

            // // Remove address
            // else if (dto.AddressId.HasValue && string.IsNullOrEmpty(dto.Address) && string.IsNullOrEmpty(dto.PostalCode) && string.IsNullOrEmpty(dto.City))
            // {
            //     entity.AddressId = null;
            // }

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
            return null;
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void Remove(PlayerDto dto)
        {
            //_playerRepository.Remove(dto.ToEntity<Player>());

            //_playerRepository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Get category from birthdate.
        /// </summary>
        /// <returns></returns>
        public CategoryDto GetCategoryFromBirthdate(DateTime date)
        {
            return null;
            // return _categoryRepository.GetByFilter(x => x.Year >= date.Year, x => x.Year, true).ToArray().ToDtos<CategoryDto>().FirstOrDefault();
        }

        #endregion Methods
    }
}