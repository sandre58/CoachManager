using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Services.SeasonModule;

namespace My.CoachManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonsController : ControllerBase
    {
        private readonly ISeasonAppService _seasonAppService;

        public SeasonsController(ISeasonAppService seasonAppService)
        {
            _seasonAppService = seasonAppService;
        }

        // GET api/[controller]
        [HttpGet]
        public ActionResult<IList<SeasonDto>> Get()
        {
            return _seasonAppService.GetSeasons().ToList();
        }

        // GET api/[controller]/5
        [HttpGet("{id:int}")]
        public ActionResult<SeasonDto> GetById(int id)
        {
            return _seasonAppService.GetSeasonById(id);
        }

        // POST api/[controller]
        [HttpPost]
        public ActionResult<int> Save([FromBody] SeasonDto value)
        {
            return _seasonAppService.SaveSeason(value);
        }

        // POST api/[controller]
        [HttpPost("orders")]
        public ActionResult<bool> UpdateOrders([FromBody] IDictionary<int,int> value)
        {
            _seasonAppService.UpdateOrders(value);
            return true;
        }

        // DELETE api/[controller]/5
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            _seasonAppService.RemoveSeason(id);
        }
    }
}
