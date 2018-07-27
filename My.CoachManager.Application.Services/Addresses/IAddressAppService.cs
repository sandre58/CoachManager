using My.CoachManager.Application.Dtos.Persons;

namespace My.CoachManager.Application.Services.Addresses
{
    /// <summary>
    /// Interface defining the address application services.
    /// </summary>
    public interface IAddressAppService
    {
        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        AddressDto CreateOrUpdate(AddressDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>
        void Remove(AddressDto dto);
    }
}