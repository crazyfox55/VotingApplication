using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            // find and load zip data
            VoterAddressDataModel address = _Context.Address.Find(userId);
            _Context.Entry(address).Reference(adr => adr.Zip).Load();

            // load zip collections
            ZipDataModel zip = address.Zip;
            _Context.Entry(zip).Collection(z => z.Ballots).Load();
            _Context.Entry(zip).Collection(z => z.District).Load();

            //_Context.Entry(zip).Reference(z => z.Ballots).Load();
            //_Context.Entry(zip).Reference(z => z.District).Load();

            // for every district the zip is in
            foreach (ZipFillsDistrict zfd in zip.District)
            {
                _Context.Entry(zfd).Reference(z => z.District).Load();

                if (zfd.District == null)
                    continue;

                DistrictDataModel district = zfd.District;

                // load district collections
                _Context.Entry(district).Collection(d => d.Ballots).Load();
                _Context.Entry(district).Collection(d => d.Region).Load();

                //_Context.Entry(district).Reference(d => d.Ballots).Load();
                //_Context.Entry(district).Reference(d => d.Region).Load();

                // for every region that every district where the zip is in
                foreach (DistrictFillsRegion dfr in district.Region)
                {
                    _Context.Entry(dfr).Reference(z => z.Region).Load();

                    if (dfr.Region == null)
                        continue;

                    RegionDataModel region = dfr.Region;

                    //load region collections
                    _Context.Entry(region).Collection(r => r.Ballots).Load();

                    //_Context.Entry(region).Reference(r => r.Ballots).Load();


                }
            }

            #region for loops instead of linq
            // collect all the ballots
            /*
            List<FilteredBallotViewModel.BallotViewModel> ballots = new List<FilteredBallotViewModel.BallotViewModel>();
            foreach(BallotDataModel ballot in zip.Ballots)
            {
                ballots.Add(new FilteredBallotViewModel.BallotViewModel()
                {
                    BallotName = ballot.BallotName,
                    ElectionDay = ballot.ElectionDay,
                    OfficeName = ballot.OfficeName,
                    // use region, if null use district, if null use zipcode; one of them should be not null
                    Zone = ballot.RegionName ?? ballot.DistrictName ?? ballot.ZipCode.ToString()
                });
            }
            foreach (ZipFillsDistrict zfd in zip.District)
            {
                _Context.Entry(zfd.District).Collection(d => d.Ballots).Load();
                foreach (BallotDataModel ballot in zfd.District.Ballots)
                {
                    ballots.Add(new FilteredBallotViewModel.BallotViewModel()
                    {
                        BallotName = ballot.BallotName,
                        ElectionDay = ballot.ElectionDay,
                        OfficeName = ballot.OfficeName,
                        // use region, if null use district, if null use zipcode; one of them should be not null
                        Zone = ballot.RegionName ?? ballot.DistrictName ?? ballot.ZipCode.ToString()
                    });
                }
                _Context.Entry(zfd.District).Collection(d => d.Region).Load();
                foreach (DistrictFillsRegion dfr in zfd.District.Region)
                {
                    _Context.Entry(dfr.Region).Collection(r => r.Ballots).Load();
                    foreach (BallotDataModel ballot in dfr.Region.Ballots)
                    {
                        ballots.Add(new FilteredBallotViewModel.BallotViewModel()
                        {
                            BallotName = ballot.BallotName,
                            ElectionDay = ballot.ElectionDay,
                            OfficeName = ballot.OfficeName,
                            // use region, if null use district, if null use zipcode; one of them should be not null
                            Zone = ballot.RegionName ?? ballot.DistrictName ?? ballot.ZipCode.ToString()
                        });
                    }
                }
            }
            */
            #endregion

            VoterBallotSearchViewModel model = new VoterBallotSearchViewModel()
            {
                ActionViewComponent = "Vote",
                Ballots = _Context
                .Address
                .Where(adr => adr.UserId == userId)
                .FirstOrDefault().Zip
                .Ballots
                .Select(b => new FilteredBallotViewModel.BallotViewModel()
                {
                    BallotName = b.BallotName,
                    ElectionDay = b.ElectionDay,
                    OfficeName = b.OfficeName,
                    // use region, if null use district, if null use zipcode; one of them should be not null
                    Zone = b.RegionName ?? b.DistrictName ?? b.ZipCode.ToString()
                })
                .Concat(
                    _Context
                    .Address
                    .Where(adr => adr.UserId == userId)
                    .FirstOrDefault().Zip
                    .District
                    .Where(zfd => zfd.District != null)
                    .SelectMany(zfd => zfd.District.Ballots)
                    .Select(b => new FilteredBallotViewModel.BallotViewModel()
                    {
                        BallotName = b.BallotName,
                        ElectionDay = b.ElectionDay,
                        OfficeName = b.OfficeName,
                        // use region, if null use district, if null use zipcode; one of them should be not null
                        Zone = b.RegionName ?? b.DistrictName ?? b.ZipCode.ToString()
                    })
                )
                .Concat(
                        _Context
                        .Address
                        .Where(adr => adr.UserId == userId)
                        .FirstOrDefault().Zip
                        .District
                        .Where(zfd => zfd.District != null)
                        .SelectMany(zfd => zfd.District.Region)
                        .Where(dfr => dfr.Region != null)
                        .SelectMany(dfr => dfr.Region.Ballots)
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


    }
}
