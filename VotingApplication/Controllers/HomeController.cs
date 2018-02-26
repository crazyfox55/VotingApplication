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
            /* Don't need this database call
             * mContext.Database.EnsureCreated();

            if (mContext.Settings.Any() == false)
            {
                mContext.Settings.Add(new SettingsDataModel
                {
                    Name = "BackgroundColor",
                    Value = "Red"
                });

                mContext.SaveChanges();
            }
            */

            return View(); //Index view
        }
        
        /* example of authorizing a user trying to use a particular request
        [Authorize]
        [Route("private")]
        public IActionResult Private()
        {
            return Content($"this is a private area. Welcome {HttpContext.User.Identity.Name}", "text/html");
        }
        */

        public IActionResult Error()
        {
            return View(); //Error view
        }
    }
}
