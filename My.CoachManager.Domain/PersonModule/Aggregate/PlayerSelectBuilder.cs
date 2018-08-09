﻿using System;
using System.Linq;
using System.Linq.Expressions;
using My.CoachManager.Application.Dtos.Category;
using My.CoachManager.Application.Dtos.Person;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.PersonModule.Aggregate
{
    public static class PlayerSelectBuilder
    {
        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Player, PlayerDto>> SelectPlayerDetails()
        {
            return x => new PlayerDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                AddressId = x.AddressId,
                Address = x.Address.Row1,
                PostalCode = x.Address.PostalCode,
                City = x.Address.City,
                Birthdate = x.Birthdate,
                Gender = x.Gender,
                LicenseNumber = x.LicenseNumber,
                Photo = x.Photo,
                PlaceOfBirth = x.PlaceOfBirth,
                Laterality = x.Laterality,
                CategoryId = x.CategoryId,
                Category = x.Category != null ? new CategoryDto()
                {
                    Id = x.Category.Id,
                    Label = x.Category.Label
                } : null,
                CountryId = x.CountryId,
                Country = x.Country != null ? new CountryDto()
                {
                    Id = x.Country.Id,
                    Flag = x.Country.Flag,
                    Label = x.Country.Label
                } : null,
                Emails = x.Contacts.OfType<Email>().Select(e => new EmailDto()
                {
                    Id = e.Id,
                    Label = e.Label,
                    Default = e.Default,
                    Value = e.Value,
                    PersonId = e.PersonId
                }),
                Phones = x.Contacts.OfType<Phone>().Select(p => new PhoneDto()
                {
                    Id = p.Id,
                    Label = p.Label,
                    Default = p.Default,
                    Value = p.Value,
                    PersonId = p.PersonId
                }),
                Height = x.Height,
                Weight = x.Weight,
                ShoesSize = x.ShoesSize,
                Size = x.Size,
            };
        }
    }
}