using Microsoft.AspNetCore.Mvc;
using BaiTap_23WebC_Nhom10.Models;
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
        public IActionResult GetAll()
        {
            var category = _DbContext.Categories.Select(c => new { c.Id ,c.CategoryName }).ToList();
            return Ok(category);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Category model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.CategoryName))
            {
                return BadRequest(new { message = "Tên danh mục không hợp lệ." });
            }

           
            model.Status = true; 

            _DbContext.Categories.Add(model);
            _DbContext.SaveChanges();

           
            var result = new { id = model.Id, categoryName = model.CategoryName };
            return Ok(new { message = "Thêm danh mục thành công", data = result });
        }
    }
}
