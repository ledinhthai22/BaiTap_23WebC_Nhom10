using BaiTap_23WebC_Nhom10.Data;
using BaiTap_23WebC_Nhom10.Models;
using BaiTap_23WebC_Nhom10.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BaiTap_23WebC_Nhom10.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeRole("Admin")]
    [Route("Admin/[controller]")]
    public class CategoryController: Controller
    {

        private readonly ApplicationDbContext _dbContext;
        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            var categories = _dbContext.Categories
               .OrderByDescending(c => c.Id)
               .ToList();
            return View(categories);
        }
        [HttpGet("them-danh-muc")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("them-danh-muc")]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                TempData["Success"] = "Thêm danh mục thành công!";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
