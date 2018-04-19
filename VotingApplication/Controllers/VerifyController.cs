using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.Controllers
{
    public class VerifyController : Controller
    {
        private ApplicationDbContext _Context;
        private UserManager<ApplicationUser> _UserManager;

        public VerifyController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _Context = context;
            _UserManager = userManager;
        }
        
        [HttpGet]
        // this is not implemented yet
        //[RequireHttps]
        public async Task<IActionResult> VerifyStrongPasswordAsync(string password)
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

            return Json(true);
        }

        [HttpGet]
        // this is not implemented yet
        //[RequireHttps]
        public async Task<IActionResult> VerifyUniqueUserAsync(string username, string email)
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

            return Json(true);
        }
        
        /// <summary>
        /// This verifies that the email is for some user and that the email is confirmed.
        /// </summary>
        /// <param name="email">email that will be verified</param>
        /// <returns>true or error text</returns>
        [HttpGet]
        // this is not implemented yet
        //[RequireHttps]
        public async Task<IActionResult> VerifyEmailExistsAsync(string email)
        {
            var user = await _UserManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json($"Email \"{email}\" is not valid for any user.");
            }
            else if (user.EmailConfirmed == false)
            {
                // TODO: allow the reset password to also confirm their email.
                return Json($"Email \"{email}\" is not confirmed yet, please confirm your email first.");
            }

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> VerifyZipExistsAsync(string zipcode)
        {
            if (int.TryParse(zipcode, out int key))
            {
                var result = await _Context.Zip.FindAsync(key);

                string errors = "";

                if (result == null)
                {
                    errors = "ZipCode does not exist, enter a valid one.";
                    return Json($"{errors}");
                }
            }

            return Json(true);
        }
    }
}
