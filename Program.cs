using BaiTap_23WebC_Nhom10.Data;
using Microsoft.EntityFrameworkCore;
namespace BaiTap_23WebC_Nhom10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSession();
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();
            var conStr = builder.Configuration.GetConnectionString("DefaultConnect");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(conStr);
            }); //Ket Noi EF Core o day ... 
            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "product_detail",
                pattern: "product/{slug}",
                defaults: new { controller = "Product", action = "Detail" });
            app.MapControllerRoute(
                 name: "areas",
                 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
