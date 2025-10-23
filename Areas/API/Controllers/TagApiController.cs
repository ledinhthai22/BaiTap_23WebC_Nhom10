using Microsoft.AspNetCore.Mvc;
using BaiTap_23WebC_Nhom10.Data;
using BaiTap_03_23WebC_Nhom10
namespace BaiTap_23WebC_Nhom10.Areas.API.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TagApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTags()
        {
           var tags = _context.Tags.ToList();
           return Ok(tags);
        }
    }
}
