using Microsoft.AspNetCore.Mvc;
using BaiTap_23WebC_Nhom10.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BaiTap_23WebC_Nhom10.Areas.Admin.Controllers;
namespace BaiTap_23WebC_Nhom10.Views.Shared.Components.RecentlyViewed
{
    public class RecentlyViewed : ViewComponent
    {
        private readonly HttpClient _httpClient;
        public RecentlyViewed(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7214/"); // API base URL
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
                    .Take(3)
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
