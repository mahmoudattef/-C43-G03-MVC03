using Demo.BusinessLogic.Services;
using Demo.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController (IDepartmentService departmentService) : Controller
    {
        public IActionResult Index()
        {
            var Department =departmentService.GetAllDepartment();

            return View(Department);
        }
    }
}
