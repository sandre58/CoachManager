using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.PersonModule.Services
{
    public interface IPlayerDomainService
    {
        /// <summary>
        /// Check if player is valide.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool IsValid(Player item);
    }
}