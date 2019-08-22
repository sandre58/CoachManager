using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Services.AddressModule;

namespace My.CoachManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressAppService _addressAppService;

        public AddressesController(IAddressAppService addressAppService)
        {
            _addressAppService = addressAppService;
        }

        // GET api/[controller]
        [HttpGet("cities")]
        public ActionResult<IList<AddressDto>> GetCities()
        {
            return _addressAppService.GetCities().ToList();
        }
    }
}
