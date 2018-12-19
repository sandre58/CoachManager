using System.Collections.Generic;
using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Application.Services.AddressModule
{
    /// <summary>
    /// Interface defining the address application services.
    /// </summary>
    public interface IAddressAppService
    {
        /// <summary>
        /// Get all dtos list.
        /// </summary>
        /// <returns></returns>
        IList<AddressDto> GetCities();

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        int SaveAddress(AddressDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>
        void RemoveAddress(int id);
    }
}