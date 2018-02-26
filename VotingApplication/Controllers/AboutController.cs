using Microsoft.AspNetCore.Mvc;

namespace VotingApplication.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View(); //Index view
        }
    }
}
