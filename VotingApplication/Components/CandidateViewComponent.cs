using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApplication.ViewModels;

namespace VotingApplication.Components
{
    public class CandidateViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _Context;

        public CandidateViewComponent(ApplicationDbContext context)
        {
            _Context = context;
        }

        public IViewComponentResult Invoke(BasicCandidateSearchViewModel model)
        {
            FilteredCandidateViewModel result = null;
            result = new FilteredCandidateViewModel()
            {
                FilteredCandidates = _Context.Users
                .Where(u => model.CandidateSelected == false || u.Id == model.UserId)
                .Where(u => u.Candidate != null && u.Candidate.BallotName == model.BallotId)
                .Select(c => new FilteredCandidateViewModel.CandidateViewModel()
                {
                    UserId = c.Id,
                    FirstName = c.Registration.FirstName,
                    LastName = c.Registration.LastName,
                    Party = c.Demographics.Party
                }),
                CandidateSelected = model.CandidateSelected
            };

            return View(result);
        }
    }
}
