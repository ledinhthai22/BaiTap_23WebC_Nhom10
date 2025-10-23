//using BaiTap_23WebC_Nhom10.Filters;//Bổ sung thêm namspace để dùng filter áa
using BaiTap_23WebC_Nhom10.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BaiTap_23WebC_Nhom10.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AuthorizeRole("Admin")]//Bổ sung hạn chế chỉ có admin mới có thể gọi controller nì 
    [Route("Admin/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]

        public IActionResult Index()
        {
            var product = _context.Products.Include(p => p.category).OrderByDescending(p => p.id).ToList();
            return View(product);
        }
        [HttpGet("tao-san-pham")]

        public IActionResult Create()
        {
            return View();
        }

    }
}