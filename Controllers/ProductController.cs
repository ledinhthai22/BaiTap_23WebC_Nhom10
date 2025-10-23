using Microsoft.AspNetCore.Mvc;
using BaiTap_23WebC_Nhom10.Data;
using BaiTap_23WebC_Nhom10.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, int pageSize = 8)
        {

            return View();
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
