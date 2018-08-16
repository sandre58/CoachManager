using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Presentation.Prism.Models.Aggregates
{
    /// <summary>
    /// The Address factory.
    /// </summary>
    public static class AddressFactory
    {
        /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>The model.</returns>
        public static CityModel Get(AddressDto dto)
        {
            if (dto == null) return null;

            var result = new CityModel
            {
                City = dto.City,
                PostalCode = dto.PostalCode
            };

            return result;
        }
    }
}