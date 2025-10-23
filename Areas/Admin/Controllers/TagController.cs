using BaiTap_23WebC_Nhom10.Data;
using BaiTap_23WebC_Nhom10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BaiTap_23WebC_Nhom10.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AuthorizeRole("Admin")]
    [Route("Admin/[controller]")]
    public class TagController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public TagController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            var tag = _dbContext.Tags
               .OrderByDescending(t => t.id)
               .ToList();
            return View(tag);
        }
        [HttpGet("them-tag")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("them-tag")]
        public IActionResult Create(Tag tags)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Tags.Add(tags);
                _dbContext.SaveChanges();
                TempData["Success"] = "Thêm thẻ thành công!";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
