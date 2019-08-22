using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Services.UserModule;

namespace My.CoachManager.Web.Controllers
{
    //[ServiceFilter(typeof(AuthenticationFilterAttribute))]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        // GET api/[controller]/5
        [Microsoft.AspNetCore.Mvc.HttpGet("{id:int}")]
        public ActionResult<UserDto> Get(int id)
        {
            return _userAppService.GetById(id);
        }

        // GET api/[controller]/login/steve5
        [Microsoft.AspNetCore.Mvc.HttpGet("{login}")]
        public ActionResult<UserDto> GetByLogin([FromUri]string login)
        {
            return _userAppService.GetByLogin(login);
        }

        // GET api/[controller]/login/steve5/******
        [Microsoft.AspNetCore.Mvc.HttpGet("{login}/{password}")]
        public ActionResult<UserDto> GetByLoginAndPassword(string login, string password)
        {
            return _userAppService.GetByLoginAndPassword(login, password);
        }
    }
}
