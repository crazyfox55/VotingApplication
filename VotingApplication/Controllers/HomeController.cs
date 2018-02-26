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

            if (mContext.Settings.Any() == false)
            {
                mContext.Settings.Add(new SettingsDataModel
                {
                    Name = "BackgroundColor",
                    Value = "Red"
                });

                mContext.SaveChanges();
            }
            ViewData["hello"] = "goodbye";

            return View(); //Index view
        }
        
        [Authorize]
        [Route("private")]
        public IActionResult Private()
        {
            return Content($"this is a private area. Welcome {HttpContext.User.Identity.Name}", "text/html");
        }

        public IActionResult Error()
        {
            return View(); //Error view
        }
    }
}
