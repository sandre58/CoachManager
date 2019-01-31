﻿using System;
using System.Linq;
using System.Linq.Expressions;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.TrainingModule.Aggregate
{
    public static class TrainingSelectBuilder
    {
        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Training, TrainingDto>> SelectTrainings()
        {
            return x => new TrainingDto
            {
                Id = x.Id,
                RosterId = x.RosterId,
                EndDate = x.EndDate,
                IsCancelled = x.IsCancelled,
                Place = x.Place,
                StartDate = x.StartDate
            };
        }

        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<RosterPlayer, RosterPlayerDto>> SelectPlayersForTraining()
        {
            return x => new RosterPlayerDto
            {
                Id = x.Id,
                RosterId = x.RosterId,
                PlayerId = x.PlayerId,
                SquadId = x.SquadId,
                Squad = x.Squad != null ? new SquadDto
                {
                    Id = x.Squad.Id,
                    Name = x.Squad.Name
                } : null,
                Player = x.Player != null ? new PlayerDto()
                {
                    Id = x.Player.Id,
                    CategoryId = x.Player.CategoryId,
                    Category = x.Player.Category != null ? new CategoryDto()
                    {
                        Id = x.Player.Category.Id,
                        Code = x.Player.Category.Code,
                        Label = x.Player.Category.Label,
                        Order = x.Player.Category.Order
                    } : null,
                    Gender = x.Player.Gender,
                    FirstName = x.Player.FirstName,
                    LastName = x.Player.LastName,
                    Photo = x.Player.Photo,
                    Injuries = x.Player.Injuries.Select(y => new InjuryDto()
                    {
                        Id = y.Id,
                        Date = y.Date,
                        Type = y.Type,
                        ExpectedReturn = y.ExpectedReturn,
                        Condition = y.Condition,
                        Severity = y.Severity
                    })
                } : null
            };
        }
    }
}