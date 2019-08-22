using System.Collections.Generic;
using System.Linq;

using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.AddressModule.Aggregate;
using My.CoachManager.Domain.AppModule.Services;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Application.Services.AddressModule
{
    /// <summary>
    /// Implementation of the AddressAppService class.
    /// </summary>
    public class AddressAppService : IAddressAppService
    {
        #region ---- Fields ----

        private readonly IRepository<Address> _addressRepository;

        private readonly ICrudDomainService<Address, AddressDto> _crudDomainService;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressAppService"/> class.
        /// </summary>
        /// <param name="addressRepository"></param>
        /// <param name="crudDomainService"></param>
        public AddressAppService(IRepository<Address> addressRepository, ICrudDomainService<Address, AddressDto> crudDomainService)
        {
            _addressRepository = addressRepository;
            _crudDomainService = crudDomainService;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IList<AddressDto> GetCities()
        {
            return _addressRepository.GetAll(AddressSelectBuilder.SelectCityAndPostalCode(), AddressOrderBuilder.OrderByCity()).ToList();
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public int SaveAddress(AddressDto dto)
        {
            return _crudDomainService.Save(dto, AddressFactory.CreateEntity, AddressFactory.UpdateEntity);
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void RemoveAddress(int id)
        {
            _crudDomainService.Remove(id);
        }

        #endregion Methods
    }
}
