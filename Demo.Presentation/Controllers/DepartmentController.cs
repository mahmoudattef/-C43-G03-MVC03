using Demo.BusinessLogic.DataTransferObjects;
using Demo.BusinessLogic.DataTransferObjects.Department;
using Demo.BusinessLogic.Services.Departments;
using Demo.DataAccess.Repositories;
using Demo.Presentation.ViewModels.DepartmentViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService,
        ILogger<DepartmentController> _logger, IWebHostEnvironment _environment) : Controller
    {
        //Get BaseUrl/Departments/Index
        [HttpGet]
        public IActionResult Index()
        {
            //test send Data from index to View 
            ViewData["Message"]=new DepartmentDto() { Name ="TestViewData"};
            ViewBag.Message =new DepartmentDto() { Name = "TestViewBage" };
            var Department = _departmentService.GetAllDepartment();

            return View(Department);
        }
        #region Create Department
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            { //Server Side Validation

                try
                {
                    var departmentDto = new CreatedDepartmentDto()
                    {
                        Name = departmentViewModel.Name,
                        Code = departmentViewModel.Code,
                        DateOfCreation = departmentViewModel.DateOfCreation,
                        Description = departmentViewModel.Description,
                    };
                    int Result = _departmentService.AddDepartment(departmentDto); //return Number of Row Affected
                    string message;
                    if (Result > 0)
                        message = $"Department {departmentViewModel.Name} Is Created Successfully";
                    else
                        message = $"Department {departmentViewModel.Name} Is Not Be Created";
                    TempData["Message"] = message;
                    return RedirectToAction(nameof(Index));
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
            return View(departmentViewModel);

        }
        #endregion

        #region Details Of Department
        [HttpGet]
        public IActionResult Details(int? id) {
            if (!id.HasValue) return BadRequest(); //400
            var department =_departmentService.GetDepartmentById(id.Value);
            if(department is null) return NotFound(); //404
            return View(department); // if all ok 
        }
        #endregion

        #region Edit Department
        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            var departmentViewModel = new DepartmentViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                DateOfCreation = department.CreatedOn,
                Description = department.Description
            };
            return View(departmentViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id,DepartmentViewModel viewModel) {

            
            if (ModelState.IsValid)
            {
                try
                {

                    var updateDepartment = new UpdatedDepartmentDto()
                    {
                        Id = id,
                        Code = viewModel.Code,
                        Name = viewModel.Name,
                        DateOfCreation = viewModel.DateOfCreation,
                        Description = viewModel.Description
                    };
                    int result = _departmentService.UpdateDeparment(updateDepartment);
                    if (result > 0)
                    {
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Is Not Found");
                  
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
                        return View("ErrorView", ex);

                    }
                }
            }
            return View(viewModel);
        
        }
        #endregion

        #region Delete Department
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if(department is null) return NotFound();
        //    return View(department);
        //}

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if(id == 0) return BadRequest();
            try
            {
                bool Deleted =_departmentService.DeleteDepartment(id);
                if (Deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Is Not Deleted");
                    return RedirectToAction(nameof(Delete), new {id});

                }
            }
            catch (Exception ex) {
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
