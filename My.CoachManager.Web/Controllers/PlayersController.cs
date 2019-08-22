using Microsoft.AspNetCore.Mvc;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Dtos.Parameters;
using My.CoachManager.Application.Dtos.Results;
using My.CoachManager.Application.Services.InjuryModule;
using My.CoachManager.Application.Services.PersonModule;

namespace My.CoachManager.Web.Controllers
{
    //[ServiceFilter(typeof(AuthenticationFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerAppService _playerAppService;
        private readonly IInjuryAppService _injuryAppService;

        public PlayersController(IPlayerAppService playerAppService, IInjuryAppService injuryAppService)
        {
            _playerAppService = playerAppService;
            _injuryAppService = injuryAppService;
        }

        // GET api/[controller]
        [HttpPost("get")]
        public ActionResult<ListResultDto<PlayerDto>> Get([FromBody] PlayersListParametersDto parameters)
        {
            return _playerAppService.GetPlayers(parameters);
        }

        // GET api/[controller]/5
        [HttpGet("{id:int}")]
        public ActionResult<PlayerDto> GetById(int id)
        {
            return _playerAppService.GetPlayerById(id);
        }

        // POST api/[controller]
        [HttpPost]
        public ActionResult<int> Save([FromBody] PlayerDto value)
        {
            return _playerAppService.SavePlayer(value);
        }

        // DELETE api/[controller]/5
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            _playerAppService.RemovePlayer(id);
        }

        [HttpGet("injury/{id:int}")]
        public ActionResult<InjuryDto> GetInjuryById(int id)
        {
            return _injuryAppService.GetInjuryById(id);
        }

        // POST api/[controller]
        [HttpPost("injury")]
        public ActionResult<int> SaveInjury([FromBody] InjuryDto value)
        {
            return value.PlayerId != null ? _injuryAppService.SaveInjury(value.PlayerId.Value, value) : 0;
        }

        // DELETE api/[controller]/5
        [HttpDelete("injury/{id:int}")]
        public void DeleteInjury(int id)
        {
            _injuryAppService.RemoveInjury(id);
        }
    }
}
