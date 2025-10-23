using Microsoft.AspNetCore.Mvc;
using BaiTap_23WebC_Nhom10.Models;


public class SingleShopProduct : ViewComponent
{
    private readonly HttpClient _httpClient;
    public SingleShopProduct(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7021/");
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var products = await _httpClient.GetFromJsonAsync<List<Product>>("/api/products");
        return View(products);
    }

}
