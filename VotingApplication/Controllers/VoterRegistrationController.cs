using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using VotingApplication.ViewModels;
using System.Threading.Tasks;

namespace VotingApplication.Controllers
{
    [Authorize]
    public class VoterRegistrationController : Controller
    {
        protected UserManager<ApplicationUser> _UserManager;
        protected ApplicationDbContext _Context;

        public VoterRegistrationController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context
            )
        {
            _UserManager = userManager;
            _Context = context;
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

                return RedirectToAction("Dashboard", "User");
            }

            return View(model);
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

                return RedirectToAction("Dashboard", "User");
            }

            return View(model);
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

                return RedirectToAction("Dashboard", "User");
            }

            return View(model); //Index view
        }
    }
}
