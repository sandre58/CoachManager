using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Dtos.Parameters;
using My.CoachManager.Application.Services.TrainingModule;

namespace My.CoachManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingsController : ControllerBase
    {
        private readonly ITrainingAppService _trainingAppService;

        public TrainingsController(ITrainingAppService trainingAppService)
        {
            _trainingAppService = trainingAppService;
        }

        // GET api/[controller]
        [HttpGet("{rosterId:int}")]
        public ActionResult<IList<TrainingDto>> Get(int rosterId)
        {
            return _trainingAppService.GetTrainings(rosterId).ToList();
        }

        // GET api/[controller]/5
        [HttpGet("training/{id:int}")]
        public ActionResult<TrainingDto> GetById(int id)
        {
            return _trainingAppService.GetTrainingById(id);
        }

        // POST api/[controller]
        [HttpPost]
        public ActionResult<int> Save([FromBody] TrainingDto value)
        {
            if (value.RosterId != null) return _trainingAppService.SaveTraining(value.RosterId.Value, value);
            return null;
        }

        // DELETE api/[controller]/5
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            _trainingAppService.RemoveTraining(id);
        }

        // POST api/[controller]
        [HttpPost("attendances")]
        public ActionResult<int> SaveAttendances([FromBody] TrainingParametersDto value)
        {
            return _trainingAppService.SaveTrainingAttendances(value.TrainingId, value.Attendances.ToList());
        }

        // GET api/[controller]/5
        [HttpGet("players/{id:int}")]
        public ActionResult<IList<RosterPlayerDto>> GetPlayers(int id)
        {
            return _trainingAppService.GetPlayersForTraining(id).ToList();
        }

        // POST api/[controller]
        [HttpPost("add")]
        public ActionResult<int> AddTrainings([FromBody] TrainingParametersDto value)
        {
            return _trainingAppService.AddTrainings(value.RosterId, value.StartDate, value.EndDate, value.StartTime, value.EndTime, value.Place, value.Days).Count;
        }
    }
}
