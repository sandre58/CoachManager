using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.PersonModule.Service
{
    public interface IPlayerDomainService : IDomainService
    {
        /// <summary>
        /// Check if player is valide.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool IsValid(Player item);
    }
}