using Microsoft.AspNetCore.Mvc;

namespace VotingSoftware.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View(); //Index view
        }
    }
}
