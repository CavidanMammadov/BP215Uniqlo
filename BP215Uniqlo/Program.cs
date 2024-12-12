using BP215Uniqlo.DataAcces;
using BP215Uniqlo.Extensions;
using BP215Uniqlo.Helpers;
using BP215Uniqlo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BP215Uniqlo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<UniqloDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSql"));
            });
            builder.Services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                opt.Password.RequiredLength = 3;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);

            }).AddDefaultTokenProviders().AddEntityFrameworkStores<UniqloDbContext>();
            builder.Services.ConfigureApplicationCookie(x =>
            {
                x.AccessDeniedPath = "/Home/Accesdenied";
            });
            
            SmtpOptions opt = new();
            builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));
            //builder.Services.AddSession();


            var app = builder.Build();
          //  app.UseSession();
            app.UseStaticFiles();
            app.UseUserSeed();
            app.MapControllerRoute(name: "register",
              pattern: "register",
              defaults: new { controller = "Account", action = "Register" }
                );
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=DashBoard}/{action=Index}/{id?}");

            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}
