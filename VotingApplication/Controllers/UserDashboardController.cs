using Microsoft.AspNetCore.Mvc;

namespace VotingSoftware.Controllers
{
    public class UserDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View(); //Index view
        }
    }
}
