using System;
using System.Linq.Expressions;

using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.SquadModule.Aggregate
{
    public static class SquadSelectBuilder
    {
        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Squad, SquadDto>> SelectSquads()
        {
            return x => new SquadDto
            {
                Id = x.Id,
                Name = x.Name,
                RosterId = x.RosterId
            };
        }
    }
}
