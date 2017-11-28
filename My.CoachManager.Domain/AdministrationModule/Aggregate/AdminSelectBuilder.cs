using System;
using System.Linq.Expressions;
using My.CoachManager.Application.Dtos.Administration;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.AdministrationModule.Aggregate
{
    public static class AdminSelectBuilder
    {
        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Address, CityDto>> SelectCity()
        {
            return x => new CityDto()
            {
                City = x.City,
                PostalCode = x.PostalCode
            };
        }

        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Category, CategoryDto>> SelectCategoryForList()
        {
            return x => new CategoryDto()
            {
                Id = x.Id,
                Label = x.Label,
                Order = x.Order,
                Year = x.Year
            };
        }

        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Position, PositionDto>> SelectPositionForList()
        {
            return x => new PositionDto()
            {
                Id = x.Id,
                Code = x.Code,
                Label = x.Label,
                Order = x.Order
            };
        }

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