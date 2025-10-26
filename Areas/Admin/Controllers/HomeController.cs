using Microsoft.AspNetCore.Mvc;
using BaiTap_23WebC_Nhom10.Filters;
namespace BaiTap_23WebC_Nhom10.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeRole("Admin")]//Bổ sung hạn chế chỉ có admin mới có thể gọi controller nì 
    [Route("Admin/[Controller]")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
