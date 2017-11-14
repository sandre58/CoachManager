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
        private readonly IEmailRepository _emailRepository;
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
        /// <param name="emailRepository"></param>
        /// <param name="playerDomainService"></param>
        public PlayerAppService(ILogger logger, IPlayerRepository playerRepository, ICountryRepository countryRepository, ICategoryRepository categoryRepository, IEmailRepository emailRepository, IPlayerDomainService playerDomainService)
            : base(logger)
        {
            _playerRepository = playerRepository;
            _countryRepository = countryRepository;
            _categoryRepository = categoryRepository;
            _emailRepository = emailRepository;
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

            //1- Get fresh data from database
            //var player = _playerRepository.GetEntity(entity.Id, x => x.Emails);

            //var existingEmails = player.Emails;

            //var updatedEmails = entity.Emails;

            ////2- Find newly added teachers by updatedTeachers (teacher came from client sided) - existingTeacher = newly added teacher
            //var addedEmails = updatedEmails.Where(x => x.Id == 0).ToList();

            ////3- Find deleted teachers by existing teachers - updatedTeachers = deleted teachers
            //var deletedEmails = existingEmails.Where(x => updatedEmails.Any(y => y.Id != x.Id)).ToList();

            ////4- Find modified teachers by updatedTeachers - addedTeachers = modified teachers
            //var modifiedEmails = updatedEmails.Where(x => x.Id != 0).ToList();

            ////5- Mark all added teachers entity state to Added
            //_emailRepository.AddRange(addedEmails);

            ////6- Mark all deleted teacher entity state to Deleted
            //deletedEmails.ForEach(x => _emailRepository.Remove(x));

            ////7- Apply modified teachers current property values to existing property values
            //modifiedEmails.ForEach(x => _emailRepository.Modify(x));

            //_playerRepository.AddOrModify(entity);

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
            var item = _playerRepository.GetEntity(id, p => p.Emails, p => p.Phones);
            return item.ToDto<PlayerDto>();
        }

        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PlayerDto> GetList()
        {
            return _playerRepository.GetAll(PersonSelectBuilder.SelectPlayerForList()).ToArray();
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