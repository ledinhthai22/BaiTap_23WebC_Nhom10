using Microsoft.AspNetCore.Mvc;
using BaiTap_23WebC_Nhom10.Data;
using BaiTap_23WebC_Nhom10.Models;
namespace BaiTap_23WebC_Nhom10.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AuthorizeRole("Admin")]//Bổ sung hạn chế chỉ có admin mới có thể gọi controller nì 
    [Route("Admin/[Controller]")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _DbContext;
        public CategoryController(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var categories = _DbContext.Categories.OrderByDescending(c => c.id).ToList();
            return View(categories);
        }

        [HttpGet("Them-danh-muc")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("Them-danh-muc")]
        public IActionResult Create(Category category)
        {
            if(ModelState.IsValid)
            {
                category.status = true;
                _DbContext.Categories.Add(category);
                _DbContext.SaveChanges();
                TempData["Success"] = "Thêm danh mục thành công!";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
