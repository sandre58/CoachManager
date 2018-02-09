using System;
using System.Linq;
using My.CoachManager.Application.Core;
using My.CoachManager.Application.Dtos.Categories;
using My.CoachManager.Application.Dtos.Mapping;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.CategoryModule.Aggregate;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.PersonModule.Aggregate;
using My.CoachManager.Domain.PersonModule.Service;

namespace My.CoachManager.Application.Services.Persons
{
    /// <summary>
    /// Implementation of the IPlayerAppService class.
    /// </summary>
    public class PlayerAppService : AppService, IPlayerAppService
    {
        #region ---- Fields ----

        private readonly IPlayerRepository _playerRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPlayerDomainService _playerDomainService;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerAppService"/> class.
        /// </summary>
        public PlayerAppService(ILogger logger, IPlayerRepository playerRepository, ICategoryRepository categoryRepository, IPlayerDomainService playerDomainService)
            : base(logger)
        {
            _playerRepository = playerRepository;
            _playerDomainService = playerDomainService;
            _categoryRepository = categoryRepository;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        public PlayerDetailDto GetPlayer(int playerId)
        {
            var player = _playerRepository.GetEntity(playerId, x => x.Category,
                x => x.Address,
                x => x.Country,
                x => x.Contacts);

            return PlayerFactory.CreatePlayerDetailDto(player);
        }

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
        /// Get category from birthdate.
        /// </summary>
        /// <returns></returns>
        public CategoryDto GetCategoryFromBirthdate(DateTime date)
        {
            return _categoryRepository.GetByFilter(x => x.Year >= date.Year, x => x.Year, true).ToArray().ToDtos<CategoryDto>().FirstOrDefault();
        }

        #endregion Methods
    }
}