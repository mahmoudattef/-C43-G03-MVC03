using Demo.BusinessLogic.DataTransferObjects.Department;
using Demo.BusinessLogic.DataTransferObjects.Employees;
using Demo.BusinessLogic.Services.Employees;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService, IWebHostEnvironment _environment,
        ILogger<EmployeesController> _logger) : Controller
    {
        public IActionResult Index()
        {
            var Employees = _employeeService.GetAllEmployee();
            return View(Employees);
        }
        #region Create
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            { //Server Side Validation

                try
                {
                    int Result = _employeeService.CreateEmployee(employeeDto); //return Number of Row Affected
                    if (Result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Can Not Created");

                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        //log error in console and return same view with error message
                        ModelState.AddModelError(string.Empty, ex.Message);

                    }
                    else
                    {
                        _logger.LogError(ex.Message);

                    }
                }
            }
            return View(employeeDto);
        }
        #endregion
        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            return employee is null ? NotFound() : View(employee);
        }
        #endregion
        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id) { 
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            var employeeDto = new UpdatedEmployeeDto()
            {
                Id=employee.Id,
                Name=employee.Name,
                Address=employee.Address,
                Age=employee.Age,
                Email=employee.Email,
                PhoneNumber=employee.PhoneNumber,
                IsAction=employee.IsAction,
                HiringDate=employee.HiringDate,
                Gender=Enum.Parse<Gender>(employee.Gender),
                EmployeeType=Enum.Parse<EmployeeType>(employee.EmployeeType)
            };
            return View(employeeDto); 
            }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id,UpdatedEmployeeDto employeeDto) {
            if (!id.HasValue || id != employeeDto.Id) return BadRequest();
            if (!ModelState.IsValid)  return View(employeeDto);
            try

            {
                var result = _employeeService.UpdateEmployee(employeeDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Is Not Updated");
                    return View(employeeDto);
                }
            }
            catch (Exception ex) {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeDto);
                }
                else
                {
                    _logger.LogError(ex.Message, ex);
                    return View("ErrorView",ex);
                }
            }
            

        }
        #endregion
        #region Delete
        public IActionResult Delete( int id) {

            if (id == 0) return BadRequest();
            try
            {
                bool Deleted = _employeeService.DeleteEmployee(id);
                if (Deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Is Not Deleted");
                    return RedirectToAction(nameof(Delete), new { id });

                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    //log error in console and return same view with error message
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));


                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);

                }
            }

        }
        #endregion
    }
}
