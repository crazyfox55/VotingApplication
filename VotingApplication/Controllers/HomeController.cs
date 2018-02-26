using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace VotingApplication.Controllers
{
    public class HomeController : Controller
    {
        protected ApplicationDbContext mContext;

        public HomeController(ApplicationDbContext context)
        {
            this.mContext = context;
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



            return View(); //Index view
        }

        public IActionResult Error()
        {
            return View(); //Error view
        }
    }
}
