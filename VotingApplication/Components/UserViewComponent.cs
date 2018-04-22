using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VotingApplication.ViewModels;

namespace VotingApplication.Components
{
    public class UserViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _Context;

        public UserViewComponent(ApplicationDbContext context)
        {
            _Context = context;
        }

        public IViewComponentResult Invoke(BasicUserSearchViewModel model)
        {
            FilteredUsersViewModel result = null;
            result = new FilteredUsersViewModel()
            {
                FilteredUsers = _Context.Users
                .Where(u => string.IsNullOrWhiteSpace(model.UserId) || u.Id == model.UserId)
                .Where(u => string.IsNullOrWhiteSpace(model.Username) || u.UserName == model.Username)
                .Where(u => string.IsNullOrWhiteSpace(model.FirstName) || u.Registration.FirstName == model.FirstName)
                .Where(u => string.IsNullOrWhiteSpace(model.LastName) || u.Registration.LastName == model.LastName)
                .Where(u => string.IsNullOrWhiteSpace(model.Party) || u.Demographics.Party == model.Party)
                .Include(u => u.Registration)
                .Include(u => u.Demographics)
                .Include(u => u.Candidate)
                .Select(u => new FilteredUsersViewModel.UserViewModel()
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    FirstName = u.Registration == null ? "" : u.Registration.FirstName,
                    LastName = u.Registration == null ? "" : u.Registration.LastName,
                    Party = u.Demographics == null ? "" : u.Demographics.Party,
                    BallotName = u.Candidate == null ? "" : u.Candidate.BallotName
                }),
                ActionViewComponent = model.ActionViewComponent
            };

            return View(result);
        }
    }
}
