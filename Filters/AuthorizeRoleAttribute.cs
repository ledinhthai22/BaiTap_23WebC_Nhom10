////using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;

//namespace BaiTap_23WebC_Nhom10.Filters
//{
//    public class AuthorizeRoleAttribute : ActionFilterAttribute
//    {
//        //private readonly string _role;

//        //public AuthorizeRoleAttribute(string role)
//        //{
//        //    _role = role;
//        //}

//        //public override void OnActionExecuting(ActionExecutingContext context)
//        //{
//        //    var session = context.HttpContext.Session;
//        //    var userName = session.GetString("UserName");
//        //    var userRole = session.GetString("UserRole");

//        //    if (string.IsNullOrEmpty(userName))
//        //    {
//        //        var reUrl = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
//        //        context.Result = new RedirectToActionResult("Login", "Account", new { area = "", reUrl });
//        //        return;
//        //    }

//        //    if (!string.IsNullOrEmpty(_role) && userRole != _role)
//        //    {
//        //        context.Result = new RedirectToActionResult("AccessDenied", "Account", new { area = "" });
//        //        return;
//        //    }

//        //    base.OnActionExecuting(context);
//        //}
//    }
//}
