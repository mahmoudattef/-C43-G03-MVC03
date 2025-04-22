using Demo.DataAccess.Models.IdentityModel;
using Demo.Presentation.Helpers;
using Demo.Presentation.Utilities;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager,SignInManager<ApplicationUser> _signInManager ,IMailService _mailService) : Controller
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

        public IActionResult GoogleLogin()
        {
            var prop = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            };
            return Challenge(prop,GoogleDefaults.AuthenticationScheme);
        }
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme) ;
            var claims=result.Principal.Identities.FirstOrDefault().Claims.Select(claim=> new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public new ActionResult SignOut(){
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(LogIn));
        }

        [HttpGet]
        public IActionResult ForgetPassword() => View();
        [HttpPost]
        public IActionResult SendResetPassword(ForgetViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if (user is not null) {
                    var token=_userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var resetPasswordLink = Url.Action("ResetPassword", "Account", new { email = viewModel.Email ,token}, Request.Scheme);
                    var email = new Email()
                    {
                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body =resetPasswordLink
                    };


                    //EmailSettings.SendEmail(email);
                    _mailService.Send(email);
                      return RedirectToAction(nameof(CheckYourInbox));
                }
                
            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword), viewModel);

        }

        [HttpGet]
        public IActionResult CheckYourInbox() => View();
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"]=email;
            TempData["token"]=token;
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)  return View(viewModel);

            string email=TempData["email"] as string ?? string.Empty;
            string token =TempData["token"] as string ?? string.Empty;
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user is not null)
            {
              var result= _userManager.ResetPasswordAsync(user, token,viewModel.Password).Result;
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty,error.Description);
                    }
                }
            }
                    return View(viewModel);

        }

    }
}
