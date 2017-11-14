using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.Admin.Aggregate
{
    /// <summary>
    /// Interface used for representing a ICategoryRepository.
    /// </summary>
    public interface ICategoryRepository : IGenericRepository<Category>
    {
    }
}