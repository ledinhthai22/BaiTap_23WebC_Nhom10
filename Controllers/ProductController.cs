using BaiTap_03_23WebC_Nhom10
using Microsoft.AspNetCore.Mvc;
//using BaiTap_23WebC_Nhom10.Filters;//Bổ sung thêm namspace để dùng filter áa
//using PagedList;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index(int page = 1, int pageSize = 8)
        {

            return View();
        }

        public IActionResult Detail(int id)
        {
            return View();
        }
        //[AuthorizeRole("")]//Chỉ có người đã đăng nhập mới thêm dô dỏ hàng được
        //public IActionResult Cart()
        //{
        //    return View();
        //}
    }
}
