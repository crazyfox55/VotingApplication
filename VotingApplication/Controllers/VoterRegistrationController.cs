using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace VotingApplication.Controllers
{
    public class VoterRegistrationController : Controller
    {
        protected ApplicationDbContext mContext;

        public VoterRegistrationController(ApplicationDbContext context)
        {
            mContext = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            mContext.Database.EnsureCreated();
            return View(); //Index view
        }

        // this should be POST or somehting not URL
        [Authorize]
        public IActionResult CreateVoterRegistration(string firstName, string lastName, string identification, string ssNumber)
        {
            var registration = new VoterRegistrationDataModel()
            {
                FirstName = firstName,
                LastName = lastName,
                Identification = identification,
                SSNumber = ssNumber
            };

            mContext.Database.EnsureCreated();
            
            mContext.Registration.Add(registration);

            mContext.SaveChanges();

            return RedirectToAction("Dashboard","User");
        }
    }
}
