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

        public IActionResult Index()
        {
            mContext.Database.EnsureCreated();

            return View();
        }

        public IActionResult Error()
        {
            return View(); //Error view
        }
    }
}
