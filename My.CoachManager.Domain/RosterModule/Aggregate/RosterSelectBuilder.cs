using System;
using System.Linq;
using System.Linq.Expressions;
using My.CoachManager.Application.Dtos.Category;
using My.CoachManager.Application.Dtos.Person;
using My.CoachManager.Application.Dtos.Roster;
using My.CoachManager.Application.Dtos.Season;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.RosterModule.Aggregate
{
    public static class RosterSelectBuilder
    {
        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Roster, RosterDto>> SelectRosters()
        {
            return x => new RosterDto
            {
                Id = x.Id,
                Name = x.Name,
                SeasonId = x.SeasonId,
                CategoryId = x.CategoryId,
                Category = x.Category != null ? new CategoryDto
                {
                    Id = x.Category.Id,
                    Code = x.Category.Code,
                    Label = x.Category.Label
                } : null,
                Season = x.Season != null ? new SeasonDto
                {
                    Id = x.Season.Id,
                    Code = x.Season.Code,
                    Label = x.Season.Label
                } : null,
            };
        }

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
        public static Func<RosterPlayer, SquadPlayerDto> SelectSquadPlayer()
        {
            return x => new SquadPlayerDto()
            {
                Id = x.Player.Id,
                FirstName = x.Player.FirstName,
                LastName = x.Player.LastName,
                Address = x.Player.Address != null ? x.Player.Address.Row1 : string.Empty,
                PostalCode = x.Player.Address != null ? x.Player.Address.PostalCode : string.Empty,
                City = x.Player.Address != null ? x.Player.Address.City : string.Empty,
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