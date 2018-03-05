using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.Controllers
{
    public class HomeController : Controller
    {
        protected ApplicationDbContext mContext;
        
        public HomeController(ApplicationDbContext context)
        {
            mContext = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            //remove, each user does not need to ensure the database is created when they navigate to the homepage
            mContext.Database.EnsureCreated();

            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(); //Error view
        }
    }
}
