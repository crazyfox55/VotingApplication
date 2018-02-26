using Microsoft.AspNetCore.Mvc;
using System;
namespace VotingSoftware.Controllers
{
    public class VoterRegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View(); //Index view
        }
    }

}
