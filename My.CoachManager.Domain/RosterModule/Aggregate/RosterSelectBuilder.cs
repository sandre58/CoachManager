﻿using System;
using System.Linq;
using System.Linq.Expressions;
using My.CoachManager.Application.Dtos.Administration;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.Application.Dtos.Rosters;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.RosterModule.Aggregate
{
    public static class RosterSelectBuilder
    {
        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Squad, SquadDto>> SelectSquad()
        {
            return x => new SquadDto()
            {
                Id = x.Id,
                Name = x.Name
            };
        }

        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Func<RosterPlayer, PlayerDetailDto> SelectPlayers()
        {
            return x => new PlayerDetailDto()
            {
                Id = x.Player.Id,
                FirstName = x.Player.FirstName,
                LastName = x.Player.LastName,
                Address = x.Player.Address != null ? new AddressDto()
                {
                    Id = x.Player.Address.Id,
                    Row1 = x.Player.Address.Row1,
                    Row2 = x.Player.Address.Row2,
                    PostalCode = x.Player.Address.PostalCode,
                    City = x.Player.Address.City,
                } : null,
                Birthdate = x.Player.Birthdate,
                Gender = x.Player.Gender,
                LicenseNumber = x.Player.LicenseNumber,
                Photo = x.Player.Photo,
                PlaceOfBirth = x.Player.PlaceOfBirth,
                Laterality = x.Player.Laterality,
                CategoryId = x.Player.CategoryId,
                CountryId = x.Player.CountryId,
                Category = x.Player.Category != null ? new CategoryDto()
                {
                    Id = x.Player.Category.Id,
                    Label = x.Player.Category.Label,
                    Order = x.Player.Category.Order
                } : null,
                Country = x.Player.Country != null ? new CountryDto()
                {
                    Id = x.Player.Country.Id,
                    Label = x.Player.Country.Label,
                    Flag = x.Player.Country.Flag
                } : null,
                Emails = x.Player.Contacts.OfType<Email>().Select(e => new EmailDto()
                {
                    Id = e.Id,
                    Label = e.Label,
                    Default = e.Default,
                    Value = e.Value,
                    PersonId = e.PersonId
                }).AsEnumerable(),
                Phones = x.Player.Contacts.OfType<Phone>().Select(p => new PhoneDto()
                {
                    Id = p.Id,
                    Label = p.Label,
                    Default = p.Default,
                    Value = p.Value,
                    PersonId = p.PersonId
                }).AsEnumerable(),
                LicenseState = x.LicenseState,
                SquadId = x.SquadId,
                Number = x.Number,
                IsMutation = x.IsMutation,
                Height = x.Player.Height,
                Weight = x.Player.Weight,
                ShoesSize = x.Player.ShoesSize,
                Size = x.Player.Size,
            };
        }
    }
}