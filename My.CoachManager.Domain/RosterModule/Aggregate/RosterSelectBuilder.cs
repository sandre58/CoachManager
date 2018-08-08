using System;
using System.Linq.Expressions;
using My.CoachManager.Application.Dtos.Category;
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
    }
}