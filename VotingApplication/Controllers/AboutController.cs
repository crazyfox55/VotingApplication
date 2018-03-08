using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VotingApplication.Controllers
{
    [AllowAnonymous]
    public class AboutController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(); //Index view
        }
    }
}
//hi
