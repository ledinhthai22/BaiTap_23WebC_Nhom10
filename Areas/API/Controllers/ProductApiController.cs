using BaiTap_23WebC_Nhom10.Data;
using Microsoft.AspNetCore.Mvc;
namespace BaiTap_03_23WebC_Nhom10.Controllers.API
{
    [Route("api/products")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductApiController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetProduct()
        {
            var product = _context.Products.ToList();
            return Ok(product);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetProductById(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

    }
}
