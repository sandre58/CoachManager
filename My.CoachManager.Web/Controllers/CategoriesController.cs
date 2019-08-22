using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Services.CategoryModule;

namespace My.CoachManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryAppService _categoryAppService;

        public CategoriesController(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        // GET api/[controller]
        [HttpGet]
        public ActionResult<IList<CategoryDto>> Get()
        {
            return _categoryAppService.GetCategories().ToList();
        }

        // GET api/[controller]/5
        [HttpGet("{id:int}")]
        public ActionResult<CategoryDto> GetById(int id)
        {
            return _categoryAppService.GetCategoryById(id);
        }

        // POST api/[controller]
        [HttpPost]
        public ActionResult<int> Save([FromBody] CategoryDto value)
        {
            return _categoryAppService.SaveCategory(value);
        }

        // POST api/[controller]
        [HttpPost("orders")]
        public ActionResult<bool> UpdateOrders([FromBody] IDictionary<int,int> value)
        {
            _categoryAppService.UpdateOrders(value);
            return true;
        }

        // DELETE api/[controller]/5
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            _categoryAppService.RemoveCategory(id);
        }
    }
}
