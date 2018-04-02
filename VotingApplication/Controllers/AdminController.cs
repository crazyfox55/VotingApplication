using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using VotingApplication.ViewModels;

namespace VotingApplication.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        protected ApplicationDbContext _Context;

        public AdminController(
            ApplicationDbContext context)
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
        
        public IActionResult Dashboard()
        {
            ViewData["UserName"] = User.Identity.Name;
            return View(); 
        }

        public IActionResult UserManagement(int page = 0, int usersPerPage = 5)
        {
            // max 50 users
            usersPerPage = Math.Min(usersPerPage, 50);
            // min 5 users
            usersPerPage = Math.Max(usersPerPage, 5);

            List<ManageUserViewModel> model = new List<ManageUserViewModel>();
            ApplicationUser[] users = _Context.Users
                .Skip(page * usersPerPage)
                .Take(usersPerPage)
                .ToArray();
            for (int i = 0; i < users.Length; i++)
            {
                model.Add(new ManageUserViewModel(users[i]));
            }

            return View(model);
        }

        // needs to use the user manager to display a form where a user can be edited.
        public IActionResult Edit(string Username)
        {
            return RedirectToAction(nameof(UserManagement));
        }

        // needs to use the user manager to delete a user.
        public IActionResult Delete(string Username)
        {
            return RedirectToAction(nameof(UserManagement));
        }
    }
}
