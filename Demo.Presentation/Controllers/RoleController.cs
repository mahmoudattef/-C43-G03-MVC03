using Demo.Presentation.ViewModels.RoleViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class RoleController(RoleManager<IdentityRole> _roleManager, IWebHostEnvironment _environment,
        ILogger<EmployeesController> _logger)  : Controller
    {
        public IActionResult Index(string? UserSearchName)
        {
            IEnumerable<IdentityRole> Role;
            if (string.IsNullOrWhiteSpace(UserSearchName)) { 
             Role =_roleManager.Roles.ToList();

            }
            else
            {
                Role = _roleManager.Roles.Where(r=>r.Name.ToLower().Contains(UserSearchName.ToLower())).ToList();

            }
            var roleViewModel = Role.Select(r=>new RoleViewModel
            {
                Id = r.Id,
                RoleName=r.Name
            });
            return View(roleViewModel);
        }

        #region Create Role
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(AddViewModel viewModel)
        {

            if (!ModelState.IsValid) return View(viewModel);
            try
            {
                var role = new IdentityRole
                {
                    Name = viewModel.RoleName,
                };
                var Result = _roleManager.CreateAsync(role).Result;
                if (Result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(viewModel);

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
                return View(viewModel);

            }
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(string? id)
        {
            if (id is null) return BadRequest();
           var role = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            if (role is null) return NotFound();
            var viewModel = new RoleViewModel
            {
                Id = role.Id,
                RoleName=role.Name
            };
            return View(viewModel);

            


        }
        #endregion
        #region Update
        [HttpGet]
        public IActionResult Edit(string? id) 
        {
            if (id is null) return BadRequest();
            var role = _roleManager.Roles.FirstOrDefault(r => r.Id == id);
            if (role is null) return NotFound();
            var viewModel = new RoleViewModel
            {

                Id = role.Id,
                RoleName = role.Name
            };
            return View(viewModel);

        }
        [HttpPost]
        public IActionResult Edit([FromRoute] string id,RoleViewModel viewModel)
        {
            if (id is null) return BadRequest();
            if (!ModelState.IsValid) return View(viewModel);
            try
            {
                var role = _roleManager.FindByIdAsync(id).Result;


                 role.Name=viewModel.RoleName;

                var result = _roleManager.UpdateAsync(role).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors) { 
                    
                          ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(viewModel);
                }



            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(viewModel);
                }
                else
                {
                    _logger.LogError(ex.Message, ex);
                    return View("ErrorView", ex);
                }

            }

        }


        #endregion

        #region Delete
        public IActionResult Delete(string? id)
        {
            var user = _roleManager.FindByIdAsync(id).Result;
            if (id is null) return BadRequest();
            try
            {
                var Deleted = _roleManager.DeleteAsync(user).Result;
                if (Deleted is not null)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Role Is Not Deleted");
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
