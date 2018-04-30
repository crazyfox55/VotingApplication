using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApplication.Components;
using VotingApplication.ViewModels;

namespace VotingApplication.Controllers
{
    [Authorize(Roles = "RegisteredVoter")]
    public class VoterController : Controller
    {
        protected UserManager<ApplicationUser> _UserManager;
        protected ApplicationDbContext _Context;

        public VoterController(UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _UserManager = userManager;
            _Context = context;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            string userId = _UserManager.GetUserId(User);

            VoterBallotSearchViewModel model = new VoterBallotSearchViewModel()
            {
                ActionViewComponent = "Vote",
                Ballots = _Context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Address)
                .Select(u => u.Address)
                .Include(a => a.Zip)
                .Select(a => a.Zip)
                .Include(z => z.Ballots)
                .Include(z => z.District)
                .SelectMany(z => z.District)
                .Select(zfd => zfd.District)
                .Include(d => d.Ballots)
                .Include(d => d.Region)
                .SelectMany(d => d.Region)
                .Select(dfr => dfr.Region)
                .Include(r => r.Ballots)
                .SelectMany(r => r.Ballots)
                .Select(b => new FilteredBallotViewModel.BallotViewModel()
                {
                    BallotName = b.BallotName,
                    ElectionDay = b.ElectionDay,
                    OfficeName = b.OfficeName,
                    // use region, if null use district, if null use zipcode; one of them should be not null
                    Zone = b.RegionName ?? b.DistrictName ?? b.ZipCode.ToString()
                })
                .Union(
                    _Context.Users
                    .Where(u => u.Id == userId)
                    .Select(u => u.Address)
                    .Select(a => a.Zip)
                    .SelectMany(z => z.District)
                    .Select(zfd => zfd.District)
                    .SelectMany(d => d.Ballots)
                    .Select(b => new FilteredBallotViewModel.BallotViewModel()
                    {
                        BallotName = b.BallotName,
                        ElectionDay = b.ElectionDay,
                        OfficeName = b.OfficeName,
                        // use region, if null use district, if null use zipcode; one of them should be not null
                        Zone = b.RegionName ?? b.DistrictName ?? b.ZipCode.ToString()
                    })
                )
                .Union(
                    _Context.Users
                    .Where(u => u.Id == userId)
                    .Select(u => u.Address)
                    .Select(a => a.Zip)
                    .SelectMany(z => z.Ballots)
                    .Select(b => new FilteredBallotViewModel.BallotViewModel()
                    {
                        BallotName = b.BallotName,
                        ElectionDay = b.ElectionDay,
                        OfficeName = b.OfficeName,
                        // use region, if null use district, if null use zipcode; one of them should be not null
                        Zone = b.RegionName ?? b.DistrictName ?? b.ZipCode.ToString()
                    })
                )
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult BallotVote(string ballotName)
        {
            return View(new BallotVoteViewModel()
            {
                BallotId = ballotName
            });
        }

        [HttpPost]
        public IActionResult FilterCandidates(BallotVoteViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.UserId))
                model.CandidateSearch.CandidateSelected = false;
            else
                model.CandidateSearch.CandidateSelected = true;
            return ViewComponent(typeof(CandidateViewComponent), model.CandidateSearch);
        }

        [HttpPost]
        public IActionResult BallotVote(BallotVoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = _UserManager.GetUserId(User);
                var data = new VoterVotesBallot()
                {
                    VoterName = userId,
                    BallotName = model.BallotId,
                    CandidateName = model.UserId
                };

                _Context.Votes.Add(data);

                _Context.SaveChanges();

                return RedirectToAction(nameof(Dashboard));
            }

            return View(model);
        }
    }
}
