using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json; 
using BaiTap_23WebC_Nhom10.Models; 

namespace BaiTap_23WebC_Nhom10.Views.Shared.Components.ProductRecentPost
{
    public class ProductRecentPost : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public ProductRecentPost(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5021/"); 
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
             
                var response = await _httpClient.GetAsync("api/products");

                if (!response.IsSuccessStatusCode)
                    return Content("Không thể tải danh sách bài viết.");

                var products = await response.Content.ReadFromJsonAsync<List<Product>>();

               
                var recentProducts = products!
                    .OrderByDescending(p => p.Id) // hoặc p.CreatedDate nếu có
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
