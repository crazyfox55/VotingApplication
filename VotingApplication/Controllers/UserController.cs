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
    public class UserController : Controller
    {
        protected SignInManager<ApplicationUser> _SignInManager;
        protected UserManager<ApplicationUser> _UserManager;

        public UserController(
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

        [Authorize]
        public IActionResult Profile()
        {
            ViewData["UserName"] = HttpContext.User.Identity.Name;
            return View("Profile/Index"); //Index view
        }

        public IActionResult ChangePassword()
        {
            return View("Profile/ChangePassword");
        }

        public IActionResult AddSecurityQuestions()
        {
            return View("Profile/SecurityQuestions");
        }

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

        #region Registration
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View("Registration/Index"); //Index view
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(RegistrationViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

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

                    return RedirectToAction(nameof(Dashboard));
                }
                //AddErrors(result);
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Code);
                }
                //ViewData["errors"] = result.ToString();
            }
            
            return View("Registration/Index", model);
        }

        [AcceptVerbs("Get", "Post")]
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

        [AcceptVerbs("Get", "Post")]
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
                    foreach(var error in result.Errors)
                    {
                        if(error.Code.Contains(errorTarget))
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

        #region Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View("Login/Index"); //Index view
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            if (ModelState.IsValid)
            {
                var result = await _SignInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: true, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction(nameof(Dashboard));

                    return Redirect(returnUrl);
                }

                ViewData["errors"] = result.ToString();
            }

            return View("Login/Index", model);
        }
        #endregion

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            return RedirectToAction(nameof(Login));
        }
    }
}
