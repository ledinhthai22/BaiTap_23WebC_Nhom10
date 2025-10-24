using BaiTap_23WebC_Nhom10.Data;
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

        public IActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.ProductImages)
                .ToList();

            return View(products);
        }

        [HttpGet("{slug}")]
        public IActionResult Detail(string slug, int? imageId)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Include(p => p.Tag)
                .FirstOrDefault(p => p.Slug == slug);

            if (product == null)
                return NotFound();

            if (imageId.HasValue && product.ProductImages != null)
            {
                foreach (var img in product.ProductImages)
                    img.IsMain = false;

                var selected = product.ProductImages.FirstOrDefault(i => i.Id == imageId.Value);
                if (selected != null)
                    selected.IsMain = true;
            }

            return View(product);
        }
    }
}
