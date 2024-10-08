﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VotingApplication.Services;
using VotingApplication.ViewModels;

namespace VotingApplication.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IEmailService emailService) : Controller
    {
        protected SignInManager<ApplicationUser> _signInManager = signInManager;
        protected UserManager<ApplicationUser> _userManager = userManager;
        protected IEmailService _emailService = emailService;

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
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["Purpose"] = "User Login";
            ViewData["Submit"] = "Login";
            
            await _signInManager.SignOutAsync();

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        string action = "";
                        string controller = "";
                        var user = await _userManager.FindByNameAsync(model.Username);
                        if (await _userManager.IsInRoleAsync(user, "Administrator"))
                        {
                            action = nameof(AdminController.Dashboard);
                            controller = nameof(AdminController).RemoveController();
                        }
                        else if(await _userManager.IsInRoleAsync(user, "GenericUser"))
                        {
                            action = nameof(VoterRegistrationController.Dashboard);
                            controller = nameof(VoterRegistrationController).RemoveController();
                        }
                        else
                        {
                            action = nameof(VoterController.Dashboard);
                            controller = nameof(VoterController).RemoveController();
                        }
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
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

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
        public async Task<IActionResult> ResetPassword(string username, string token)
        {
            if (username != null && token != null)
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user != null)
                {
                    var temp = new ResetPasswordViewModel
                    {
                        Token = token,
                        Email = user.Email
                    };
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
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    string emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    await _userManager.ConfirmEmailAsync(user, emailToken);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordComplete");
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
        public IActionResult RequestResetPasswordEmail(EmailViewModel model)
        {
            return View("ResetPasswordEmailRequest", model);
        }

        /// <summary>
        /// Handles the submission of a email to reset the password of a user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SendResetPasswordEmail(EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                
                // message setup.
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                string action = nameof(ResetPassword);
                string controller = nameof(AuthenticationController).RemoveController();
                string resetTokenLink = Url.Action(action, controller, new
                {
                    username = user.UserName,
                    token = resetToken
                }, protocol: HttpContext.Request.Scheme);

                string subject = "Reset Password";
                string body = $"Hello {user.UserName}.\nYou can reset your password by following this <a href=\"{resetTokenLink}\">link</a>.";

                // actual send
                await _emailService.SendEmailAsync(user, subject, body);
                
                return View("ResetPasswordEmailSent", model);
            }

            return View("ResetPasswordRequest", model);
        }
        #endregion
    }
}
