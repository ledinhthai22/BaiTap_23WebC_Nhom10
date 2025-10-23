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
            var category = _DbContext.Categories.Select(c => new { c.id ,c.categoryName }).ToList();
            return Ok(category);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Category model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.categoryName))
            {
                return BadRequest(new { message = "Tên danh mục không hợp lệ." });
            }

            // Nếu model.status là bool hoặc int? đảm bảo gán mặc định
            model.status = true; // hoặc 1 nếu bạn dùng int

            _DbContext.Categories.Add(model);
            _DbContext.SaveChanges();

            // Trả về object đã tạo (chỉ trả những trường cần thiết)
            var result = new { id = model.id, categoryName = model.categoryName };
            return Ok(new { message = "Thêm danh mục thành công", data = result });
        }
    }
}
