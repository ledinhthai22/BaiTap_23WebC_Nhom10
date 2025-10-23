using Microsoft.AspNetCore.Mvc;
using BaiTap_23WebC_Nhom10.Data;
namespace BaiTap_23WebC_Nhom10.Areas.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryApiController : Controller
    {
        private readonly ApplicationDbContext _DbContext;
        public CategoryApiController(ApplicationDbContext context)
        {
            _DbContext = context;
        }
        [HttpGet]
        public IActionResult GetCategories()
        {
            var category = _DbContext.Categories.ToList();
            return Ok(category);
        }
    }
}
