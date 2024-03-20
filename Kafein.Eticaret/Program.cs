using Kafein.Database;
using Microsoft.EntityFrameworkCore;

namespace Kafein.Eticaret
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<EticaretDbContext>();
            builder.Services.AddAuthentication()
                .AddCookie(x =>
                {
                    x.Cookie.Name = "eticaret.cookie";
                    x.ExpireTimeSpan = TimeSpan.FromHours(1);
                    x.SlidingExpiration = true;
                    x.LoginPath = "/Account/Login";
                    x.LogoutPath = "/Account/Logout";
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication()
                .UseCookiePolicy();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}