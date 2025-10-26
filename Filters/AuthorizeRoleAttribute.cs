using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BaiTap_23WebC_Nhom10.Filters
{
    public class AuthorizeRoleAttribute : ActionFilterAttribute
    {
        private readonly string[] _roles;

        public AuthorizeRoleAttribute(string roles)
        {
            _roles = roles?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                         .Select(r => r.Trim())
                         .ToArray() ?? Array.Empty<string>();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Retrieve ILogger from the service provider
            var logger = context.HttpContext.RequestServices.GetService<ILogger<AuthorizeRoleAttribute>>();

            var session = context.HttpContext.Session;
            var userName = session.GetString("UserName");
            var userRole = session.GetString("UserRole");

            // Log authorization check
            logger?.LogInformation("Checking authorization: UserName={UserName}, UserRole={UserRole}, RequiredRoles={RequiredRoles}",
                userName, userRole, string.Join(", ", _roles));

            // Check if user is logged in
            if (string.IsNullOrEmpty(userName))
            {
                logger?.LogWarning("User not logged in. Redirecting to Login page.");
                var reUrl = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
                context.Result = new RedirectToActionResult("Index", "Login", new { area = "", reUrl });
                return;
            }

            // Check role
            if (_roles.Length > 0 && (string.IsNullOrEmpty(userRole) || !_roles.Contains(userRole)))
            {
                logger?.LogWarning("User {UserName} with role {UserRole} does not have required role(s): {RequiredRoles}. Redirecting to AccessDenied.",
                    userName, userRole, string.Join(", ", _roles));
                context.Result = new RedirectToActionResult("AccessDenied", "Login", new { area = "" });
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}