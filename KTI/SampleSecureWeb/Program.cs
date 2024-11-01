// using Microsoft.AspNetCore.Authentication.Cookies;
// using Microsoft.EntityFrameworkCore;
// using SampleSecureWeb.Data;

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.AddControllersWithViews();

// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// //menambahkan autentukasi
// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
// .AddCookie(options =>
// {
//     options.LoginPath = "/Account/Login";
//     options.AccessDeniedPath = "/Account/AccessDenied";
// });

// builder.Services.AddScoped<IStudent, StudentData>();
// builder.Services.AddScoped<IUser, UserData>();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
// app.UseStaticFiles();

// app.UseRouting();

// app.UseAuthentication();
// app.UseAuthorization();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

// app.Run();

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SampleSecureWeb.Data;

namespace SampleSecureWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Authentication configuration
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";

                    if (builder.Environment.IsDevelopment())
                    {
                        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    }
                    else
                    {
                        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    }

                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(3);
                    options.SlidingExpiration = true;
                });

            builder.Services.AddScoped<IStudent, StudentData>();
            builder.Services.AddScoped<IUser, UserData>();

            var app = builder.Build();
            
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
