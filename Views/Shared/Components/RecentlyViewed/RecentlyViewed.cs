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
            _httpClient.BaseAddress = new Uri("http://localhost:5021/");
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Product> products = new List<Product>();

            try
            {
                products = await _httpClient.GetFromJsonAsync<List<Product>>("api/products/type/recentlyviewed") ?? new List<Product>();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Lỗi View Component RecentlyViewed: {ex.Message}");
            }

            return View(products);
        }
    }
}
