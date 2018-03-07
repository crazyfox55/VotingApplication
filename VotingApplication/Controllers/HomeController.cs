using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.Controllers
{
    public class HomeController : Controller
    {
        protected ApplicationDbContext _Context;
        
        public HomeController(ApplicationDbContext context)
        {
            _Context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            //remove, each user does not need to ensure the database is created when they navigate to the homepage
            _Context.Database.EnsureCreated();

            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(); //Error view
        }
    }
}
