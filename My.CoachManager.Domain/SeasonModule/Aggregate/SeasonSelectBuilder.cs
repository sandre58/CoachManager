using System;
using System.Linq.Expressions;
using My.CoachManager.Application.Dtos.Administration;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.SeasonModule.Aggregate
{
    public static class SeasonSelectBuilder
    {
        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Season, SeasonDto>> SelectSeasonForList()
        {
            return x => new SeasonDto()
            {
                Id = x.Id,
                Label = x.Label,
                Order = x.Order
            };
        }
    }
}