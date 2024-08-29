using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;
using VotingApplication.Components;
using VotingApplication.Services;
using VotingApplication.ViewModels;

namespace VotingApplication.Controllers
{
    [Authorize(Roles = "Administrator")]
    public sealed class AdminController(
        ApplicationDbContext context,
        IEmailService emailService,
        UserManager<ApplicationUser> userManager) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IEmailService _emailService = emailService;

        [HttpGet]
        public IActionResult Dashboard()
        {
            ViewData["UserName"] = User.Identity.Name;
            return View();
        }

        [HttpGet]
        public IActionResult UserSearch()
        {
            return View(new UserSearchViewModel());
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

                _context.Office.Add(data);

                _context.SaveChanges();

                return RedirectToAction(nameof(Dashboard));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AddBallot()
        {
            var model = new AddBallotViewModel()
            {
                OfficeNames = _context.Office.Select(o => o.OfficeName)
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
                IEnumerable<ApplicationUser> userList = new HashSet<ApplicationUser>();
                switch (model.Zone)
                {
                    case "ZipCode":
                        data.RegionName = null;
                        data.ZipCode = int.Parse(model.ZipCode);
                        data.DistrictName = null;
                        userList = _context.Zip
                            // get the one zip
                            .Where(z => z.ZipCode == data.ZipCode)
                            // load its addresses
                            .Include(z => z.Residents)
                            // convert addresses to one list
                            .SelectMany(z => z.Residents)
                            // load the user
                            .Include(a => a.User)
                            // convert address to user
                            .Select(a => a.User)
                            // return users with confirmed emails
                            .Where(u => u.EmailConfirmed);
                        break;
                    case "District":
                        data.RegionName = null;
                        data.ZipCode = null;
                        data.DistrictName = model.DistrictName;
                        userList = _context.District
                            // get the one district
                            .Where(d => d.DistrictName == data.DistrictName)
                            // load its bridge table
                            .Include(d => d.Zip)
                            // load its addresses
                            .ThenInclude(zfd => zfd.Zip)
                            .ThenInclude(z => z.Residents)
                            // load the user
                            .ThenInclude (r => r.User)
                            // convert bridge table to one list
                            .SelectMany(d => d.Zip)
                            // convert bridge table to zip
                            .Select(zfd => zfd.Zip)
                            // convert addresses to one list
                            .SelectMany(z => z.Residents)
                            // convert address to user
                            .Select(a => a.User)
                            // return users with confirmed emails
                            .Where(u => u.EmailConfirmed);
                        break;
                    case "Region":
                        data.RegionName = model.RegionName;
                        data.ZipCode = null;
                        data.DistrictName = null;
                        userList = _context.Region
                            // get the one region
                            .Where(r => r.RegionName == data.RegionName)
                            // load its bridge table
                            .Include(r => r.District)
                            // load its bridge table
                            .ThenInclude(dfr => dfr.District)
                            .ThenInclude(d => d.Zip)
                            // load its addresses
                            .ThenInclude(zfd => zfd.Zip)
                            .ThenInclude(z => z.Residents)
                            // load the user
                            .ThenInclude(r => r.User)
                            // convert the bridge table to one list
                            .SelectMany(r => r.District)
                            // convert the bridge to district
                            .Select(dfr => dfr.District)
                            // convert bridge table to one list
                            .SelectMany(d => d.Zip)
                            // convert bridge table to zip
                            .Select(zfd => zfd.Zip)
                            // convert addresses to one list
                            .SelectMany(z => z.Residents)
                            // convert address to user
                            .Select(a => a.User)
                            // return users with confirmed emails
                            .Where(u => u.EmailConfirmed)
                            .Distinct();
                        break;
                }
                string subject = "New Ballot Avaliable";
                string body = "There is a new ballot that you can vote on.";
                foreach (ApplicationUser user in userList)
                {
                    _emailService.SendEmailAsync(user, subject, body);
                }

                _context.Ballot.Add(data);

                _context.SaveChanges();

                return RedirectToAction(nameof(Dashboard));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ViewBallot(string ballotname)
        {
            var ballotQuery = _context.Ballot
                .Where(b => b.BallotName == ballotname)
                .Include(b => b.Cadidates)
                .Include(b => b.Voter);

            var completeVoters = ballotQuery
                .SelectMany(b => b.Voter)
                .Select(v => v.Voter);

            var ballot = ballotQuery.FirstOrDefault();

            if (ballot == null)
                return BadRequest("Not a valid ballot name");

            //_context.Entry(ballot).Collection(b => b.Cadidates).Load();
            //_context.Entry(ballot).Collection(b => b.Voter).Load();
            //_context.Entry(ballot).Reference(b => b.Office).Load();

            /*List<ApplicationUser> completeVoters = _context
                .Ballot
                .Where(b => b.BallotName == ballotname)
                .SelectMany(b => b.Voter)
                .Select(vvb => vvb.Voter)
                .ToList();*/

            var location = ballot.ZipCode?.ToString() ?? ballot.DistrictName ?? ballot.RegionName;

            IIncludableQueryable<ApplicationUser, VoterDemographicsDataModel> allVoters = null;
            if (location == ballot.ZipCode.ToString())
            {
                allVoters = _context.Users
                    .Where(u => u.Address.ZipCode == ballot.ZipCode)
                    .Include(u => u.Demographics);
            }
            else if (location == ballot.DistrictName)
            {
                allVoters = _context.Users
                    .Where(u => u.Address.Zip.District.Any(zfd => zfd.DistrictName == ballot.DistrictName))
                    .Include(u => u.Demographics);
            }
            else if (location == ballot.RegionName)
            {
                allVoters = _context.Users
                    .Where(u => u.Address.Zip.District.Any(zfd => zfd.District.Region.Any(dfr => dfr.RegionName == ballot.RegionName)))
                    .Include(u => u.Demographics);
            }

            int totalComplete = completeVoters.Count();
            int totalUsers = allVoters?.Count() ?? 0;
            var ethnicityPercent = new Dictionary<string, float>();
            var perCandidatePercent = new Dictionary<string, float>();
            var incomePercent = new Dictionary<string, float>();
            var partyPercent = new Dictionary<string, float>();
            var readinessPercent = new Dictionary<string, float>();
            var sexPercent = new Dictionary<string, float>();

            var userDemographics = allVoters.Select(u => u.Demographics);

            foreach (VoterDemographicsDataModel demographics in userDemographics)
            {
                if (demographics == null)
                    continue;

                if (ethnicityPercent.ContainsKey(demographics.Ethnicity))
                    ethnicityPercent[demographics.Ethnicity] += (float)(100.0 / totalUsers);
                else
                    ethnicityPercent[demographics.Ethnicity] = (float)(100.0 / totalUsers);

                if (incomePercent.ContainsKey(demographics.IncomeRange))
                    incomePercent[demographics.IncomeRange] += (float)(100.0 / totalUsers);
                else
                    incomePercent[demographics.IncomeRange] = (float)(100.0 / totalUsers);

                if (partyPercent.ContainsKey(demographics.Party))
                    partyPercent[demographics.Party] += (float)(100.0 / totalUsers);
                else
                    partyPercent[demographics.Party] = (float)(100.0 / totalUsers);

                if (readinessPercent.ContainsKey(demographics.VoterReadiness))
                    readinessPercent[demographics.VoterReadiness] += (float)(100.0 / totalUsers);
                else
                    readinessPercent[demographics.VoterReadiness] = (float)(100.0 / totalUsers);

                if (sexPercent.ContainsKey(demographics.Sex))
                    sexPercent[demographics.Sex] += (float)(100.0 / totalUsers);
                else
                    sexPercent[demographics.Sex] = (float)(100.0 / totalUsers);
            }

            var votes = ballotQuery
                .SelectMany(b => b.Cadidates)
                .Select(c => new { c.User.UserName, c.VoteReceived.Count });
            foreach (var candidateVotes in votes)
            {
                perCandidatePercent[candidateVotes.UserName] = (float)((candidateVotes.Count * 100.0) / (float)totalComplete);
            }

            ViewBallotViewModel model = new ViewBallotViewModel()
            {
                BallotName = ballotname,
                ElectionDay = ballot.ElectionDay,
                CompleteVotePercent = totalUsers == 0 ? 1 : (float)totalComplete / (float)totalUsers,
                PerCandidatePercent = perCandidatePercent,
                EthnicityPercent = ethnicityPercent,
                IncomePercent = incomePercent,
                PartyPercent = partyPercent,
                ReadinessPercent = readinessPercent,
                SexPercent = sexPercent
            };

            return View(model);
        }
        
        [HttpGet]
        public IActionResult ViewAllBallots()
        {
            return View(new BasicBallotSearchViewModel());
        }

        [HttpPost]
        public IActionResult SearchBallot(BasicBallotSearchViewModel model)
        {
            model.ActionViewComponent = "ViewBallot";
            return ViewComponent(typeof(BallotViewComponent), model);
        }

        [HttpGet]
        public IActionResult AddCandidate()
        {
            return View(new AddCandidateViewModel());
        }

        [HttpPost]
        public IActionResult FilterUsers(AddCandidateViewModel model)
        {
            if(string.IsNullOrWhiteSpace(model.UserId))
                model.UserSearch.ActionViewComponent = AddCandidateViewModel.CandidateSelectActionViewComponent;
            else
                model.UserSearch.ActionViewComponent = AddCandidateViewModel.CandidateDeselectActionViewComponent;
            return ViewComponent(typeof(UserViewComponent), model.UserSearch);
        }

        [HttpPost]
        public IActionResult FilterBallot(AddCandidateViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.BallotId))
                model.BallotSearch.ActionViewComponent = AddCandidateViewModel.BallotSelectActionViewComponent;
            else
                model.BallotSearch.ActionViewComponent = AddCandidateViewModel.BallotDeselectActionViewComponent;
            return ViewComponent(typeof(BallotViewComponent), model.BallotSearch);
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

                _context.Candidate.Add(data);

                _context.SaveChanges();

                return RedirectToAction(nameof(Dashboard));
            }

            return View(model);
        }
        
