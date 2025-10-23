using Microsoft.AspNetCore.Mvc;
using BaiTap_23WebC_Nhom10.Models; // chứa class Product

namespace BaiTap_23WebC_Nhom10.Views.Shared.Components.ProductRelated
{
    public class ProductRelated : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public ProductRelated(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5021/"); // API base URL
        }

        public async Task<IViewComponentResult> InvokeAsync(int? id = null)
        {
            // Lấy danh sách sản phẩm từ API
            var response = await _httpClient.GetAsync("api/products");
            if (!response.IsSuccessStatusCode)
            {
                return Content("Không thể tải danh sách sản phẩm.");
            }

            var products = await response.Content.ReadFromJsonAsync<List<Product>>();

            // Nếu có id (ví dụ sản phẩm hiện tại), bạn có thể lọc sản phẩm liên quan
            if (id != null)
            {
                products = products!.Where(p => p.id != id).Take(4).ToList(); // 4 sản phẩm liên quan
            }

            return View(products);
        }
    }
}
