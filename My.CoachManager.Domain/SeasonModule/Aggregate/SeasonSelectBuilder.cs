using System;
using System.Linq.Expressions;

using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.SeasonModule.Aggregate
{
    public static class SeasonSelectBuilder
    {
        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Season, SeasonDto>> SelectSeasons()
        {
            return x => new SeasonDto
            {
                Id = x.Id,
                Code = x.Code,
                Label = x.Label,
                Description = x.Description,
                Order = x.Order,
                StartDate = x.StartDate,
                EndDate = x.EndDate
            };
        }
    }
}
