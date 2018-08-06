﻿using System.Collections.Generic;
using My.CoachManager.Application.Dtos.Person;

namespace My.CoachManager.Application.Services.Persons
{
    /// <summary>
    /// Interface defining the category application services.
    /// </summary>
    public interface ICountryAppService
    {
        /// <summary>
        /// Get all dtos list.
        /// </summary>
        /// <returns></returns>
        IEnumerable<CountryDto> GetList();
    }
}