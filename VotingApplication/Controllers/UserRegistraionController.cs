using Microsoft.AspNetCore.Mvc;

namespace VotingSoftware.Controllers
{
    public class UserRegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View(); //Index view
        }
    }
}
