using Microsoft.AspNetCore.Mvc;
using BaiTap_23WebC_Nhom10.Models;
namespace BaiTap_23WebC_Nhom10.Views.Shared.Components.ProductSidebar
{
    public class ProductSidebar : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public ProductSidebar(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5021/");
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Product>? products = null;

            try
            {
                products = await _httpClient.GetFromJsonAsync<List<Product>>("api/products");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                products = new List<Product>();
            }

            return View(products);
        }
    }
}
