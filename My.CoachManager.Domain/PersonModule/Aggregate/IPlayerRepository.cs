using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.PersonModule.Aggregate
{
    /// <summary>
    /// Interface used for representing a IPlayerGenericRepository.
    /// </summary>
    public interface IPlayerRepository : IGenericRepository<Player>
    {
    }
}