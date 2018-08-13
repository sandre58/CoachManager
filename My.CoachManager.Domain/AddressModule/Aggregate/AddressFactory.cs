using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.AddressModule.Aggregate
{
    /// <summary>
    /// The Address factory.
    /// </summary>
    public static class AddressFactory
    {
        /// <summary>
        /// Create the entity from the DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The entity.</returns>
        public static Address CreateEntity(AddressDto item)
        {
            if (item == null) return null;

            return new Address
            {
                Id = item.Id,
                CountryId = item.CountryId,
                City = item.City,
                PostalCode = item.PostalCode,
                Latitude = item.Latitude,
                Longitude = item.Longitude,
                Row1 = item.Row1,
                Row2 = item.Row2
            };
        }

        /// <summary>
        /// Create entity.
        /// </summary>
        /// <returns>Result of the convert to DTO.</returns>
        public static Address CreateEntity(string address, string postalCode, string city)
        {
            if (string.IsNullOrEmpty(address) && string.IsNullOrEmpty(postalCode) && string.IsNullOrEmpty(city))
            {
                return null;
            }

            return new Address
            {
                Row1 = address,
                City = city,
                PostalCode = postalCode
            };
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="entity">The entity.</param>
        public static bool UpdateEntity(AddressDto item, Address entity)
        {
            entity.CountryId = item.CountryId;
            entity.City = item.City;
            entity.PostalCode = item.PostalCode;
            entity.Latitude = item.Latitude;
            entity.Longitude = item.Longitude;
            entity.Row1 = item.Row1;
            entity.Row2 = item.Row2;

            return true;
        }

        /// <summary>
        /// Create entity.
        /// </summary>
        /// <returns>Result of the convert to DTO.</returns>
        public static AddressDto GetDto(int id, string address, string postalCode, string city, CrudStatus crudStatus)
        {
            return new AddressDto
            {
                Id = id,
                CrudStatus = crudStatus,
                Row1 = address,
                City = city,
                PostalCode = postalCode
            };
        }
    }
}