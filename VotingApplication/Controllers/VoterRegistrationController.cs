using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using VotingApplication.ViewModels;
using System.Threading.Tasks;

namespace VotingApplication.Controllers
{
    [Authorize(Roles = "GenericUser")]
    public class VoterRegistrationController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ApplicationDbContext context
            ) : Controller
    {
        protected UserManager<ApplicationUser> _userManager = userManager;
        protected SignInManager<ApplicationUser> _signInManager = signInManager;
        protected ApplicationDbContext _context = context;

        [HttpGet]
        public IActionResult Dashboard()
        {
            string userId = _userManager.GetUserId(User);
            VoterRegistrationDataModel registrationData = _context.Registration.Find(userId);
            VoterAddressDataModel addressData = _context.Address.Find(userId);
            VoterDemographicsDataModel demographicsData = _context.Demographics.Find(userId);
            VoterDashboardViewModel model = new(
                registrationData != null, 
                addressData != null,
                demographicsData != null);

            return View(model);
        }

        [HttpGet]
        public IActionResult FinalizeRegistration()
        {
            string userId = _userManager.GetUserId(User);
            VoterRegistrationDataModel registrationData = _context.Registration.Find(userId);
            VoterAddressDataModel addressData = _context.Address.Find(userId);
            VoterDemographicsDataModel demographicsData = _context.Demographics.Find(userId);

            VoterFinalizeRegistrationViewModel model = new(registrationData, addressData, demographicsData);

            if (registrationData == null || addressData == null || demographicsData == null)
                // TODO change to some error page -- not done with other registration
                return View(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FinalizeRegistration(VoterFinalizeRegistrationViewModel model)
        {
            string userId = _userManager.GetUserId(User);
            VoterRegistrationDataModel registrationData = _context.Registration.Find(userId);
            VoterAddressDataModel addressData = _context.Address.Find(userId);
            VoterDemographicsDataModel demographicsData = _context.Demographics.Find(userId);

            if (registrationData == null || addressData == null || demographicsData == null)
                // TODO change to some error page -- not done with other registration
                return RedirectToAction(nameof(Dashboard));

            ApplicationUser user = await _userManager.GetUserAsync(User);
            _userManager.AddToRoleAsync(user, "RegisteredVoter").Wait();
            _userManager.RemoveFromRoleAsync(user, "GenericUser").Wait();

            // this is needed to update the cookie so it has the correct roles
            // otherwise the user will sill have access as a genericUser.
            _signInManager.RefreshSignInAsync(user).Wait();

            return RedirectToAction(nameof(VoterController.Dashboard), nameof(VoterController).RemoveController());
        }

        [HttpGet]
        public IActionResult Register()
        {
            VoterRegistrationDataModel data = _context.Registration.Find(_userManager.GetUserId(User));
            VoterRegistrationViewModel model = new(data);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(VoterRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = _userManager.GetUserId(User);
                VoterRegistrationDataModel registration = await _context.Registration.FindAsync(userId);
                if (registration == null)
                {
                    registration = new VoterRegistrationDataModel(userId, model);

                    _context.Registration.Add(registration);
                }
                else
                {
                    registration.Update(model);

                    _context.Registration.Update(registration);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Dashboard));
            }

            return View("Register", model);
        }

        [HttpGet]
        public IActionResult Address()
        {
            VoterAddressDataModel data = _context.Address.Find(_userManager.GetUserId(User));
            VoterAddressViewModel model = new(data);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Address(VoterAddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = _userManager.GetUserId(User);
                VoterAddressDataModel address = await _context.Address.FindAsync(userId);
                if (address == null)
                {
                    address = new VoterAddressDataModel(userId, model);

                    _context.Address.Add(address);
                }
                else
                {
                    address.Update(model);

                    _context.Address.Update(address);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Dashboard));
            }

            return View("Address",  model);
        }

        [HttpGet]
        public IActionResult Demographics()
        {
            VoterDemographicsDataModel data = _context.Demographics.Find(_userManager.GetUserId(User));
            VoterDemographicsViewModel model = new(data);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Demographics(VoterDemographicsViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = _userManager.GetUserId(User);
                VoterDemographicsDataModel demographics = await _context.Demographics.FindAsync(userId);
                if (demographics == null)
                {
                    demographics = new VoterDemographicsDataModel(userId, model);

                    _context.Demographics.Add(demographics);
                }
                else
                {
                    demographics.Update(model);

                    _context.Demographics.Update(demographics);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Dashboard));
            }

            return View("Demographics", model); //Index view
        }

        
    }
}
