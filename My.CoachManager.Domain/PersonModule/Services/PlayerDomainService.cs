using FluentValidation.Results;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.PersonModule.Aggregate;

namespace My.CoachManager.Domain.PersonModule.Services
{
    public class PlayerDomainService : IPlayerDomainService
    {
        #region Fields

        private readonly IRepository<Player> _playerRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PlayerDomainService"/>.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="playerRepository"></param>
        public PlayerDomainService(IRepository<Player> playerRepository)
        {
            _playerRepository = playerRepository;
        }

        #endregion Constructors

        #region methods

        /// <summary>
        /// Check if the category is used by others properties.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsUsed(int id)
        {
            return false;
        }

        /// <summary>
        /// Validates entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ValidationResult Validate(Player entity)
        {
            var validator = new PlayerValidator();
            return validator.Validate(entity);
        }

        #endregion methods
    }
}