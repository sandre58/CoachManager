using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.RosterModule.Aggregate
{
    /// <summary>
    /// Interface used for representing a IRosterRepository.
    /// </summary>
    public interface IRosterRepository : IGenericRepository<Roster>
    {
    }
}