using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Core;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.PersonModule.Aggregate;

namespace My.CoachManager.Application.Services.Persons
{
    /// <summary>
    /// Implementation of the ICountryAppService class.
    /// </summary>
    public class CountryAppService : AppService, ICountryAppService
    {
        #region ---- Fields ----

        private readonly ICountryRepository _countryRepository;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryAppService"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="countryRepository"></param>
        public CountryAppService(ILogger logger, ICountryRepository countryRepository)
            : base(logger)
        {
            _countryRepository = countryRepository;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CountryDto> GetList()
        {
            return _countryRepository.GetAll(CountrySelectBuilder.SelectCountry()).ToArray();
        }

        #endregion Methods
    }
}