using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VotingApplication.ViewModels;

namespace VotingApplication.Controllers
{
    [Authorize]
    public class VoterController : Controller
    {
        protected ApplicationDbContext _Context;

        public VoterController(ApplicationDbContext context)
        {
            _Context = context;
        }
        
        [HttpGet]
        public IActionResult Registration()
        {
            return View("Registration/Index"); //Index view
        }
        [HttpGet]
        public IActionResult Demographics()
        {
            return View("Registration/DemographicEntry"); //Index view
        }

        [HttpPost]
        public IActionResult Registration(VoterRegistrationViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var registration = new VoterRegistrationDataModel()
                {
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
    }
}
