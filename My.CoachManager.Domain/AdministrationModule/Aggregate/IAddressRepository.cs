using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.AdministrationModule.Aggregate
{
    /// <summary>
    /// Interface used for representing a IAddressRepository.
    /// </summary>
    public interface IAddressRepository : IGenericRepository<Address>
    {
    }
}