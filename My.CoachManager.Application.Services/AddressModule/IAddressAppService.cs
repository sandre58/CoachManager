using My.CoachManager.Application.Dtos.Person;

namespace My.CoachManager.Application.Services.AddressModule
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
        AddressDto SaveAddress(AddressDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>
        void RemoveAddress(int id);
    }
}