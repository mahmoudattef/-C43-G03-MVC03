using Demo.BusinessLogic.Services;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
         {  
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
         });
            //builder.Services.AddScoped<DepartmentRepositories>();
            builder.Services.AddScoped<IDepartmentRepositories,DepartmentRepositories>();
            builder.Services.AddScoped<IDepartmentService,DepartmentService>();
            #endregion

            var app = builder.Build();

            #region Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            #endregion


            app.Run();
        }
    }
}
