using My.CoachManager.Application.Core;
using My.CoachManager.Application.Dtos.Mapping;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.AddressModule.Aggregate;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Application.Services.Addresses
{
    /// <summary>
    /// Implementation of the AddressAppService class.
    /// </summary>
    public class AddressAppService : AppService, IAddressAppService
    {
        #region ---- Fields ----

        private readonly IAddressRepository _addressRepository;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressAppService"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="addressRepository"></param>
        public AddressAppService(ILogger logger, IAddressRepository addressRepository)
            : base(logger)
        {
            _addressRepository = addressRepository;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public AddressDto CreateOrUpdate(AddressDto dto)
        {
            var entity = dto.ToEntity<Address>();

            _addressRepository.AddOrModify(entity);

            _addressRepository.UnitOfWork.Commit();

            return entity.ToDto<AddressDto>();
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void Remove(AddressDto dto)
        {
            var entity = dto.ToEntity<Address>();

            _addressRepository.Remove(entity);

            _addressRepository.UnitOfWork.Commit();
        }

        #endregion Methods
    }
}