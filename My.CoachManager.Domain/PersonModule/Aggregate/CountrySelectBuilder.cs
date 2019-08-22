using System;
using System.Linq.Expressions;

using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.PersonModule.Aggregate
{
    public static class CountrySelectBuilder
    {
        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Country, CountryDto>> SelectCountry()
        {
            return x => new CountryDto()
            {
                Id = x.Id,
                Code = x.Code,
                Label = x.Label,
                Flag = x.Flag
            };
        }
    }
}
