using System;
using System.Linq.Expressions;

using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.AddressModule.Aggregate
{
    public static class AddressSelectBuilder
    {
        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Address, AddressDto>> SelectCityAndPostalCode()
        {
            return x => new AddressDto
            {
                PostalCode = x.PostalCode,
                City = x.City
            };
        }
    }
}
