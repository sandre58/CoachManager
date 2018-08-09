using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos.Person;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.PersonModule.Aggregate;
using My.CoachManager.Domain.ReferenceModule.Aggregates;

namespace My.CoachManager.Application.Services.PersonModule
{
    /// <summary>
    /// Implementation of the ICountryAppService class.
    /// </summary>
    public class CountryAppService : ICountryAppService
    {
        #region ---- Fields ----

        private readonly IRepository<Country> _countryRepository;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryAppService"/> class.
        /// </summary>
        /// <param name="countryRepository"></param>
        public CountryAppService(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IList<CountryDto> GetCountries()
        {
            return _countryRepository.GetAll(CountrySelectBuilder.SelectCountry(), ReferenceOrderBuilder.OrderByLabel<Country>()).ToList();
        }

        #endregion Methods
    }
}