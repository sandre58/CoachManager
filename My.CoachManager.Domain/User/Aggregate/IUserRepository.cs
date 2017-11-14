using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.User.Aggregate
{
    /// <summary>
    /// Interface used for representing a IUserRepository.
    /// </summary>
    public interface IUserRepository : IGenericRepository<Entities.User>
    {
    }
}