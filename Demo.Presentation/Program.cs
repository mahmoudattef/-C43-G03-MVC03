using Demo.BusinessLogic.Profiles;
using Demo.BusinessLogic.Services.AttachmentService;
using Demo.BusinessLogic.Services.Departments;
using Demo.BusinessLogic.Services.Employees;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.IdentityModel;
using Demo.DataAccess.Repositories.Classes;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Demo.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
         {
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
             options.UseLazyLoadingProxies();
         });
            //builder.Services.AddScoped<DepartmentRepositories>();
            //builder.Services.AddScoped<IDepartmentRepositories,DepartmentRepositories>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            //builder.Services.AddScoped<IEmployeeRepository ,EmployeeRepositories>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddAutoMapper(E => E.AddProfile(new MappingProfiles()));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
            {
                //some configuration 
            ////Options.User.RequireUniqueEmail = true
            //    Options.Password.RequireLowercase = true;
            //    Options.Password.RequireUppercase = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

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
                pattern: "{controller=Account}/{action=Register}/{id?}");
            #endregion


            app.Run();
        }
    }
}