        // TODO: use the page and usersPerPage fields to make a interactive table.
        [HttpGet]
        public IActionResult UserManagement()
        {
           
            // TODO: create an actual model so we can send additional information. (i.e total number of pages and number of users)
            List<ManageUserViewModel> model = new List<ManageUserViewModel>();
            ApplicationUser[] users = _context.Users
                
                .Take(1000)
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
            ApplicationUser user = _context.Users.Where(u => u.UserName == Username).FirstOrDefault();
            return View(new ManageUserViewModel(user));
            //return RedirectToAction(nameof(UserManagement));
        }

        [HttpPost]
        public IActionResult Edit(ManageUserViewModel model)
        {
            ApplicationUser user = _context.Users.Where(u => u.UserName == model.PrevUsername).FirstOrDefault();
            user.EmailConfirmed = model.EmailConfirmed == "Yes" ? true : model.EmailConfirmed == "No" ? false: user.EmailConfirmed;
            user.UserName = model.Username;
            _userManager.UpdateAsync(user).Wait();
            return RedirectToAction(nameof(UserManagement));
        }

        // TODO: needs to use the user manager to delete a user.
        /******IMPORTANT****** TODO: need to change in CSHTML so that the button performs a POST instead of a GET ***********/
        [HttpGet] // change to [HttpPost]
        public async Task<IActionResult> Delete(string Username)
        {
            ApplicationUser user = _context.Users.Where(u => u.UserName == Username).FirstOrDefault();
            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(UserManagement));
        }
        
        [HttpPost]
        public JsonResult AddDistrict(string districtName, HashSet<string> values)
        {
            if (string.IsNullOrWhiteSpace(districtName))
                return Json(new { Result = "District name is required to create a district." });
            if (values == null || values.Count() == 0)
                return Json(new { Result = "A list of zipcodes is required to create a district." });
            
            var district = new DistrictDataModel()
            {
                DistrictName = districtName,
                Zip = values.Select(z => new ZipFillsDistrict() { ZipCode = int.Parse(z), DistrictName = districtName }).ToList()
            };
            
            _context.District.Add(district);

            _context.SaveChanges();

            return Json(new { Result = string.Format("District sucessfuly created with name '{0}'.", districtName) });
        }

        [HttpPost]
        public JsonResult UpdateDistrict(string districtName, HashSet<string> values)
        {
            if (string.IsNullOrWhiteSpace(districtName))
                return Json(new { Result = "District name is required to update the district." });
            if(values == null || values.Count() == 0)
                return Json(new { Result = "A list of zipcodes is required to update a district." });
            
            var district = _context.District.Include(d => d.Zip).SingleOrDefault(d => d.DistrictName == districtName);

            if (district == null)
                return Json(new { Result = string.Format("There is no district with the name '{0}'.", districtName) });

            HashSet<ZipFillsDistrict> zipCopy = new HashSet<ZipFillsDistrict>(district.Zip);
            foreach(ZipFillsDistrict zfd in zipCopy)
            {
                if (values.Contains(zfd.ZipCode.ToString()) == false)
                {
                    district.Zip.Remove(zfd);
                }
                else
                {
                    values.Remove(zfd.ZipCode.ToString());
                }
            }

            foreach (string zipcode in values)
            {
                district.Zip.Add(new ZipFillsDistrict()
                {
                    DistrictName = districtName,
                    ZipCode = int.Parse(zipcode)
                });
            }
            
            _context.District.Update(district);

            _context.SaveChanges();

            return Json(new { Result = string.Format("District sucessfuly updated with name '{0}'.", districtName) });
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
                default: return state;
            }
        }
        [HttpGet]
        public IActionResult RequestZipCodes(string state = null)
        {
            ZipCodeFeatureCollection collection = new ZipCodeFeatureCollection();
            if (state != null)
            {
                foreach (ZipDataModel zipCode in _context.Zip.Where(zip => zip.State == StateAbbreviation(state)))
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
        
        [HttpGet]
        public IActionResult RequestDistrict(string districtName = null)
        {
            if (districtName == null)
                return Json(new { Result = "District name is required to view a district." });
            var districtData = _context.District.Where(d => d.DistrictName == districtName).SelectMany(d => d.Zip).Select(zfd => zfd.Zip).ToList();
            if (districtData == null || districtData.Count() == 0)
                return Json(new { Result = string.Format("There is no district with the name '{0}'.", districtName) });
            return Content(JsonConvert.SerializeObject(districtData));
        }
    }
}
