using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using VotingApplication.ViewModels;
using System.Collections;

namespace VotingApplication.Controllers
{
    public class AdminController : Controller
    {
        protected SignInManager<ApplicationUser> _SignInManager;
        protected UserManager<ApplicationUser> _UserManager;

        
        public AdminController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _SignInManager = signInManager;
            _UserManager = userManager;
        }
        
        
        [Authorize]
        public IActionResult Dashboard()
        {
            ViewData["UserName"] = HttpContext.User.Identity.Name;
            return View("Dashboard/Index"); //Index view
        }

        public IActionResult UserManagement()
        {
            ViewData["UserName"] = HttpContext.User.Identity.Name;
            return View("UserManagement/Index");
        }

        public IActionResult UserManagement2()
        {
            ViewData["UserName"] = HttpContext.User.Identity.Name;
            return View("UserManagement2/Index");
        }
    }
}
