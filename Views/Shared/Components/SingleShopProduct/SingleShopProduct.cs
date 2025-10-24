using Microsoft.AspNetCore.Mvc;
using BaiTap_23WebC_Nhom10.Models;
namespace BaiTap_03_23WebC_Nhom10.Views.Shared.Components;

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
