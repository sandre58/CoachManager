using System;
using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Core;
using My.CoachManager.Application.Dtos.Admin;
using My.CoachManager.Application.Dtos.Mapping;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.Admin.Aggregate;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.Person.Aggregate;
using My.CoachManager.Domain.Person.Service;

namespace My.CoachManager.Application.Services.Admin
{
    /// <summary>
    /// Implementation of the IPlayerAppService class.
    /// </summary>
    public class PlayerAppService : AppService, IPlayerAppService
    {
        #region ---- Fields ----

        private readonly IPlayerRepository _playerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPlayerDomainService _playerDomainService;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerAppService"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="playerRepository"></param>
        /// <param name="countryRepository"></param>
        /// <param name="categoryRepository"></param>
        /// <param name="playerDomainService"></param>
        public PlayerAppService(ILogger logger, IPlayerRepository playerRepository, ICountryRepository countryRepository, ICategoryRepository categoryRepository, IPlayerDomainService playerDomainService)
            : base(logger)
        {
            _playerRepository = playerRepository;
            _countryRepository = countryRepository;
            _categoryRepository = categoryRepository;
            _playerDomainService = playerDomainService;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public PlayerDto CreateOrUpdate(PlayerDto dto)
        {
            var entity = dto.ToEntity<Player>();

            if (!_playerDomainService.IsValid(entity))
            {
                throw new BusinessException(ValidationMessageResources.NotValidMessage);
            }

            _playerRepository.AddOrModify(entity);

            _playerRepository.UnitOfWork.Commit();

            return entity.ToDto<PlayerDto>();
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void Remove(PlayerDto dto)
        {
            _playerRepository.Remove(dto.ToEntity<Player>());

            _playerRepository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public PlayerDto GetById(int id)
        {
            var item = _playerRepository.GetEntity(id, p => p.Contacts);
            return item.ToDto<PlayerDto>();
        }

        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PlayerDto> GetList()
        {
            return _playerRepository.GetAll(PersonSelectBuilder.SelectPlayerForList(), x => x.Category.Order, true).ToArray();
        }

        /// <summary>
        /// Load all countries.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CountryDto> GetCountries()
        {
            return _countryRepository.GetAll(PersonSelectBuilder.SelectCountries(), AdminOrderBuilder.OrderByLabel<Country>()).ToArray();
        }

        /// <summary>
        /// Load all countries.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryDto> GetCategories()
        {
            return _categoryRepository.GetAll(PersonSelectBuilder.SelectCategories(), AdminOrderBuilder.OrderByOrder<Category>()).ToArray();
        }

        /// <summary>
        /// Get category from birthdate.
        /// </summary>
        /// <returns></returns>
        public CategoryDto GetCategoryFromBirthdate(DateTime date)
        {
            return _categoryRepository.GetByFilter(x => x.Year >= date.Year, x => x.Year, true).ToArray().ToDtos<CategoryDto>().FirstOrDefault();
        }

        /// <summary>
        /// Load all cities.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CityDto> GetCities()
        {
            return _playerRepository.GetByFilter(PersonSelectBuilder.SelectCity(), x => !string.IsNullOrEmpty(x.City), x => x.City, true).Distinct().ToArray();
        }

        #endregion Methods
    }
}