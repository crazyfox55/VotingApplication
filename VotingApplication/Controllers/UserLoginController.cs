using Microsoft.AspNetCore.Mvc;

namespace VotingSoftware.Controllers
{
    public class UserLoginController : Controller
    {
        public IActionResult Index()
        {
            return View(); //Index view
        }
    }
}
