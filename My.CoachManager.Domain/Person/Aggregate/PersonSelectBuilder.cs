using System;
using System.Linq.Expressions;
using My.CoachManager.Application.Dtos.Admin;
using My.CoachManager.Application.Dtos.Persons;

namespace My.CoachManager.Domain.Person.Aggregate
{
    public static class PersonSelectBuilder
    {
        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Entities.Player, CityDto>> SelectCity()
        {
            return x => new CityDto()
            {
                City = x.City,
                PostalCode = x.PostalCode
            };
        }

        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Entities.Country, CountryDto>> SelectCountries()
        {
            return x => new CountryDto()
            {
                Id = x.Id,
                Code = x.Code,
                Label = x.Label,
                Flag = x.Flag
            };
        }

        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Entities.Category, CategoryDto>> SelectCategories()
        {
            return x => new CategoryDto()
            {
                Id = x.Id,
                Label = x.Label
            };
        }

        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Entities.Player, PlayerDto>> SelectPlayerForList()
        {
            return x => new PlayerDto()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address = x.Address,
                Birthdate = x.Birthdate,
                City = x.City,
                Gender = x.Gender,
                LicenseNumber = x.LicenseNumber,
                Photo = x.Photo,
                PostalCode = x.PostalCode,
                PlaceOfBirth = x.PlaceOfBirth,
                Laterality = x.Laterality
            };
        }
    }
}