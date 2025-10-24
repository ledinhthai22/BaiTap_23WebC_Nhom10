using BaiTap_23WebC_Nhom10.Data;
using BaiTap_23WebC_Nhom10.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Route("product")]
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

        [HttpGet("{slug}")]
        public IActionResult Detail(string slug)
        {
            var product = _context.Products
                .Include(p => p.category)
                .FirstOrDefault(p => p.slug == slug);

            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}
