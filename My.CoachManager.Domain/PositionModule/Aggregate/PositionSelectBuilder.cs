using System;
using System.Linq.Expressions;
using My.CoachManager.Application.Dtos.Positions;
using My.CoachManager.Domain.CategoryModule.Aggregate;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.PositionModule.Aggregate
{
    public static class PositionSelectBuilder
    {
        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Position, PositionDto>> SelectPositionForList()
        {
            return x => new PositionDto()
            {
                Id = x.Id,
                Label = x.Label,
                Order = x.Order
            };
        }
    }
}