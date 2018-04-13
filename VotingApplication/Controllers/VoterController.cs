using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using VotingApplication.ViewModels;

namespace VotingApplication.Controllers
{
    [Authorize]
    public class VoterController : Controller
    {
        protected UserManager<ApplicationUser> _UserManager;
        protected ApplicationDbContext _Context;

        public VoterController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context
            )
        {
            _UserManager = userManager;
            _Context = context;
        }
        
        [HttpGet]
        public IActionResult Registration()
        {
            var data = _Context.Registration.Find(_UserManager.GetUserId(User));
            var model = new VoterRegistrationViewModel(data);

            return View("Registration/Index", model); //Index view
        }

        [HttpPost]
        public IActionResult Registration(VoterRegistrationViewModel model)
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

            return View("Registration/Index", model);
        }

        [HttpGet]
        public IActionResult Demographics()
        {
            var data = _Context.Demographics.Find(_UserManager.GetUserId(User));
            var model = new DemographicsEntryViewModel(data);

            return View("Registration/DemographicEntry", model); //Index view
        }

        [HttpPost]
        public IActionResult Demographics(DemographicsEntryViewModel model)
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

            return View("Registration/DemographicEntry", model); //Index view
        }
    }
}
