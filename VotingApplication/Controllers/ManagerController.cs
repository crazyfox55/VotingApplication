using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VotingApplication.Controllers
{
    public class ManagerController : Controller
    {
        [HttpGet]
        public IActionResult UserQuery()
        {
            return View();
        }
    }
}