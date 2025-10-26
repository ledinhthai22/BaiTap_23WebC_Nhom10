using System.Diagnostics;
using BaiTap_23WebC_Nhom10.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiTap_23WebC_Nhom10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(); // Views/Home/Index.cshtml
        }

        public IActionResult Checkout()
        {
            return View(); // Views/Home/Checkout.cshtml
        }

        public IActionResult Contact()
        {
            return View(); // Views/Home/Contact.cshtml
        }

        public IActionResult NotFound()
        {
            return View(); // Views/Home/NotFound.cshtml
        }

        // Optional: error logging
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
