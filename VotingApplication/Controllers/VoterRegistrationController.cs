using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using VotingApplication.ViewModels;

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
        public VoterController() { }
        
        [HttpGet]
        public IActionResult Register()
        {
            var data = _Context.Registration.Find(_UserManager.GetUserId(User));
            var model = new VoterRegistrationViewModel(data);

            return View(model);
        }

        [HttpPost]
        public IActionResult Register(VoterRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var registration = _Context.Registration.Find(_UserManager.GetUserId(User));
                if (registration == null)
                {
                    registration = new VoterRegistrationDataModel(_UserManager.GetUserId(User), model);

                    _Context.Registration.Add(registration);
                }
                else
                {
                    registration.Update(model);

                    _Context.Registration.Update(registration);
                }

                _Context.SaveChanges();

                return RedirectToAction("Dashboard", "User");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AddDemographics()
        {
            var data = _Context.Demographics.Find(_UserManager.GetUserId(User));
            var model = new DemographicsEntryViewModel(data);

            return View(model);
        }

        [HttpPost]
        public IActionResult AddDemographics(DemographicsEntryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var demographics = _Context.Demographics.Find(_UserManager.GetUserId(User));
                if (demographics == null)
                {
                    demographics = new VoterDemographicsDataModel(_UserManager.GetUserId(User), model);

                    _Context.Demographics.Add(demographics);
                }
                else
                {
                    demographics.Update(model);

                    _Context.Demographics.Update(demographics);
                }

                _Context.SaveChanges();

                return RedirectToAction("Dashboard", "User");
            }

            return View(model); //Index view
        }
    }
}
