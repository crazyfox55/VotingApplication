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
            return View("Registration/Index"); //Index view
        }

        [HttpPost]
        public IActionResult Registration(VoterRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var registration = new VoterRegistrationDataModel()
                {
                    UserId = _UserManager.GetUserId(User),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Identification = model.Identification,
                    SSNumber = model.SSNumber
                };

                _Context.Registration.Add(registration);

                _Context.SaveChanges();

                return RedirectToAction("Dashboard", "User");
            }

            return View("Registration/Index", model);
        }

        [HttpGet]
        public IActionResult Demographics()
        {
            return View("Registration/DemographicEntry"); //Index view
        }

        [HttpPost]
        public IActionResult Demographics(DemographicsEntryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var demographics = new VoterDemographicsDataModel()
                {
                    UserId = _UserManager.GetUserId(User),
                    AddressLineOne = model.AddressLineOne,
                    AddressLineTwo = model.AddressLineTwo,
                    City = model.City,
                    ZipCode = model.ZipCode,
                    State = model.State,
                    DOB = model.DOB,
                    Party = model.Party,
                    Ethnicity = model.Ethnicity,
                    Sex = model.Sex,
                    IncomeRange = model.IncomeRange,
                    VoterReadiness = model.VoterReadiness
                };

                _Context.Demographics.Add(demographics);

                _Context.SaveChanges();

                return RedirectToAction("Dashboard", "User");
            }

            return View("Registration/DemographicEntry", model); //Index view
        }
    }
}
