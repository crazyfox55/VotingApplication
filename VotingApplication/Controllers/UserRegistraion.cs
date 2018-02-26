using Microsoft.AspNetCore.Mvc;
using System;
namespace VotingSoftware.Controllers
{
    public class UserRegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View(); //Index view
        }
    }

}
