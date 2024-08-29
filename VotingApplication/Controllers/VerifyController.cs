using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.Controllers
{
    public class VerifyController(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager) : Controller
    {
        private ApplicationDbContext _context = context;
        private UserManager<ApplicationUser> _userManager = userManager;

        [HttpGet]
        // this is not implemented yet
        //[RequireHttps]
        public async Task<IActionResult> VerifyStrongPassword(string password)
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

            return Json(true);
        }

        [HttpGet]
        // this is not implemented yet
        //[RequireHttps]
        public async Task<IActionResult> VerifyUniqueUser(string username, string email, string prevUsername = null)
        {
            if (username != null && prevUsername == username)
                return Json(true);

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
        public async Task<IActionResult> VerifyEmailExists(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json($"Email \"{email}\" is not valid for any user.");
            }

            return Json(true);
        }

        [HttpGet]
        // this is not implemented yet
        //[RequireHttps]
        public async Task<IActionResult> VerifyUserExists(string username)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return Json($"User \"{username}\" is not valid for any user.");
            }

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> VerifyZipExists(string zipcode)
        {
            ZipDataModel result = null;

            if (int.TryParse(zipcode, out int key))
            {
                result = await _context.Zip.FindAsync(key);
            }

            if (result == null)
            {
                return Json($"ZipCode \"{zipcode}\" does not exist, enter a valid one.");
            }

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> VerifyDistrictExists(string districtName)
        {
            DistrictDataModel result = await _context.District.FindAsync(districtName);

            if (result == null)
            {
                return Json($"District \"{districtName}\" does not exist, enter a valid one.");
            }

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> VerifyRegionExistsAsync(string regionName)
        {
            RegionDataModel result = await _context.Region.FindAsync(regionName);

            if (result == null)
            {
                return Json($"Region \"{regionName}\" does not exist, enter a valid one.");
            }

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> VerifyBallotExistsAsync(string ballotName)
        {
            BallotDataModel result = await _context.Ballot.FindAsync(ballotName);
            
            if (result == null)
            {
                return Json($"Ballot \"{ballotName}\"does not exist, enter a valid one.");
            }

            return Json(true);
        }
    }
}
