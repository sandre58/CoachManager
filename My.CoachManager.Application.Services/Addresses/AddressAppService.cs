using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Application.Services.Addresses
{
    /// <summary>
    /// Implementation of the AddressAppService class.
    /// </summary>
    public class AddressAppService : IAddressAppService
    {
        #region ---- Fields ----

        private readonly IRepository<Address> _addressRepository;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressAppService"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="addressRepository"></param>
        public AddressAppService(ILogger logger, IRepository<Address> addressRepository)
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
            //var entity = dto.ToEntity<Address>();

            ////_addressRepository.AddOrModify(entity);

            //_addressRepository.UnitOfWork.Commit();

           //return entity.ToDto<AddressDto>();

            return null;
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void Remove(AddressDto dto)
        {
            //var entity = dto.ToEntity<Address>();

            //_addressRepository.Remove(entity);

            //_addressRepository.UnitOfWork.Commit();
        }

        #endregion Methods
    }
}