using Demo.BusinessLogic.DataTransferObjects.Department;
using Demo.BusinessLogic.DataTransferObjects.Employees;
using Demo.BusinessLogic.Services.Departments;
using Demo.BusinessLogic.Services.Employees;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService, IWebHostEnvironment _environment,
        ILogger<EmployeesController> _logger) : Controller
    {
        public IActionResult Index(string? EmployeeSearchName)
        {
            var Employees = _employeeService.GetAllEmployee(EmployeeSearchName);
            return View(Employees);
        }
        #region Create
        [HttpGet]
        //from service to enforce Clr To Create opj from IDepartmentService
        public IActionResult Create(/*[FromServices]IDepartmentService departmentService*/)
        {
            //ViewData["Departments"]=departmentService.GetAllDepartment();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            { //Server Side Validation

                try
                {
                    var employeeDto =new CreatedEmployeeDto()
                    {
                        Name = employeeViewModel.Name,  
                        Address = employeeViewModel.Address,    
                        Age = employeeViewModel.Age,
                        Email = employeeViewModel.Email,    
                        EmployeeType = employeeViewModel.EmployeeType,
                        Gender = employeeViewModel.Gender,
                        HiringDate = employeeViewModel.HiringDate,
                        IsAction = employeeViewModel.IsAction,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        Salary   = employeeViewModel.Salary,
                        DepartmentId = employeeViewModel.DepartmentId,
                    };
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
            return View(employeeViewModel);
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
        //public IActionResult Edit(int? id) { 
        //    if (!id.HasValue) return BadRequest();
        //    var employee = _employeeService.GetEmployeeById(id.Value);
        //    if (employee is null) return NotFound();
        //    var employeeDto = new UpdatedEmployeeDto()
        //    {
        //        Id=employee.Id,
        //        Name=employee.Name,
        //        Address=employee.Address,
        //        Age=employee.Age,
        //        Email=employee.Email,
        //        PhoneNumber=employee.PhoneNumber,
        //        IsAction=employee.IsAction,
        //        HiringDate=employee.HiringDate,
        //        Gender=Enum.Parse<Gender>(employee.Gender),
        //        EmployeeType=Enum.Parse<EmployeeType>(employee.EmployeeType)
        //    };
        //    return View(employeeDto); 
        //    }
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            var employeeViewModel = new EmployeeViewModel()
            {
                     
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IsAction = employee.IsAction,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId,
                
            };
            return View(employeeViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id,EmployeeViewModel employeeViewModel ) {
            if (!id.HasValue ) return BadRequest();
            if (!ModelState.IsValid)  return View(employeeViewModel);
            try

            {
                var employeeDto = new UpdatedEmployeeDto()
                {
                   Id=id.Value,
                   Name = employeeViewModel.Name,
                   Address = employeeViewModel.Address,
                   Age = employeeViewModel.Age,
                   Email = employeeViewModel.Email,
                   PhoneNumber = employeeViewModel.PhoneNumber,
                   IsAction = employeeViewModel.IsAction,
                   HiringDate = employeeViewModel.HiringDate,
                   Gender=employeeViewModel.Gender,
                   EmployeeType=employeeViewModel.EmployeeType,
                   Salary=employeeViewModel.Salary,
                   
                   DepartmentId = employeeViewModel.DepartmentId,
                };
                var result = _employeeService.UpdateEmployee(employeeDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Is Not Updated");
                    return View(employeeViewModel);
                }
            }
            catch (Exception ex) {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeViewModel);
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
