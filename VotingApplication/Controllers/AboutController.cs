using Microsoft.AspNetCore.Mvc;
using System;
namespace VotingSoftware.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View(); //Index view
        }
    }

}
