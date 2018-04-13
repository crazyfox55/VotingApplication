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
        
        protected UserManager<ApplicationUser> _userManager;
        protected IEmailService _emailService;

        public UserRegistrationController(
            UserManager<ApplicationUser> userManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }
        public RegistrationController() { }

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

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // send email confirmation.
                    // message setup.
                    string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    const string action = nameof(EmailConfirmedAsync);
                    string controller = nameof(UserRegistrationController);
                    controller = controller.Remove(controller.Length - 10);
                    string confirmationTokenLink = Url.Action(action, controller, new
                    {
                        username = user.UserName,
                        token = confirmationToken
                    }, protocol: HttpContext.Request.Scheme);

                    const string subject = "Welcome, Confirmation Email";
                    string body = $"Hello {user.UserName}.\nPlease confirm your email address by following this <a href=\"{confirmationTokenLink}\">link</a>.\nThank you.";

                    // actual send
                    await _emailService.SendEmailAsync(user, subject, body);

                    var modelEmail = new ConfirmEmailViewModel();
                    modelEmail.State = ConfirmEmailViewModel.Status.Sent;
                    return View("EmailConfirmationSent", modelEmail);
                }
            }

            return View("Register", model);
        }
        #endregion

        #region Verify Registration View Model
        [HttpGet]
        // this is not implemented yet
        //[RequireHttps]
        public async Task<IActionResult> VerifyPassword(string password)
        {
            if (_userManager != null)
            {
                string errors = "";

                IdentityResult result;

                foreach (var validator in _userManager.PasswordValidators)
                {
                    result = await validator.ValidateAsync(_userManager, null, password);
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
            if (_userManager != null)
            {
                string errorTarget = username == null ? "Email" : "UserName";

                var user = new ApplicationUser
                {
                    UserName = username,
                    Email = email
                };

                string errors = "";

                IdentityResult result;

                foreach (var validator in _userManager.UserValidators)
                {
                    result = await validator.ValidateAsync(_userManager, user);

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
        #endregion

        #region Confirmation Email: Affirm, Request, Resend
        [HttpGet]
        public async Task<IActionResult> EmailConfirmedAsync(string username, string token)
        {
            if (username != null && token != null)
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user != null)
                {
                    var confirm = await _userManager.ConfirmEmailAsync(user, token);
                    if (confirm.Succeeded)
                    {
                        return View("EmailConfirmed");
                    }
                }
            }

            var model = new ConfirmEmailViewModel();
            model.State = ConfirmEmailViewModel.Status.Error;
            return View("EmailConfirmationSent", model);
        }

        [HttpGet]
        public IActionResult RequestEmailConfirmation()
        {
            var model = new ConfirmEmailViewModel();
            model.State = ConfirmEmailViewModel.Status.Request;
            return View("EmailConfirmationSent", model);
        }

        [HttpPost]
        public async Task<IActionResult> EmailConfirmationSentAsync(ConfirmEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                // send email confirmation.
                // message setup.
                string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                const string action = nameof(EmailConfirmedAsync);
                string controller = nameof(UserRegistrationController);
                controller = controller.Remove(controller.Length - 10);
                string confirmationTokenLink = Url.Action(action, controller, new
                {
                    username = user.UserName,
                    token = confirmationToken
                }, protocol: HttpContext.Request.Scheme);

                const string subject = "Welcome, Confirmation Email";
                string body = $"Hello {user.UserName}.\nPlease confirm your email address by following this <a href=\"{confirmationTokenLink}\">link</a>.\nThank you.";

                // actual send
                await _emailService.SendEmailAsync(user, subject, body);

                model.Email = "";
                model.State = ConfirmEmailViewModel.Status.Sent;
                return View("EmailConfirmationSent", model);
            }

            return View("EmailConfirmationSent", model);
        }
        #endregion

        #region Verify Confirm Email View Model
        [HttpGet]
        // this is not implemented yet
        //[RequireHttps]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            if (_userManager != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                
                if(user == null)
                {
                    return Json($"Email: {email} is not valid for any user.");
                }
                else if (user.EmailConfirmed)
                {
                    return Json($"Email: {email} is already confirmed.");
                }
            }

            return Json(true);
        }
        #endregion
    }
}
