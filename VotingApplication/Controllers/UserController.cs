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

        public UserController(
            UserManager<ApplicationUser> userManager)
        {
            _UserManager = userManager;
        }
        
        [HttpGet]
        public IActionResult Dashboard()
        {
            ViewData["UserName"] = HttpContext.User.Identity.Name;
            return View("Dashboard/Index"); //Index view
        }

        [HttpGet]
        public IActionResult Profile()
        {
            ViewData["UserName"] = HttpContext.User.Identity.Name;
            return View("Profile/Index"); //Index view
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View("Profile/ChangePassword");
        }

        [HttpGet]
        public async Task<IActionResult> AddSecurityQuestionsAsync()
        {
            var user = await _UserManager.FindByNameAsync(User.Identity.Name);
            var model = new SecurityQuestionViewModel
            {
                SecurityQuestionOne = user.SecurityQuestionOne,
                SecurityQuestionTwo = user.SecurityQuestionTwo,
                SecurityAnswerOne = user.SecurityAnswerOne,
                SecurityAnswerTwo = user.SecurityAnswerTwo
            };

            return View("Profile/SecurityQuestions", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddSecurityQuestionsAsync(SecurityQuestionViewModel model)
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

            return View("Profile/SecurityQuestions", model);
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View("ForgotPassword/Index"); //Index view
        }

        //needs to be finished
        [HttpPost]
        public IActionResult SendResetEmail(string emailInput)
        {
           
            var fromAddress = new MailAddress("votingnotification6@gmail.com", "Voting System");
            var toAddress = new MailAddress("cole-pierce@uiowa.edu", emailInput);
            if (emailInput != null)
            {
                toAddress = new MailAddress(emailInput, emailInput);
            }
                
            const string fromPassword = "Team6Admin";
                const string subject = "Reset Password";
                string body = "Reset link";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            return View("ForgotPassword/CheckEmail");
        }
    }
}
