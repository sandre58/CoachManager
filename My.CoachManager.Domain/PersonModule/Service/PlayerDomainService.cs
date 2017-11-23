using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.PersonModule.Aggregate;

namespace My.CoachManager.Domain.PersonModule.Service
{
    public class PlayerDomainService : DomainService, IPlayerDomainService
    {
        #region Fields

        private readonly IPlayerRepository _playerRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PlayerDomainService"/>.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="playerRepository"></param>
        public PlayerDomainService(ILogger logger, IPlayerRepository playerRepository)
            : base(logger)
        {
            _playerRepository = playerRepository;
        }

        #endregion Constructors

        #region methods

        /// <summary>
        /// Check if player is valide.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool IsValid(Player item)
        {
            if (string.IsNullOrEmpty(item.LastName) || string.IsNullOrEmpty(item.FirstName))
            {
                return false;
            }

            if (item.CategoryId <= 0)
            {
                return false;
            }

            return true;
        }

        #endregion methods
    }
}