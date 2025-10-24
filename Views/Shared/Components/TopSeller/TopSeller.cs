using Microsoft.AspNetCore.Mvc;
using BaiTap_23WebC_Nhom10.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BaiTap_23WebC_Nhom10.Areas.Admin.Controllers;
namespace BaiTap_23WebC_Nhom10.Views.Shared.Components.Home.Index.TopSeller
{
    public class TopSeller : ViewComponent
    {
        private readonly HttpClient _httpClient;
        public TopSeller(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5021/");
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Move the await statement out of the complex statement for better edit-and-continue support
            List<Product> products = new List<Product>();

            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<Product>>("api/products/type/topsellers");
                products = result ?? new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi View Component TopSeller: {ex.Message}");
            }

            return View(products);
        }
    }
}
