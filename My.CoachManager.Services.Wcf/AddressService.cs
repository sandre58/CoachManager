using System.Collections.Generic;
using CommonServiceLocator;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Services.AddressModule;
using My.CoachManager.Services.Wcf.Interfaces;

namespace My.CoachManager.Services.Wcf
{
    /// <summary>
    /// Player Service.
    /// </summary>
    public class AddressService : IAddressService
    {
        /// <inheritdoc />
        /// <summary>
        /// Get categories list.
        /// </summary>
        /// <returns></returns>
        public IList<AddressDto> GetCities()
        {
            return ServiceLocator.Current.GetInstance<IAddressAppService>().GetCities();
        }
    }
}