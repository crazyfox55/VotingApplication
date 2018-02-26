using Microsoft.AspNetCore.Mvc;

namespace VotingApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); //Index view
        }

        public IActionResult Error()
        {
            return View(); //Error view
        }
    }
}
