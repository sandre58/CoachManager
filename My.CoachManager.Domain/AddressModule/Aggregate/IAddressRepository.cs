using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.AddressModule.Aggregate
{
    /// <summary>
    /// Interface used for representing a IUserRepository.
    /// </summary>
    public interface IAddressRepository : IGenericRepository<Entities.Address>
    {
    }
}