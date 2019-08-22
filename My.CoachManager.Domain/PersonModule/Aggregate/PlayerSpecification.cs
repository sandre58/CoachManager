using My.CoachManager.Application.Dtos.Parameters;
using My.CoachManager.Domain.Core.Specification;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.PersonModule.Aggregate
{
    public static class PlayerSpecification
    {
        /// <summary>
        /// Specification exist in other customer.
        /// </summary>
        /// <returns>The specification for getting if exist in other customer.</returns>
        public static ISpecification<Player> IsMatch(PlayersListParametersDto parameters)
        {
            Specification<Player> specification = new TrueSpecification<Player>();

            if (!string.IsNullOrEmpty(parameters.Name))
            {
                Specification<Player> specName = new DirectSpecification<Player>(x => x.LastName.Contains(parameters.Name));
                specName |= new DirectSpecification<Player>(x => x.FirstName.Contains(parameters.Name));
                specification &= specName;
            }

            if (parameters.YearOfBirth != null)
            {
                specification &= new DirectSpecification<Player>(x => x.Birthdate.Value.Year == parameters.YearOfBirth);
            }

            if (parameters.CountryId != null)
            {
                specification &= new DirectSpecification<Player>(x => x.CountryId == parameters.CountryId);
            }

            if (parameters.Gender != null)
            {
                specification &= new DirectSpecification<Player>(x => x.Gender == parameters.Gender);
            }

            if (!string.IsNullOrEmpty(parameters.LicenseNumber))
            {
                specification &= new DirectSpecification<Player>(x => x.LicenseNumber.Contains(parameters.LicenseNumber));
            }

            return specification;
        }
    }
}
