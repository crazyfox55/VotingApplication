using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using VotingApplication.ViewModels;

namespace VotingApplication.Controllers
{
    public class RegistrationController : Controller
    {

        protected SignInManager<ApplicationUser> _SignInManager;
        protected UserManager<ApplicationUser> _UserManager;

        public RegistrationController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _SignInManager = signInManager;
            _UserManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["Purpose"] = "Create Account";
            ViewData["Submit"] = "Create";

            return View();
        }

        [HttpPost]
        // this is not implemented yet
        //[RequireHttps]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(RegistrationViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["Purpose"] = "Create Account";
            ViewData["Submit"] = "Create";

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                var result = await _UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var fromAddress = new MailAddress("votingnotification6@gmail.com", "Voting System");
                    var toAddress = new MailAddress(model.Email, model.Username);
                    const string fromPassword = "Team6Admin";
                    const string subject = "Welcome Confirmation Email";
                    string body = "Hello " + model.Username;

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

                    await _SignInManager.SignInAsync(user, isPersistent: false);

                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        string action = nameof(UserController.Dashboard);
                        string controller = nameof(UserController);
                        controller = controller.Remove(controller.Length - 10);
                        return RedirectToAction(action, controller);
                    }

                    return Redirect(returnUrl);
                }
            }

            return View("Register", model);
        }

        [HttpGet]
        // this is not implemented yet
        //[RequireHttps]
        public async Task<IActionResult> VerifyPassword(string password)
        {
            if (_UserManager != null)
            {
                string errors = "";

                IdentityResult result;

                foreach (var validator in _UserManager.PasswordValidators)
                {
                    result = await validator.ValidateAsync(_UserManager, null, password);
                    foreach (var error in result.Errors)
                    {
                        errors += error.Description + "\n";
                    }
                }
                if (errors != string.Empty)
                {
                    return Json($"{errors}");
                }
            }

            return Json(true);
        }

        [HttpGet]
        // this is not implemented yet
        //[RequireHttps]
        public async Task<IActionResult> VerifyUser(string username, string email)
        {
            if (_UserManager != null)
            {
                string errorTarget = username == null ? "Email" : "UserName";

                var user = new ApplicationUser
                {
                    UserName = username,
                    Email = email
                };

                string errors = "";

                IdentityResult result;

                foreach (var validator in _UserManager.UserValidators)
                {
                    result = await validator.ValidateAsync(_UserManager, user);

                    foreach (var error in result.Errors)
                    {
                        if (error.Code.Contains(errorTarget))
                            errors += error.Description + "\n";
                    }
                }
                if (errors != string.Empty)
                {
                    return Json($"{errors}");
                }
            }

            return Json(true);
        }
    }
}
