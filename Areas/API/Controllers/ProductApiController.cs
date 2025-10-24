using BaiTap_23WebC_Nhom10.Data;
using BaiTap_23WebC_Nhom10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var products = _context.Products
                .Include(p => p.ProductImages)
                .ToList();

            return Ok(products);
        }

       
        [HttpGet("{id:int}")]
        public IActionResult GetProductById(int id)
        {
            var product = _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetByType(string type)
        {
            List<Product> products = new();

            switch (type.ToLower())
            {
                case "latest":
                    products = await _context.Products
                        .Include(p => p.ProductImages) 
                        .OrderByDescending(p => p.CreateAt)
                        .Take(3)
                        .ToListAsync();
                    break;

                case "topsellers":
                    products = await _context.Products
                        .Include(p => p.ProductImages)
                        .OrderByDescending(p => p.Selled)
                        .Take(3)
                        .ToListAsync();
                    break;

                case "recentlyviewed":
                    products = await _context.Products
                        .Include(p => p.ProductImages)
                        .OrderByDescending(p => p.UpdateAt)
                        .Take(3)
                        .ToListAsync();
                    break;

                case "topnew":
                    products = await _context.Products
                        .Include(p => p.ProductImages)
                        .OrderByDescending(p => p.CreateAt)
                        .Take(3)
                        .ToListAsync();
                    break;

                default:
                    products = await _context.Products
                        .Include(p => p.ProductImages)
                        .Take(3)
                        .ToListAsync();
                    break;
            }

            return Ok(products);
        }
    }
}
