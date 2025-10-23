using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json; // Dùng để đọc JSON
using BaiTap_23WebC_Nhom10.Models; // Model Product

namespace BaiTap_23WebC_Nhom10.Views.Shared.Components.ProductRecentPost
{
    public class ProductRecentPost : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public ProductRecentPost(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5021/"); // API Base URL
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                // Gọi API lấy danh sách sản phẩm
                var response = await _httpClient.GetAsync("api/products");

                if (!response.IsSuccessStatusCode)
                    return Content("Không thể tải danh sách bài viết.");

                var products = await response.Content.ReadFromJsonAsync<List<Product>>();

                // Lấy 5 sản phẩm mới nhất
                var recentProducts = products!
                    .OrderByDescending(p => p.id) // hoặc p.CreatedDate nếu có
                    .Take(5)
                    .ToList();

                return View(recentProducts);
            }
            catch (Exception ex)
            {
                return Content("Lỗi: " + ex.Message);
            }
        }
    }
}
