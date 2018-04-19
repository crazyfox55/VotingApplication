using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using VotingApplication.ViewModels;

namespace VotingApplication.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        protected UserManager<ApplicationUser> _UserManager;
        protected ApplicationDbContext _Context;

        public UserController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _UserManager = userManager;
            _Context = context;
        }
        
        [HttpGet]
        public IActionResult Profile()
        {
            ViewData["UserName"] = HttpContext.User.Identity.Name;
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByNameAsync(User.Identity.Name);
                var authenticate = await _UserManager.CheckPasswordAsync(user, model.OldPassword);
                if (authenticate)
                {
                    var result = await _UserManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Profile));
                    }
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid password.");
                }
            }

            return View("ChangePassword", model);
        }
        
        [HttpGet]
        public async Task<IActionResult> SecurityQuestionsAsync()
        {
            var user = await _UserManager.FindByNameAsync(User.Identity.Name);
            var model = new SecurityQuestionViewModel
            {
                SecurityQuestionOne = user.SecurityQuestionOne,
                SecurityQuestionTwo = user.SecurityQuestionTwo,
                SecurityAnswerOne = user.SecurityAnswerOne,
                SecurityAnswerTwo = user.SecurityAnswerTwo
            };

            return View("SecurityQuestions", model);
        }

        [HttpPost]
        public async Task<IActionResult> SecurityQuestionsAsync(SecurityQuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                // get the current user
                var user = await _UserManager.FindByNameAsync(User.Identity.Name);

                user.SecurityQuestionOne = model.SecurityQuestionOne;
                user.SecurityQuestionTwo = model.SecurityQuestionTwo;
                user.SecurityAnswerOne = model.SecurityAnswerOne;
                user.SecurityAnswerTwo = model.SecurityAnswerTwo;

                // update the database
                var result = await _UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    // TODO: show that the security questions were saved then redirect
                    return RedirectToAction(nameof(Profile));
                }

                foreach (IdentityError error in result.Errors)
                {
                    // these errors can be displayed in the web page by adding:
                    /* <div class="text-danger">
                     *      @Html.ValidationSummary()
                     * </div>
                     */
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View("SecurityQuestions", model);
        }
    }
}
