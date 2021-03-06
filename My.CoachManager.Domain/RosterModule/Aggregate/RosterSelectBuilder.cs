﻿using System;
using System.Linq;
using System.Linq.Expressions;

using My.CoachManager.Application.Dtos;
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
        public static Expression<Func<RosterPlayer, RosterPlayerDto>> SelectRosterPlayers()
        {
            return x => new RosterPlayerDto
            {
                Id = x.Id,
                IsMutation = x.IsMutation,
                Number = x.Number,
                LicenseState = x.LicenseState,
                PlayerId = x.PlayerId,
                RosterId = x.RosterId,
                SquadId = x.SquadId,
                CategoryId = x.CategoryId,
                Category = x.Category != null ? new CategoryDto()
                {
                    Id = x.Category.Id,
                    Label = x.Category.Label
                } : null,
                Player = x.Player != null ? new PlayerDto()
                {
                    Id = x.Player.Id,
                    FirstName = x.Player.FirstName,
                    LastName = x.Player.LastName,
                    AddressId = x.Player.AddressId,
                    Address = x.Player.Address.Row1,
                    PostalCode = x.Player.Address.PostalCode,
                    City = x.Player.Address.City,
                    Birthdate = x.Player.Birthdate,
                    FromDate = x.Player.FromDate,
                    Gender = x.Player.Gender,
                    LicenseNumber = x.Player.LicenseNumber,
                    Photo = x.Player.Photo,
                    PlaceOfBirth = x.Player.PlaceOfBirth,
                    Laterality = x.Player.Laterality,
                    CountryId = x.Player.CountryId,
                    Country = x.Player.Country != null ? new CountryDto()
                    {
                        Id = x.Player.Country.Id,
                        Flag = x.Player.Country.Flag,
                        Label = x.Player.Country.Label
                    } : null,
                    Emails = x.Player.Contacts.OfType<Email>().Select(e => new EmailDto()
                    {
                        Id = e.Id,
                        Label = e.Label,
                        Default = e.Default,
                        Value = e.Value,
                        PersonId = e.PersonId
                    }),
                    Phones = x.Player.Contacts.OfType<Phone>().Select(p => new PhoneDto()
                    {
                        Id = p.Id,
                        Label = p.Label,
                        Default = p.Default,
                        Value = p.Value,
                        PersonId = p.PersonId
                    }),
                    Positions = x.Player.Positions.Select(y => new PlayerPositionDto()
                    {
                        Id = y.Id,
                        IsNatural = y.IsNatural,
                        Rating = y.Rating,
                        PositionId = y.PositionId,
                        Position = y.Position != null ? new PositionDto()
                        {
                            Id = y.Position.Id,
                            Label = y.Position.Label,
                            Code = y.Position.Code,
                            Row = y.Position.Row,
                            Column = y.Position.Column,
                            Order = y.Position.Order
                        } : null
                    }),
                    Injuries = x.Player.Injuries.Select(y => new InjuryDto()
                    {
                        Id = y.Id,
                        Date = y.Date,
                        Type = y.Type,
                        ExpectedReturn = y.ExpectedReturn,
                        Condition = y.Condition,
                        Severity = y.Severity
                    }),
                    Height = x.Player.Height,
                    Weight = x.Player.Weight,
                    ShoesSize = x.Player.ShoesSize,
                    Size = x.Player.Size,
                } : null
            };
        }
    }
}
