using System;
using System.Linq.Expressions;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.PositionModule.Aggregate
{
    public static class PositionSelectBuilder
    {
        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Position, PositionDto>> SelectPositions()
        {
            return x => new PositionDto
            {
                Id = x.Id,
                Code = x.Code,
                Label = x.Label,
                Description = x.Description,
                Order = x.Order,
                Type = x.Type,
                Column = x.Column,
                Row = x.Row,
                Side = x.Side
            };
        }
    }
}