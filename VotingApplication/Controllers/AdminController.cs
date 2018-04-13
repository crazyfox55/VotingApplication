using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult Dashboard()  
        {
            ViewData["UserName"] = User.Identity.Name;
            return View();
        }

        [HttpGet]
        public IActionResult UserSearch()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VerifyVoter()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddOffice()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddOffice(AddOfficeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = new OfficeDataModel()
                {
                    OfficeName = model.OfficeName,
                    OfficeDescription = model.OfficeDescription,
                    OfficeLevel = model.OfficeLevel
                };

                _Context.Office.Add(data);

                _Context.SaveChanges();

                return RedirectToAction(nameof(Dashboard));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AddBallot()
        {
            var model = new AddBallotViewModel()
            {
                OfficeNames = _Context.Office.Select(o => o.OfficeName)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddBallot(AddBallotViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = new BallotDataModel()
                {
                    BallotName = model.BallotName,
                    ElectionDay = model.ElectionDay,
                    OfficeName = model.OfficeName
                };

                switch (model.Zone)
                {
                    case "ZipCode":
                        data.RegionName = null;
                        data.ZipCode = int.Parse(model.ZipCode);
                        data.DistrictName = null;
                        break;
                    case "District":
                        data.RegionName = null;
                        data.ZipCode = null;
                        data.DistrictName = model.DistrictName;
                        break;
                    case "Region":
                        data.RegionName = model.RegionName;
                        data.ZipCode = null;
                        data.DistrictName = null;
                        break;
                }

                _Context.Ballot.Add(data);

                _Context.SaveChanges();

                return RedirectToAction(nameof(Dashboard));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> VerifyBallot(string ballotName)
        {
            if (_Context != null)
            {
                var result = await _Context.Ballot.FindAsync(ballotName);

                string errors = "";

                if (result == null)
                {
                    errors = "Ballot name does not exist, enter a valid one.";
                    return Json($"{errors}");
                }
            }

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> VerifyZip(string zipcode)
        {
            if (_Context != null)
            {
                if (int.TryParse(zipcode, out int key))
                {
                    var result = await _Context.Zip.FindAsync(key);

                    string errors = "";

                    if (result == null)
                    {
                        errors = "ZipCode does not exist, enter a valid one.";
                        return Json($"{errors}");
                    }
                }
            }

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> VerifyDistrict(string districtName)
        {
            if (_Context != null)
            {
                var result = await _Context.District.FindAsync(districtName);

                string errors = "";

                if (result == null)
                {
                    errors = "District does not exist, enter a valid one.";
                    return Json($"{errors}");
                }
            }

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> VerifyRegion(string regionName)
        {
            if (_Context != null)
            {
                var result = await _Context.Region.FindAsync(regionName);

                string errors = "";

                if (result == null)
                {
                    errors = "Region does not exist, enter a valid one.";
                    return Json($"{errors}");
                }
            }

            return Json(true);
        }

        [HttpGet]
        public IActionResult AddCandidate()
        {
            var model = new AddCandidateViewModel()
            {
                FilteredBallots = _Context.Ballot
                    .Take(5)
                    .Select(b => new AddCandidateViewModel.BallotViewModel() {
                        BallotName = b.BallotName,
                        ElectionDay = b.ElectionDay,
                        OfficeName = b.OfficeName,
                        // use region, if null use district, if null use zipcode; one of them should be not null
                        Zone = b.RegionName ?? b.DistrictName ?? b.ZipCode.ToString()
                    }),
                FilteredUsers = _Context.Users
                    .Take(5)
                    .Include(u => u.Registration)
                    .Include(u => u.Demographics)
                    .Include(u => u.Candidate)
                    .Select(u => new AddCandidateViewModel.UserViewModel()
                    {
                        UserId = u.Id,
                        UserName = u.UserName,
                        FirstName = u.Registration == null ? "" : u.Registration.FirstName,
                        LastName = u.Registration == null ? "" : u.Registration.LastName,
                        Party = u.Demographics == null ? "" : u.Demographics.Party,
                        BallotName = u.Candidate == null ? "" : u.Candidate.BallotName
                    })
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult FilterCandidateAndBallot(AddCandidateViewModel model)
        {
            model.FilteredBallots = _Context.Ballot
                .Where(b => string.IsNullOrWhiteSpace(model.BallotId) || b.BallotName == model.BallotId)
                .Where(b => model.ElectionDay == null || b.ElectionDay.Date == model.ElectionDay.Value.Date)
                .Where(b => model.BallotName == null || b.BallotName == model.BallotName)
                .Take(5)
                .Select(b => new AddCandidateViewModel.BallotViewModel()
                {
                    BallotName = b.BallotName,
                    ElectionDay = b.ElectionDay,
                    OfficeName = b.OfficeName,
                    // use region, if null use district, if null use zipcode; one of them should be not null
                    Zone = b.RegionName ?? b.DistrictName ?? b.ZipCode.ToString()
                });
            model.FilteredUsers = _Context.Users
                .Where(u => string.IsNullOrWhiteSpace(model.UserId) || u.Id == model.UserId)
                .Where(u => string.IsNullOrWhiteSpace(model.FirstName) || u.Registration.FirstName == model.FirstName)
                .Where(u => string.IsNullOrWhiteSpace(model.LastName) || u.Registration.LastName == model.LastName)
                .Where(u => string.IsNullOrWhiteSpace(model.Party) || u.Demographics.Party == model.Party)
                .Where(u => string.IsNullOrWhiteSpace(model.Username) || u.UserName == model.Username)
                .Take(5)
                .Include(u => u.Registration)
                .Include(u => u.Demographics)
                .Include(u => u.Candidate)
                .Select(u => new AddCandidateViewModel.UserViewModel()
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    FirstName = u.Registration == null ? "" : u.Registration.FirstName,
                    LastName = u.Registration == null ? "" : u.Registration.LastName,
                    Party = u.Demographics == null ? "" : u.Demographics.Party,
                    BallotName = u.Candidate == null ? "" : u.Candidate.BallotName
                });
            
            return View("AddCandidate", model);
        }

        [HttpPost]
        public IActionResult AddCandidate(AddCandidateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = new CandidateDataModel()
                {
                    UserId = model.UserId,
                    BallotName = model.BallotId
                };

                _Context.Candidate.Add(data);

                _Context.SaveChanges();

                return RedirectToAction(nameof(Dashboard));
            }
            else
            {
                model.FilteredBallots = _Context.Ballot
                    .Take(5)
                    .Select(b => new AddCandidateViewModel.BallotViewModel()
                    {
                        BallotName = b.BallotName,
                        ElectionDay = b.ElectionDay,
                        OfficeName = b.OfficeName,
                        // use region, if null use district, if null use zipcode; one of them should be not null
                        Zone = b.RegionName ?? b.DistrictName ?? b.ZipCode.ToString()
                    });
                model.FilteredUsers = _Context.Users
                    .Take(5)
                    .Include(u => u.Registration)
                    .Include(u => u.Demographics)
                    .Include(u => u.Candidate)
                    .Select(u => new AddCandidateViewModel.UserViewModel()
                    {
                        UserId = u.Id,
                        UserName = u.UserName,
                        FirstName = u.Registration == null ? "" : u.Registration.FirstName,
                        LastName = u.Registration == null ? "" : u.Registration.LastName,
                        Party = u.Demographics == null ? "" : u.Demographics.Party,
                        BallotName = u.Candidate == null ? "" : u.Candidate.BallotName
                    });
            }

            return View(model);
        }


        
        // TODO: use the page and usersPerPage fields to make a interactive table.
        [HttpGet]
        public IActionResult UserManagement(int page = 0, int usersPerPage = 5)
        {
            // max 50 users
            usersPerPage = Math.Min(usersPerPage, 50);
            // min 5 users
            usersPerPage = Math.Max(usersPerPage, 5);

            // TODO: create an actual model so we can send additional information. (i.e total number of pages and number of users)
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
        
        // TODO: needs to use the user manager to display a form where a user can be edited.
        [HttpGet]
        public IActionResult Edit(string Username)
        {
            return RedirectToAction(nameof(UserManagement));
        }

        // TODO: needs to use the user manager to delete a user.
        /******IMPORTANT****** TODO: need to change in CSHTML so that the button performs a POST instead of a GET ***********/
        [HttpGet] // change to [HttpPost]
        public IActionResult Delete(string Username)
        {
            return RedirectToAction(nameof(UserManagement));
        }


        public JsonResult AddDistrict(string districtName, HashSet<string> values)
        {
            var district = new DistrictDataModel()
            {
                DistrictName = districtName,
                Zip = values.Select(z => new ZipFillsDistrict() { ZipCode = int.Parse(z), DistrictName = districtName }).ToList()
            };

            /*district.Zip = _Context.Zip
                .Where(z => values.Contains(z.ZipCode.ToString()))
                .Select(z => new ZipFillsDistrict() { Zip = z, District = district })
                .ToList();
            foreach (string zipCode in values)
            {
                district.Zip.Add(new ZipFillsDistrict() { ZipCode = int.Parse(zipCode), DistrictName = districtName });
            }
            */

            _Context.District.Add(district);

            _Context.SaveChanges();

            return Json(new { Result = string.Format("First item in list: '{0}'", districtName) });
        }

        [HttpGet]
        public IActionResult ZipCodeMap()
        {
            return View(); //Index view
        }

        private string StateAbbreviation(string state)
        {
            switch (state.ToUpper())
            {
                case "ALABAMA": return "AL";
                case "ALASKA": return "AK";
                case "ARIZONA": return "AZ";
                case "ARKANSAS": return "AR";
                case "CALIFORNIA": return "CA";
                case "COLORADO": return "CO";
                case "CONNECTICUT": return "CT";
                case "DELAWARE": return "DE";
                case "FLORIDA": return "FL";
                case "GEORGIA": return "GA";
                case "HAWAII": return "HI";
                case "IDAHO": return "ID";
                case "ILLINOIS": return "IL";
                case "INDIANA": return "IN";
                case "IOWA": return "IA";
                case "KANSAS": return "KS";
                case "KENTUCKY": return "KY";
                case "LOUISIANA": return "LA";
                case "MAINE": return "ME";
                case "MARYLAND": return "MD";
                case "MASSACHUSETTS": return "MA";
                case "MICHIGAN": return "MI";
                case "MINNESOTA": return "MN";
                case "MISSISSIPPI": return "MS";
                case "MISSOURI": return "MO";
                case "MONTANA": return "MT";
                case "NEBRASKA": return "NE";
                case "NEVADA": return "NV";
                case "NEW HAMPSHIRE": return "NH";
                case "NEW JERSEY": return "NJ";
                case "NEW MEXICO": return "NM";
                case "NEW YORK": return "NY";
                case "NORTH CAROLINA": return "NC";
                case "NORTH DAKOTA": return "ND";
                case "OHIO": return "OH";
                case "OKLAHOMA": return "OK";
                case "OREGON": return "OR";
                case "PENNSYLVANIA": return "PA";
                case "RHODE ISLAND": return "RI";
                case "SOUTH CAROLINA": return "SC";
                case "SOUTH DAKOTA": return "SD";
                case "TENNESSEE": return "TN";
                case "TEXAS": return "TX";
                case "UTAH": return "UT";
                case "VERMONT": return "VT";
                case "VIRGINIA": return "VA";
                case "WASHINGTON": return "WA";
                case "WEST VIRGINIA": return "WV";
                case "WISCONSIN": return "WI";
                case "WYOMING": return "WY";
                case "GUAM": return "GU";
                case "PUERTO RICO": return "PR";
                case "VIRGIN ISLANDS": return "VI";
                default: return "IA";
            }
        }
        [HttpGet]
        public IActionResult RequestZipCodes(string state = null)
        {
            ZipCodeFeatureCollection collection = new ZipCodeFeatureCollection();
            if (state != null)
            {
                foreach (ZipDataModel zipCode in _Context.Zip.Where(zip => zip.State == StateAbbreviation(state)))
                {
                    ZipCodeFeature feature = new ZipCodeFeature
                    {
                        properties = new Properties(zipCode),
                        geometry = new Geometry(zipCode)
                    };
                    collection.features.Add(feature);
                }
            }
            return Content(JsonConvert.SerializeObject(collection), "application/json");
        }
        
    }
}
