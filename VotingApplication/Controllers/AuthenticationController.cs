﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VotingApplication.ViewModels;

namespace VotingApplication.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        protected SignInManager<ApplicationUser> _SignInManager;

        public AuthenticationController(
            SignInManager<ApplicationUser> signInManager)
        {
            _SignInManager = signInManager;
        }

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

        [HttpGet]
        public async Task<IActionResult> LogoutAsync()
        {
            await _SignInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }
    }
}
