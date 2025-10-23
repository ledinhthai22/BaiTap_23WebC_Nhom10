using System.Net.Http.Json;
using System.Threading.Channels;
using BaiTap_23WebC_Nhom10.Data;
using BaiTap_23WebC_Nhom10.Models;
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
        private readonly HttpClient _httpClient;
        public ProductsController(ApplicationDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5021/");
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var product = _context.Products.Include(p => p.category).OrderByDescending(p => p.id).ToList();
            return View(product);
        }

        [HttpGet("tao-san-pham")]
        public async Task<IActionResult> Create()
        {
            var categories = await _httpClient.GetFromJsonAsync<List<Category>>("api/categories");
            ViewBag.Categories = categories;
            return View();

        }

        [HttpPost("tao-san-pham")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form, List<IFormFile> images)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(form["PRODUCT_NAME"]))
                {
                    TempData["Error"] = "⚠️ Tên sản phẩm không được bỏ trống.";
                    return RedirectToAction("Create");
                }

                // Lấy Tag ID (nếu có)
                string tagName = form["TAG_ID"];
                int? finalTagId = null;
                if (!string.IsNullOrEmpty(tagName))
                {
                    var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.tagName == tagName);
                    if (existingTag != null)
                    {
                        finalTagId = existingTag.id;
                    }
                }

                // Lưu ý: Trong form bạn đang đặt name="QUANLITY" nhưng property là "quanlity" => vẫn ổn, miễn đúng name.
                // Nếu form đặt sai thì phải đổi cho khớp.
                var product = new Product
                {
                    productName = form["PRODUCT_NAME"],
                    price = decimal.TryParse(form["PRICE"], out var priceValue) ? priceValue : 0,
                    quanlity = int.TryParse(form["QUANLITY"], out var q) ? q : 0,
                    description = form["DESCRIPTION"],
                    discount = decimal.TryParse(form["DISCOUNT"], out var d) ? d : 0,
                    categoryID = int.Parse(form["CATEGORY_ID"]),
                    tagID = finalTagId,
                    status = true,
                    createAT = DateTime.Now
                };

                // 2️ Đường dẫn thư mục upload
                string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "uploads", "products");
                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                List<string> fileNames = new();

                foreach (var file in images)
                {
                    if (file != null && file.Length > 0)
                    {
                        // ⚡ Format: yyyyMMdd-HHmmss-tensanpham-tengoc.jpg
                        string timestamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");
                        string safeProductName = new string(product.productName
                            .Where(c => !Path.GetInvalidFileNameChars().Contains(c))
                            .ToArray());
                        string ext = Path.GetExtension(file.FileName);
                        string uniqueName = $"{timestamp}-{safeProductName}{ext}";
                        string filePath = Path.Combine(uploadFolder, uniqueName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        fileNames.Add($"/img/uploads/products/{uniqueName}");
                    }
                }

                product.image = string.Join(";", fileNames);

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  
                return RedirectToAction("Create");
            }
        }

    }
}