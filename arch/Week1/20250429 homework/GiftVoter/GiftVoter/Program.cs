using GiftVoter.Data;
using GiftVoter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace GiftVoter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //            var builder = WebApplication.CreateBuilder(args);

            //            // Add services to the container.
              //       var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
              //       builder.Services.AddDbContext<GiftDbContext>(options =>
              //           options.UseSqlServer(connectionString));
              //       builder.Services.AddDatabaseDeveloperPageExceptionFilter();
              //
            //            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //                .AddEntityFrameworkStores<GiftDbContext>();
            //            builder.Services.AddControllersWithViews();

            //            builder.Services.AddAuthorization();
            //        //    builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);

            //            //builder.Services.AddIdentityCore<Employee>().AddEntityFrameworkStores<GiftDbContext>().AddApiEndpoints();

            //            var app = builder.Build();

            //            // Configure the HTTP request pipeline.
            //            if (app.Environment.IsDevelopment())
            //            {
            //                app.UseMigrationsEndPoint();
            //            }
            //            else
            //            {
            //                app.UseExceptionHandler("/Home/Error");
            //                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //                app.UseHsts();
            //            }

            //            app.UseHttpsRedirection();
            //            app.UseStaticFiles();

            //            app.UseRouting();

            //            app.UseAuthorization();

            //            app.MapControllerRoute(
            //                name: "default",
            //                pattern: "{controller=Home}/{action=Index}/{id?}");
            //            app.MapRazorPages();

            //            //app.MapIdentityApi<Employee>();

            //            app.Run();
            //        }
            //    }
            //}
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<GiftDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            // Add Authentication
            builder.Services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme);


            // Add Authorization
            builder.Services.AddAuthorization();

            // Configure DBContext
            builder.Services.AddDbContext<GiftDbContext>(opt => opt.UseSqlite("DataSource=appdata.db"));

            // Add IdentityCore
            builder.Services
                .AddIdentityCore<Employee>()
                .AddEntityFrameworkStores<GiftDbContext>()
                .AddApiEndpoints();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
              //  app.UseSwagger();
              //  app.UseSwaggerUI();
            }

            // Enable Identity APIs
            app.MapIdentityApi<Employee>();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}