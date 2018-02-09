using System.Collections.Generic;
using My.CoachManager.Application.Core;
using My.CoachManager.Application.Dtos.Persons;

namespace My.CoachManager.Application.Services.Persons
{
    /// <summary>
    /// Interface defining the category application services.
    /// </summary>
    public interface ICountryAppService : IAppService
    {
        /// <summary>
        /// Get all dtos list.
        /// </summary>
        /// <returns></returns>
        IEnumerable<CountryDto> GetList();
    }
}