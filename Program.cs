using System.Text.Json.Serialization;
using BaiTap_23WebC_Nhom10.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Add for logging support

namespace BaiTap_23WebC_Nhom10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add logging services
            builder.Services.AddLogging(logging =>
            {
                logging.AddConsole(); // Log to console for debugging
                logging.AddDebug();   // Log to debug output
            });

            // Configure session
            builder.Services.AddDistributedMemoryCache(); // Required for session
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Add controllers with views
            builder.Services.AddControllersWithViews();

            // Add HttpClient
            builder.Services.AddHttpClient();

            // Configure DbContext
            var conStr = builder.Configuration.GetConnectionString("DefaultConnect");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(conStr);
            });

            // Configure JSON serialization
            builder.Services.AddControllers()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            var app = builder.Build();

            // Configure middleware pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession(); // Must be before UseAuthorization
            app.UseAuthorization();

            // Configure routing
            app.MapControllerRoute(
                name: "product_detail",
                pattern: "product/{slug}",
                defaults: new { controller = "Product", action = "Detail" });

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}