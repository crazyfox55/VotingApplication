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
        public IActionResult Register()
        {
            ViewData["Purpose"] = "Create Account";
            ViewData["Submit"] = "Create";

            return View();
        }

        [HttpPost]
        // this is not implemented yet
        //[RequireHttps]
        [AllowAnonymous]
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
                    await SendConfirmationEmailAsync(user);

                    return View("EmailConfirmationSent");
                }
            }

            return View("Register", model);
        }

        #region Verify Registration View Model
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
        #endregion

        //[AcceptVerbs("Get", "Post")]
        [HttpGet]
        [AllowAnonymous]
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
                    else
                    {
                        foreach (IdentityError error in confirm.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid user.");
                }
            }
            
            return View("EmailConfirmationSent", new ConfirmEmailViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> EmailConfirmationSentAsync(ConfirmEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByEmailAsync(model.Email);

                await SendConfirmationEmailAsync(user);
                
                return View("EmailConfirmationSent");
            }
            
            return View("EmailConfirmationSent", model);
        }
        
        private async Task SendConfirmationEmailAsync(ApplicationUser user)
        {
            if (_UserManager != null && user != null)
            {
                string confirmationToken = await _UserManager.GenerateEmailConfirmationTokenAsync(user);
                const string action = nameof(EmailConfirmedAsync);
                string controller = nameof(RegistrationController);
                controller = controller.Remove(controller.Length - 10);
                string confirmationTokenLink = Url.Action(action, controller, new
                {
                    username = user.UserName,
                    token = confirmationToken
                }, protocol: HttpContext.Request.Scheme);

                MailAddress fromAddress = new MailAddress("votingnotification6@gmail.com", "Voting Online");
                MailAddress toAddress = new MailAddress(user.Email, user.UserName);
                const string fromPassword = "Team6Admin";
                const string subject = "Welcome, Confirmation Email";
                string body = $"Hello {user.UserName}.\nPlease confirm your email address by following this <a href=\"{confirmationTokenLink}\">link</a>.\nThank you.";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                var message = new MailMessage
                {
                    From = fromAddress,
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                message.To.Add(toAddress);
                await smtp.SendMailAsync(message);
            }
        }

        [HttpGet]
        // this is not implemented yet
        //[RequireHttps]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            if (_UserManager != null)
            {
                var user = await _UserManager.FindByEmailAsync(email);
                
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

    }
}
