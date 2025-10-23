using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BaiTap_23WebC_Nhom10.Models;

namespace BaiTap_23WebC_Nhom10.Controllers
{
    public class LoginController : Controller
    {
    //    private readonly DatabaseHelper _db;
    //    public AccountController(DatabaseHelper db)
    //    {
    //        _db = db;
    //    }
    //    [HttpGet]
    //    public IActionResult Login(string? reUrl)
    //    {
    //        ViewData["ReturnUrl"] = reUrl;
    //        return View();
    //    }
    //    [HttpPost]
    //    public IActionResult Login(User user, string? reUrl)
    //    {
    //        //string sql = @"
    //        //    SELECT U.NAME, U.USER_NAME, U.PASSWORD, R.ROLE_NAME
    //        //    FROM [USER] U
    //        //    JOIN [ROLE] R ON U.ROLE_ID = R.ID
    //        //    WHERE U.USER_NAME = @UserName 
    //        //    AND U.PASSWORD = @Password
    //        //    AND U.STATUS = 1";
    //        //var paramenters = new SqlParameter[]
    //        //{
    //        //    new SqlParameter("@UserName", user.userName ?? ""),
    //        //    new SqlParameter("@Password", user.passWord ?? "")
    //        //};
    //        //DataTable result = _db.ExecuteQuery(sql, paramenters);

    //        //if (result.Rows.Count > 0)
    //        //{
    //        //    var row = result.Rows[0];
    //        //    string role = row["Role_Name"].ToString();
    //        //    HttpContext.Session.SetString("UserName", user.userName);
    //        //    HttpContext.Session.SetString("UserRole", role);
    //        //    if (role == "Admin")
    //        //        return RedirectToAction("Index", "Home", new { area = "Admin" });
    //        //    if (!string.IsNullOrEmpty(reUrl))
    //        //        return Redirect(reUrl);

    //        //    return RedirectToAction("Index", "Home");
    //        //}
    //        //ViewBag.Error = "The User Name or Password is incorrect";
    //        //return View();
    //    }
    //    public IActionResult AccessDenied()
    //    {
    //        return View();
    //    }
    //    public IActionResult Logout()
    //    {
    //        HttpContext.Session.Clear();
    //        return RedirectToAction("Login");
    //    }
        public IActionResult Index()
        {
            return View();
        }
    }
}