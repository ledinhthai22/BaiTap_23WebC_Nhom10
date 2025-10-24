using Microsoft.AspNetCore.Mvc;
using BaiTap_23WebC_Nhom10.Models; 

namespace BaiTap_23WebC_Nhom10.Views.Shared.Components.ProductRelated
{
    public class ProductRelated : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public ProductRelated(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5021/"); 
        }

        public async Task<IViewComponentResult> InvokeAsync(int? id = null)
        {

            var response = await _httpClient.GetAsync("api/products");
            if (!response.IsSuccessStatusCode)
            {
                return Content("Không thể tải danh sách sản phẩm.");
            }

            var products = await response.Content.ReadFromJsonAsync<List<Product>>();

            
            if (id != null)
            {
                products = products!.Where(p => p.Id != id).Take(4).ToList();
            }

            return View(products);
        }
    }
}
