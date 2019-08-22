using Microsoft.AspNetCore.Mvc;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Dtos.Parameters;
using My.CoachManager.Application.Services.RosterModule;
using System.Collections.Generic;
using System.Linq;

namespace My.CoachManager.Web.Controllers
{
    //[ServiceFilter(typeof(AuthenticationFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class RostersController : ControllerBase
    {
        private readonly IRosterAppService _rosterAppService;

        public RostersController(IRosterAppService rosterAppService)
        {
            _rosterAppService = rosterAppService;
        }

        // GET api/[controller]
        [HttpGet]
        public ActionResult<IList<RosterDto>> Get()
        {
            return _rosterAppService.GetRosters().ToList();
        }

        // GET api/[controller]/5
        [HttpGet("{id:int}")]
        public ActionResult<RosterDto> GetById(int id)
        {
            return _rosterAppService.GetRosterById(id);
        }

        // POST api/[controller]
        [HttpPost]
        public ActionResult<int> Save([FromBody] RosterDto value)
        {
            return _rosterAppService.SaveRoster(value);
        }

        // DELETE api/[controller]/5
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            _rosterAppService.RemoveRoster(id);
        }

        #region Players

        // GET api/[controller]/5
        [HttpGet("players/{rosterId:int}")]
        public ActionResult<IList<RosterPlayerDto>> GetPlayers(int rosterId)
        {
            return _rosterAppService.GetPlayers(rosterId).ToList();
        }

        [HttpGet("player/{id:int}")]
        public ActionResult<RosterPlayerDto> GetPlayerById(int id)
        {
            return _rosterAppService.GetRosterPlayerById(id);
        }

        // POST api/[controller]
        [HttpPost("player")]
        public ActionResult<int> SavePlayer([FromBody] RosterPlayerDto value)
        {
            return _rosterAppService.UpdatePlayer(value);
        }

        // DELETE api/[controller]/5
        [HttpPost("players/add")]
        public void AddPlayers([FromBody] RosterParametersDto parameters)
        {
            _rosterAppService.AddPlayers(parameters.SquadId, parameters.PlayersId);
        }

        // DELETE api/[controller]/5
        [HttpPost("players/delete")]
        public void DeletePlayers([FromBody] RosterParametersDto parameters)
        {
            _rosterAppService.RemovePlayers(parameters.RosterId, parameters.PlayersId);
        }

        // POST api/[controller]
        [HttpPost("players/move")]
        public void MovePlayersInSquad([FromBody] RosterParametersDto parameters)
        {
            _rosterAppService.MovePlayersInSquad(parameters.SquadId, parameters.PlayersId);
        }

        #endregion Players
    }
}