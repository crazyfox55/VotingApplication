using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

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
            ViewData["UserName"] = HttpContext.User.Identity.Name;
            return View("Dashboard/Index"); //Index view
        }

        public IActionResult Registration()
        {
            return View("Registration/Index"); //Index view
        }
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


        // this should be POST or somehting not URL
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
                var fromAddress = new MailAddress("votingnotification6@gmail.com", "Voting System");
                var toAddress = new MailAddress(email, username);
                const string fromPassword = "Team6Admin";
                const string subject = "Welcome Confirmation Email";
                string body = "Hello " + username;

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
                await mSignInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction(nameof(Dashboard));
            }

            ViewData["errors"] = result.ToString();

            return View("Registration/Index");
        }

        // this should be POST or somehting not URL
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

            ViewData["errors"] = result.ToString();

            return View("Login/Index");
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            return RedirectToAction(nameof(Login));
        }
    }
}
