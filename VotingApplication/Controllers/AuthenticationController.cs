using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VotingApplication.Services;
using VotingApplication.ViewModels;

namespace VotingApplication.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        protected SignInManager<ApplicationUser> _SignInManager;
        protected UserManager<ApplicationUser> _userManager;
        protected IEmailService _emailService;

        public AuthenticationController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IEmailService emailService)
        {
            _SignInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }

        #region Login User
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["Purpose"] = "User Login";
            ViewData["Submit"] = "Login";

            return View();
        }

        [HttpPost]
        // this is not implemented yet
        //[RequireHttps]
        public async Task<IActionResult> LoginAsync(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["Purpose"] = "User Login";
            ViewData["Submit"] = "Login";
            
            await _SignInManager.SignOutAsync();

            if (ModelState.IsValid)
            {
                var result = await _SignInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        string action = nameof(UserController.Dashboard);
                        string controller = nameof(UserController);
                        controller = controller.Remove(controller.Length - 10);
                        return RedirectToAction(action, controller);
                    }

                    return Redirect(returnUrl);
                }
                else
                {
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "Account is locked out.");
                    }
                    else if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError(string.Empty, "Email confirmation required.");
                    }
                    else if (result.RequiresTwoFactor)
                    {
                        ModelState.AddModelError(string.Empty, "Two factor authentication required.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    }
                }
            }
            
            return View("Login", model);
        }
        #endregion

        #region Logout User
        [HttpGet]
        public async Task<IActionResult> LogoutAsync()
        {
            await _SignInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }
        #endregion

        // This implementation passes trust to the email provider.
        // Users that have corupted emails will be at risk.
        #region Reset Password
        /// <summary>
        /// This method is invoked by users who click the link sent to their email. Otherwise it should not be accessed.
        /// </summary>
        /// <param name="username">The user account with the link.</param>
        /// <param name="token">The token to verify the password change.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ResetPasswordAsync(string username, string token)
        {
            if (username != null && token != null)
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user != null)
                {
                    var temp = new ResetPasswordViewModel();
                    temp.Token = token;
                    temp.Email = user.Email;
                    return View("ResetPassword", temp);
                }
            }
            
            return View("ResetPasswordEmailError");
        }

        /// <summary>
        /// Handles the submission of a new password for the given user account.
        /// </summary>
        /// <param name="model">Contains the new password, the users email, and the token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmed");
                    }
                }
                
                return View("ResetPasswordEmailError");
            }
            
            return View("ResetPassword", model);
        }

        /// <summary>
        /// Displays a webpage with a form to submit email
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RequestResetPasswordEmail()
        {
            return View("ResetPasswordEmailRequest");
        }

        [HttpPost]
        public IActionResult RequestResetPasswordEmail(ResetPasswordEmailViewModel model)
        {
            return View("ResetPasswordEmailRequest", model);
        }

        /// <summary>
        /// Handles the submission of a email to reset the password of a user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SendResetPasswordEmailAsync(ResetPasswordEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                // send email confirmation.
                // message setup.
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                string action = nameof(ResetPasswordAsync);
                string controller = nameof(AuthenticationController);
                controller = controller.Remove(controller.Length - 10);
                string resetTokenLink = Url.Action(action, controller, new
                {
                    username = user.UserName,
                    token = resetToken
                }, protocol: HttpContext.Request.Scheme);

                const string subject = "Reset Password";
                string body = $"Hello {user.UserName}.\nYou can reset your password by following this <a href=\"{resetTokenLink}\">link</a>.";

                // actual send
                await _emailService.SendEmailAsync(user, subject, body);
                
                return View("ResetPasswordEmailSent", model);
            }

            return View("ResetPasswordRequest", model);
        }
        #endregion

        #region Verify - Confirm Reset Password View Model
        /// <summary>
        /// This verifies that the email is for some user and that the email is confirmed.
        /// </summary>
        /// <param name="email">email that will be verified</param>
        /// <returns>true or error text</returns>
        [HttpGet]
        // this is not implemented yet
        //[RequireHttps]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            if (_userManager != null)
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    return Json($"Email: {email} is not valid for any user.");
                }
                else if (user.EmailConfirmed == false)
                {
                    // TODO: allow the reset password to also confirm their email.
                    return Json($"Email: {email} is not confirmed yet, please confirm your email first.");
                }
            }

            return Json(true);
        }
        #endregion
    }
}
