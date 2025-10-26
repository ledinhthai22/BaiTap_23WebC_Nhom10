using System.Diagnostics;
using System.Linq;
using BaiTap_23WebC_Nhom10.Data;
using BaiTap_23WebC_Nhom10.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Thêm logging

namespace BaiTap_23WebC_Nhom10.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LoginController> _logger; // Thêm logger

        public LoginController(ApplicationDbContext context, ILogger<LoginController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(string? reUrl)
        {
            ViewData["ReturnUrl"] = reUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Index(User users, string? reUrl)
        {
            // Kiểm tra ModelState và ghi log lỗi nếu có
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                _logger.LogWarning("ModelState invalid: {Errors}", string.Join(", ", errors));
                ViewBag.Error = "Dữ liệu đầu vào không hợp lệ. Vui lòng kiểm tra lại.";
                return View(users);
            }

            // Kiểm tra người dùng trong cơ sở dữ liệu
            var user = _context.Users
                .FirstOrDefault(u => u.UserName == users.UserName && u.Status);

            if (user == null)
            {
                _logger.LogWarning("User not found or inactive: {UserName}", users.UserName);
                ViewBag.Error = "Tên đăng nhập không tồn tại hoặc tài khoản bị khóa.";
                return View(users);
            }

            // Kiểm tra mật khẩu (thay bằng BCrypt nếu mật khẩu được mã hóa)
            bool isPasswordValid = user.PassWord == users.PassWord; // Thay bằng BCrypt nếu cần
            // Ví dụ với BCrypt:
            // bool isPasswordValid = BCrypt.Net.BCrypt.Verify(users.PassWord, user.PassWord);

            if (!isPasswordValid)
            {
                _logger.LogWarning("Invalid password for user: {UserName}", users.UserName);
                ViewBag.Error = "Mật khẩu không đúng.";
                return View(users);
            }

            // Lấy vai trò
            var roleName = _context.Roles
                .Where(r => r.Id == user.RoleID)
                .Select(r => r.RoleName)
                .FirstOrDefault() ?? "User";

            // Lưu vào Session
            HttpContext.Session.SetString("UserName", user.UserName);
            HttpContext.Session.SetString("UserRole", roleName);
            _logger.LogInformation("User logged in: {UserName}, Role: {RoleName}", user.UserName, roleName);

            // Chuyển hướng dựa trên vai trò
            if (roleName == "Admin" && string.IsNullOrEmpty(reUrl))
            {
                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }

            // Chuyển hướng đến reUrl nếu có, hoặc về Home
            if (!string.IsNullOrEmpty(reUrl))
            {
                return Redirect(reUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            ViewBag.Error = "Bạn không có quyền truy cập vào trang này.";
            return View();
        }
    }
}