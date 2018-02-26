using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace VotingApplication.Controllers
{
    public class UserController : Controller
    {
        protected SignInManager<ApplicationUser> mSignInManager;
        protected UserManager<ApplicationUser> mUserManager;

        public UserController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            mSignInManager = signInManager;
            mUserManager = userManager;
        }

        public IActionResult Login()
        {
            return View("Login/Index"); //Index view
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            return View("Dashboard/Index"); //Index view
        }

        public IActionResult Registration()
        {
            return View("Registration/Index"); //Index view
        }
        
        public async Task<IActionResult> CreateUserAsync(string username, string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = username,
                Email = email
            };
            
            var result = await mUserManager.CreateAsync(user, password: password);

            if (result.Succeeded)
            {
                await mSignInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction(nameof(Dashboard));
            }

            ViewData["errors"] = result.ToString();

            return View("Registration/Index");
        }

        public async Task<IActionResult> SubmitLoginAsync(string username, string password, string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            var result = await mSignInManager.PasswordSignInAsync(username, password, true, false);

            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(returnUrl))
                    return RedirectToAction(nameof(Dashboard));

                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            return RedirectToAction(nameof(Login));
        }
    }
}
