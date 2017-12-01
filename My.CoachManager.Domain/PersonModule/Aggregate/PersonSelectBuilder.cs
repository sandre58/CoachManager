using System;
using System.Linq;
using System.Linq.Expressions;
using My.CoachManager.Application.Dtos.Administration;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.PersonModule.Aggregate
{
    public static class PersonSelectBuilder
    {
        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Country, CountryDto>> SelectCountries()
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
        public static Expression<Func<Category, CategoryDto>> SelectCategories()
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
        public static Expression<Func<Player, PlayerDto>> SelectPlayerForList()
        {
            return x => new PlayerDto()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address = x.Address != null ? new AddressDto()
                {
                    Id = x.Address.Id,
                    Row1 = x.Address.Row1,
                    Row2 = x.Address.Row2,
                    PostalCode = x.Address.PostalCode,
                    City = x.Address.City,
                } : null,
                Birthdate = x.Birthdate,
                Gender = x.Gender,
                LicenseNumber = x.LicenseNumber,
                Photo = x.Photo,
                PlaceOfBirth = x.PlaceOfBirth,
                Laterality = x.Laterality,
                CategoryId = x.CategoryId,
                CountryId = x.CountryId,
                Category = x.Category != null ? new CategoryDto()
                {
                    Id = x.Category.Id,
                    Label = x.Category.Label,
                    Order = x.Category.Order
                } : null,
                Country = x.Country != null ? new CountryDto()
                {
                    Id = x.Country.Id,
                    Label = x.Country.Label,
                    Flag = x.Country.Flag
                } : null,
                Emails = x.Contacts.OfType<Email>().Select(e => new EmailDto()
                {
                    Id = e.Id,
                    Label = e.Label,
                    Default = e.Default,
                    Value = e.Value,
                    PersonId = e.PersonId
                }).AsEnumerable(),
                Phones = x.Contacts.OfType<Phone>().Select(p => new PhoneDto()
                {
                    Id = p.Id,
                    Label = p.Label,
                    Default = p.Default,
                    Value = p.Value,
                    PersonId = p.PersonId
                }).AsEnumerable()
            };
        }
    }
}