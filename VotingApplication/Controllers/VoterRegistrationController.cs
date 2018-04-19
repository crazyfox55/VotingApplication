using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using VotingApplication.ViewModels;
using System.Threading.Tasks;

namespace VotingApplication.Controllers
{
    [Authorize(Roles = "GenericUser")]
    public class VoterRegistrationController : Controller
    {
        protected UserManager<ApplicationUser> _UserManager;
        protected SignInManager<ApplicationUser> _SignInManager;
        protected ApplicationDbContext _Context;

        public VoterRegistrationController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context
            )
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _Context = context;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            string userId = _UserManager.GetUserId(User);
            VoterRegistrationDataModel registrationData = _Context.Registration.Find(userId);
            VoterAddressDataModel addressData = _Context.Address.Find(userId);
            VoterDemographicsDataModel demographicsData = _Context.Demographics.Find(userId);
            VoterDashboardViewModel model = new VoterDashboardViewModel(
                registrationData != null, 
                addressData != null,
                demographicsData != null);

            return View(model);
        }

        [HttpGet]
        public IActionResult FinalizeRegistration()
        {
            string userId = _UserManager.GetUserId(User);
            VoterRegistrationDataModel registrationData = _Context.Registration.Find(userId);
            VoterAddressDataModel addressData = _Context.Address.Find(userId);
            VoterDemographicsDataModel demographicsData = _Context.Demographics.Find(userId);

            VoterFinalizeRegistrationViewModel model = new VoterFinalizeRegistrationViewModel(registrationData, addressData, demographicsData);

            if (registrationData == null || addressData == null || demographicsData == null)
                // TODO change to some error page -- not done with other registration
                return View(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FinalizeRegistrationAsync(VoterFinalizeRegistrationViewModel model)
        {
            string userId = _UserManager.GetUserId(User);
            VoterRegistrationDataModel registrationData = _Context.Registration.Find(userId);
            VoterAddressDataModel addressData = _Context.Address.Find(userId);
            VoterDemographicsDataModel demographicsData = _Context.Demographics.Find(userId);

            if (registrationData == null || addressData == null || demographicsData == null)
                // TODO change to some error page -- not done with other registration
                return RedirectToAction(nameof(Dashboard));

            ApplicationUser user = await _UserManager.GetUserAsync(User);
            _UserManager.AddToRoleAsync(user, "RegisteredVoter").Wait();
            _UserManager.RemoveFromRoleAsync(user, "GenericUser").Wait();

            // this is needed to update the cookie so it has the correct roles
            // otherwise the user will sill have access as a genericUser.
            _SignInManager.RefreshSignInAsync(user).Wait();

            return RedirectToAction("Profile", "User");
        }

        [HttpGet]
        public IActionResult Register()
        {
            VoterRegistrationDataModel data = _Context.Registration.Find(_UserManager.GetUserId(User));
            VoterRegistrationViewModel model = new VoterRegistrationViewModel(data);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(VoterRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = _UserManager.GetUserId(User);
                VoterRegistrationDataModel registration = await _Context.Registration.FindAsync(userId);
                if (registration == null)
                {
                    registration = new VoterRegistrationDataModel(userId, model);

                    _Context.Registration.Add(registration);
                }
                else
                {
                    registration.Update(model);

                    _Context.Registration.Update(registration);
                }

                await _Context.SaveChangesAsync();

                return RedirectToAction(nameof(Dashboard));
            }

            return View("Register", model);
        }

        [HttpGet]
        public IActionResult Address()
        {
            VoterAddressDataModel data = _Context.Address.Find(_UserManager.GetUserId(User));
            VoterAddressViewModel model = new VoterAddressViewModel(data);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddressAsync(VoterAddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = _UserManager.GetUserId(User);
                VoterAddressDataModel address = await _Context.Address.FindAsync(userId);
                if (address == null)
                {
                    address = new VoterAddressDataModel(userId, model);

                    _Context.Address.Add(address);
                }
                else
                {
                    address.Update(model);

                    _Context.Address.Update(address);
                }

                await _Context.SaveChangesAsync();

                return RedirectToAction(nameof(Dashboard));
            }

            return View("Address",  model);
        }

        [HttpGet]
        public IActionResult Demographics()
        {
            VoterDemographicsDataModel data = _Context.Demographics.Find(_UserManager.GetUserId(User));
            VoterDemographicsViewModel model = new VoterDemographicsViewModel(data);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DemographicsAsync(VoterDemographicsViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = _UserManager.GetUserId(User);
                VoterDemographicsDataModel demographics = await _Context.Demographics.FindAsync(userId);
                if (demographics == null)
                {
                    demographics = new VoterDemographicsDataModel(userId, model);

                    _Context.Demographics.Add(demographics);
                }
                else
                {
                    demographics.Update(model);

                    _Context.Demographics.Update(demographics);
                }

                await _Context.SaveChangesAsync();

                return RedirectToAction(nameof(Dashboard));
            }

            return View("Demographics", model); //Index view
        }

        
    }
}
