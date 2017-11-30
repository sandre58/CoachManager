using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.CompetitionModule.Aggregate
{
    /// <summary>
    /// Interface used for representing a Club repository.
    /// </summary>
    public interface IClubRepository : IGenericRepository<Club>
    {
    }
}