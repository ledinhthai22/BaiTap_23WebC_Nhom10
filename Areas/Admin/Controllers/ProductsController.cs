using BaiTap_23WebC_Nhom10.Data;
using BaiTap_23WebC_Nhom10.Models;
using BaiTap_23WebC_Nhom10.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
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
            var product = _context.Products.Include(p => p.Category).OrderByDescending(p => p.Id).ToList();
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
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (string.IsNullOrEmpty(form["PRODUCT_NAME"]))
                {
                    TempData["Error"] = "Tên sản phẩm không được bỏ trống.";
                    return RedirectToAction("Create");
                }

                string tagName = form["TAG_ID"];
                int? finalTagId = null;
                if (!string.IsNullOrEmpty(tagName))
                {
                    var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.TagName == tagName);
                    if (existingTag != null)
                        finalTagId = existingTag.Id;
                }

                var product = new Product
                {
                    ProductName = form["PRODUCT_NAME"],
                    Price = decimal.TryParse(form["PRICE"], out var priceValue) ? priceValue : 0,
                    Quanlity = int.TryParse(form["QUANLITY"], out var q) ? q : 0,
                    Description = form["DESCRIPTION"],
                    Discount = decimal.TryParse(form["DISCOUNT"], out var d) ? d : 0,
                    CategoryId = int.TryParse(form["CATEGORY_ID"], out var catId) ? catId : null,
                    Slug = SlugHelper.GenerateSlug(form["PRODUCT_NAME"]),
                    TagId = finalTagId,
                    Status = true,
                    CreateAt = DateTime.Now
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                //Lưu file ảnh
                string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "uploads", "products");
                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                int index = 0;
                foreach (var file in images.Where(f => f != null && f.Length > 0))
                {

                    string ext = Path.GetExtension(file.FileName);
                    string safeProductName = SlugHelper.GenerateSlug(product.ProductName.ToLower());

                    // Đặt tên file ảnh theo định dạng yêu cầu
                    string fileName = $"{DateTime.Now:yyyyMMddHHmmss}-{safeProductName}-{index}{ext}";

                    // Tạo đường dẫn tuyệt đối để lưu vật lý
                    string filePath = Path.Combine(uploadFolder, fileName);

                    // Ghi file vật lý
                    await using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Thêm bản ghi vào bảng PRODUCT_IMAGES
                    var img = new ProductImage
                    {
                        ProductId = product.Id,
                        ImagePath = $"/img/uploads/products/{fileName}",
                        IsMain = (index == 0)
                    };
                    _context.ProductImages.Add(img);

                    index++;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                TempData["Success"] = "✅ Thêm sản phẩm thành công!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                TempData["Error"] = "Lỗi khi thêm sản phẩm: " + ex.Message;
                return RedirectToAction("Create");
            }
        }


    }
}