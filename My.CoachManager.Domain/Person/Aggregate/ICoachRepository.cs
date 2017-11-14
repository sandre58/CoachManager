using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.Person.Aggregate
{
    /// <summary>
    /// Interface used for representing a ICoachRepository.
    /// </summary>
    public interface ICoachRepository : IGenericRepository<Coach>
    {
    }
}