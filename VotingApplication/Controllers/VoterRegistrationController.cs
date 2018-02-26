using Microsoft.AspNetCore.Mvc;

namespace VotingSoftware.Controllers
{
    public class VoterRegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View(); //Index view
        }
    }
}
