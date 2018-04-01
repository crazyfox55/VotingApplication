using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.Controllers
{
    [AllowAnonymous]
    public class AdminController : Controller
    {
        protected ApplicationDbContext _Context;
        
        public AdminController(ApplicationDbContext context)
        {
            _Context = context;
        }
        
        [HttpGet]
        public IActionResult AddOffice()
        {
            return View(); 
        }

        [HttpGet]
        public IActionResult AddCandidate()
        {
            return View(); 
        }
    }
}
