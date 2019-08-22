using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Services.PersonModule;

namespace My.CoachManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryAppService _countryAppService;

        public CountriesController(ICountryAppService countryAppService)
        {
            _countryAppService = countryAppService;
        }

        // GET api/[controller]
        [HttpGet]
        public ActionResult<IList<CountryDto>> Get()
        {
            return _countryAppService.GetCountries().ToList();
        }
    }
}
