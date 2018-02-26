using Microsoft.AspNetCore.Mvc;

namespace VotingApplication.Controllers
{
    public class VoterRegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View(); //Index view
        }
    }
}
