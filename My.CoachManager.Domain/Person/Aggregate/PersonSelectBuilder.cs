using System;
using System.Linq;
using System.Linq.Expressions;
using My.CoachManager.Application.Dtos.Admin;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.Person.Aggregate
{
    public static class PersonSelectBuilder
    {
        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Player, CityDto>> SelectCity()
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
        public static Func<Contact, TTarget> SelectContact<TTarget>() where TTarget : ContactDto, new()
        {
            return x => new TTarget
            {
                Id = x.Id,
                Label = x.Label,
                Value = x.Value
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
                Address = x.Address,
                Birthdate = x.Birthdate,
                City = x.City,
                Gender = x.Gender,
                LicenseNumber = x.LicenseNumber,
                Photo = x.Photo,
                PostalCode = x.PostalCode,
                PlaceOfBirth = x.PlaceOfBirth,
                Laterality = x.Laterality,
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
                    Value = e.Value
                }).AsEnumerable(),
                Phones = x.Contacts.OfType<Phone>().Select(p => new PhoneDto()
                {
                    Id = p.Id,
                    Label = p.Label,
                    Value = p.Value
                }).AsEnumerable()
            };
        }
    }
}