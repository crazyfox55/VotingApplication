using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VotingApplication.Services;
using VotingApplication.ViewModels;

namespace VotingApplication.Controllers
{
    [AllowAnonymous]
    public class UserRegistrationController : Controller
    {
        protected UserManager<ApplicationUser> _UserManager;
        protected IEmailService _EmailService;

        public UserRegistrationController(
            UserManager<ApplicationUser> userManager,
            IEmailService emailService)
        {
            _UserManager = userManager;
            _EmailService = emailService;
        }

        #region Register User
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["Purpose"] = "Create Account";
            ViewData["Submit"] = "Create";

            return View();
        }

        [HttpPost]
        // this is not implemented yet
        //[RequireHttps]
        public async Task<IActionResult> RegisterAsync(RegistrationViewModel model)
        {
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
                    // send email confirmation.
                    // message setup.
                    string confirmationToken = await _UserManager.GenerateEmailConfirmationTokenAsync(user);

                    string action = nameof(EmailConfirmedAsync);
                    string controller = nameof(UserRegistrationController).RemoveController();
                    string confirmationTokenLink = Url.Action(action, controller, new
                    {
                        username = user.UserName,
                        token = confirmationToken
                    }, protocol: HttpContext.Request.Scheme);

                    string subject = "Welcome, Confirmation Email";
                    string body = $"Hello {user.UserName}.\nPlease confirm your email address by following this <a href=\"{confirmationTokenLink}\">link</a>.\nThank you.";

                    // actual send
                    await _EmailService.SendEmailAsync(user, subject, body);
                    
                    return View("EmailConfirmationSent");
                }
            }

            return View("Register", model);
        }
        #endregion
        
        #region Confirmation Email: Affirm, Request, Resend
        [HttpGet]
        public async Task<IActionResult> EmailConfirmedAsync(string username, string token)
        {
            if (username != null && token != null)
            {
                var user = await _UserManager.FindByNameAsync(username);
                if (user != null)
                {
                    var confirm = await _UserManager.ConfirmEmailAsync(user, token);
                    if (confirm.Succeeded)
                    {
                        return View("EmailConfirmed");
                    }
                }
            }
            
            return View("EmailConfirmationError");
        }

        [HttpGet]
        public IActionResult RequestEmailConfirmation()
        {
            return View("EmailConfirmationRequest");
        }

        [HttpPost]
        public async Task<IActionResult> EmailConfirmationSentAsync(EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByEmailAsync(model.Email);

                // send email confirmation.
                // message setup.
                string confirmationToken = await _UserManager.GenerateEmailConfirmationTokenAsync(user);
                string action = nameof(EmailConfirmedAsync);
                string controller = nameof(UserRegistrationController).RemoveController();
                string confirmationTokenLink = Url.Action(action, controller, new
                {
                    username = user.UserName,
                    token = confirmationToken
                }, protocol: HttpContext.Request.Scheme);

                string subject = "Welcome, Confirmation Email";
                string body = $"Hello {user.UserName}.\nPlease confirm your email address by following this <a href=\"{confirmationTokenLink}\">link</a>.\nThank you.";

                // actual send
                await _EmailService.SendEmailAsync(user, subject, body);
                
                return View("EmailConfirmationSent", model);
            }

            return View("EmailConfirmationError", model);
        }
        #endregion
    }
}
