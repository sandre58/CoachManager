using Microsoft.EntityFrameworkCore;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Dtos.Parameters;
using My.CoachManager.Application.Dtos.Results;
using My.CoachManager.Application.Services.AddressModule;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.Domain.AddressModule.Aggregate;
using My.CoachManager.Domain.AppModule.Services;
using My.CoachManager.Domain.CategoryModule.Aggregate;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.PersonModule.Aggregate;
using My.CoachManager.Domain.PersonModule.Services;
using System;
using System.ComponentModel;
using System.Linq;

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
        public ListResultDto<PlayerDto> GetPlayers(PlayersListParametersDto parameters)
        {
            var result = _playerRepository.GetBySpecAndCount(PlayerSelectBuilder.SelectPlayerDetails(),
                PlayerSpecification.IsMatch(parameters),
                PlayerOrderBuilder.OrderByProperty(parameters.SortProperty, parameters.SortDirection == ListSortDirection.Descending),
                parameters.Page,
                parameters.Count,
                query =>
                {
                    return query
                        .Include(x => x.Address)
                        .Include(x => x.Contacts);
                });

            var count = _playerRepository.Query.Count();

            return new ListResultDto<PlayerDto>
            {
                Items = result.Item1.ToList(),
                Count = result.Item2,
                AllItemsCount = count
            };
        }

        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        public PlayerDto GetPlayerById(int playerId)
        {
            var player = _playerRepository.Query
                .Include(x => x.Address)
                .Include(x => x.Contacts)
                .Include(x => x.Positions)
                    .ThenInclude(x => x.Position)
                .SingleOrDefault(x => x.Id == playerId);

            return PlayerFactory.Get(player);
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public int SavePlayer(PlayerDto dto)
        {
            if (dto.CrudStatus == CrudStatus.Updated)
            {
                // Create or update address
                if (!string.IsNullOrEmpty(dto.Address) || !string.IsNullOrEmpty(dto.PostalCode) || !string.IsNullOrEmpty(dto.City))
                {
                    var adressDto = AddressFactory.GetDto(dto.AddressId ?? 0, dto.Address, dto.PostalCode, dto.City, dto.AddressId.HasValue ? CrudStatus.Updated : CrudStatus.Created);
                    dto.AddressId = _addressAppService.SaveAddress(adressDto);
                }

                // Remove address
                else if (dto.AddressId.HasValue && string.IsNullOrEmpty(dto.Address) && string.IsNullOrEmpty(dto.PostalCode) && string.IsNullOrEmpty(dto.City))
                {
                    _addressAppService.RemoveAddress(dto.AddressId.Value);
                    dto.AddressId = null;
                }
            }

            return _crudDomainService.Save(dto, PlayerFactory.CreateEntity, PlayerFactory.UpdateEntity, x => _playerDomainService.Validate(x),
                query => { return query.Include(x => x.Positions).Include(x => x.Contacts); });
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void RemovePlayer(int id)
        {
            if (_playerDomainService.IsUsed(id))
            {
                throw new IsUsedException(GetName(id));
            }

            var addressId = GetAddressId(id);
            if (addressId != null) _addressAppService.RemoveAddress(addressId.Value);
            _crudDomainService.Remove(id);
        }

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        private string GetName(int id)
        {
            return _playerRepository.Query.Where(x => x.Id == id).Select(x => x.FirstName + " " + x.LastName).FirstOrDefault();
        }

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        private int? GetAddressId(int id)
        {
            return _playerRepository.Query.Where(x => x.Id == id).Select(x => x.AddressId).FirstOrDefault();
        }

        /// <summary>
        /// Get category from birthdate.
        /// </summary>
        /// <returns></returns>
        public CategoryDto GetCategoryFromDate(DateTime fromDate, DateTime toDate)
        {
            var diffYear = toDate.Year - fromDate.Year;
            return _categoryRepository.GetByFilter(x => x.Age <= diffYear, x => x.Age, true).ToArray().Select(CategoryFactory.Get).FirstOrDefault();
        }

        #endregion Methods
    }
}