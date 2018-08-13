using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.PersonModule.Aggregate
{
    /// <summary>
    /// The Country factory.
    /// </summary>
    public static class CountryFactory
    {
        /// <summary>
        /// Convert the entity to DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Result of the convert to DTO.</returns>
        public static CountryDto Get(Country item)
        {
            if (item == null) return null;

            return new CountryDto
            {
                Id = item.Id,
                Code = item.Code,
                Label = item.Label,
                Description = item.Description,
                Order = item.Order,
                Flag = item.Flag
            };
        }
    }
}