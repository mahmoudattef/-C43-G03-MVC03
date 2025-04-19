using Demo.DataAccess.Models.IdentityModel;
using Demo.Presentation.ViewModels;
using Demo.Presentation.ViewModels.UserViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Demo.Presentation.Controllers
{
    public class UserController(UserManager<ApplicationUser> _userManager,
        IWebHostEnvironment _environment,
        ILogger<EmployeesController> _logger) : Controller
    {
        public IActionResult Index(string? UserSearchName) 
        {
            IEnumerable<ApplicationUser> users ;
            if (string.IsNullOrWhiteSpace(UserSearchName))
            {
                users = _userManager.Users.ToList();
            }
            else
            {
                users= _userManager.Users.Where(u=>u.FirstName.ToLower().Contains(UserSearchName.ToLower())).ToList();
            }
           
            
                var userViewModel = users.Select(d => new UserViewModel()
                {
                    Id = d.Id,
                    FName = d.FirstName,
                    LName = d.LastName,
                    Email = d.Email,
                    PhoneNumber = d.PhoneNumber

                });
            return View(userViewModel);
        }

        [HttpGet]
        public IActionResult Details(string? id)
        {
            if (id is null) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            var viewModel = new UserViewModel {
                 Id= user.Id,
                 FName= user.FirstName,
                 LName= user.LastName,
                 Email= user.Email,
                 PhoneNumber= user.PhoneNumber

            };
            return View(viewModel);

        }
        #region Edit

        [HttpGet]
        public IActionResult Edit(string? id)
        {
            if (id is null) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            var viewModel = new EditViewModel
            {
                FName = user.FirstName,
                LName = user.LastName,
                PhoneNumber = user.PhoneNumber
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] string? id, EditViewModel viewModel)
        {
            if (id is null) return BadRequest();
            if (!ModelState.IsValid) return View(viewModel);
            try
            {
                var user = _userManager.FindByIdAsync(id).Result;
                
                user.FirstName =viewModel.FName;
                user.LastName =viewModel.LName;
                user.PhoneNumber =viewModel.PhoneNumber;

                var result = _userManager.UpdateAsync(user).Result;
                if (result.Succeeded) { 
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User Is Not Updated");
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
            var user = _userManager.FindByIdAsync(id).Result;
            if (id is null) return BadRequest();
            try
            {
                var Deleted = _userManager.DeleteAsync(user).Result;
                if (Deleted is not null)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "User Is Not Deleted");
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
