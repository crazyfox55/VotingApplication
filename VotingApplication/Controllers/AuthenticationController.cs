using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VotingApplication.ViewModels;

namespace VotingApplication.Controllers
{
    public class AuthenticationController : Controller
    {
        protected SignInManager<ApplicationUser> _SignInManager;

        public AuthenticationController(
            SignInManager<ApplicationUser> signInManager)
        {
            _SignInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["Purpose"] = "User Login";
            ViewData["Submit"] = "Login";

            return View();
        }

        [HttpPost]
        // this is not implemented yet
        //[RequireHttps]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["Purpose"] = "User Login";
            ViewData["Submit"] = "Login";
            
            await _SignInManager.SignOutAsync();

            if (ModelState.IsValid)
            {
                var result = await _SignInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        string action = nameof(UserController.Dashboard);
                        string controller = nameof(UserController);
                        controller = controller.Remove(controller.Length - 10);
                        return RedirectToAction(action, controller);
                    }

                    return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }
            }

            return View("Login", model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LogoutAsync()
        {
            await _SignInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }
    }
}
