using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Services.RosterModule;

namespace My.CoachManager.Web.Controllers
{
    //[ServiceFilter(typeof(AuthenticationFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class SquadsController : ControllerBase
    {
        private readonly ISquadAppService _squadAppService;

        public SquadsController(ISquadAppService squadAppService)
        {
            _squadAppService = squadAppService;
        }

        // GET api/[controller]/5
        [HttpGet("roster/{rosterId:int}")]
        public ActionResult<IList<SquadDto>> Get(int rosterId)
        {
            return _squadAppService.GetSquads(rosterId).ToList();
        }

        // GET api/[controller]/5
        [HttpGet("{id:int}")]
        public ActionResult<SquadDto> GetById(int id)
        {
            return _squadAppService.GetSquadById(id);
        }

        // POST api/[controller]
        [HttpPost]
        public ActionResult<int> Save([FromBody] SquadDto value)
        {
            return _squadAppService.SaveSquad(value.RosterId, value);
        }

        // DELETE api/[controller]/5
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            _squadAppService.RemoveSquad(id);
        }

    }
}
