using Demo.DataAccess.Models.IdentityModel;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager,SignInManager<ApplicationUser> _signInManager) : Controller
    {
        [HttpGet]
        public IActionResult Register() => View();
        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
           if (!ModelState.IsValid) return View(viewModel);
            var user = new ApplicationUser
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.Lastname,
                UserName = viewModel.UserName,
                Email = viewModel.Email
            };
            var Result = _userManager.CreateAsync(user,viewModel.Password).Result;
            if (Result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else {
                foreach (var error in Result.Errors) { 
                    ModelState.AddModelError(string.Empty,error.Description);   

                }
                return View(viewModel); 
            }
        }
        [HttpGet]
        public IActionResult Login() => View();
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid) { 
                var user= _userManager.FindByEmailAsync(loginViewModel.Email).Result;

                if (user is not null) {
                    var flag= _userManager.CheckPasswordAsync(user, loginViewModel.Password).Result;
                    if (flag) {//Email Exist And Password Correct
                        var result=await  _signInManager.PasswordSignInAsync(user,loginViewModel.Password,loginViewModel.RememberMe,false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }

                    }
                    else//Email Exist And Password Is InCorrect
                    {
                        ModelState.AddModelError(string.Empty, "Password Is Not Found");

                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email Is Not Found");
                }
                    
                    }
                return View(loginViewModel);
        }
        [HttpGet]
        public new ActionResult SignOut(){
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }
    }
}
