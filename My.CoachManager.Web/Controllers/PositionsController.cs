using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Services.PositionModule;

namespace My.CoachManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionAppService _positionAppService;

        public PositionsController(IPositionAppService positionAppService)
        {
            _positionAppService = positionAppService;
        }

        // GET api/[controller]
        [HttpGet]
        public ActionResult<IList<PositionDto>> Get()
        {
            return _positionAppService.GetPositions().ToList();
        }
    }
}
