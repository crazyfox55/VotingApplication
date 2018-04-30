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
using VotingApplication.Components;
using VotingApplication.Services;

namespace VotingApplication.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {

        protected ApplicationDbContext _Context;
        protected UserManager<ApplicationUser> _UserManager;
        protected IEmailService _EmailService;

        public AdminController(
            ApplicationDbContext context,
            IEmailService emailService,
            UserManager<ApplicationUser> userManager)
        {
            _Context = context;
            _EmailService = emailService;
            _UserManager = userManager;
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
                IEnumerable<ApplicationUser> userList = new HashSet<ApplicationUser>();
                switch (model.Zone)
                {
                    case "ZipCode":
                        data.RegionName = null;
                        data.ZipCode = int.Parse(model.ZipCode);
                        data.DistrictName = null;
                        userList = _Context.Zip
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
                        userList = _Context.District
                            // get the one district
                            .Where(d => d.DistrictName == data.DistrictName)
                            // load its bridge table
                            .Include(d => d.Zip)
                            // convert bridge table to one list
                            .SelectMany(d => d.Zip)
                            // convert bridge table to zip
                            .Select(zfd => zfd.Zip)
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
                    case "Region":
                        data.RegionName = model.RegionName;
                        data.ZipCode = null;
                        data.DistrictName = null;
                        userList = _Context.Region
                            // get the one region
                            .Where(r => r.RegionName == data.RegionName)
                            // load its bridge table
                            .Include(r => r.District)
                            // convert the bridge table to one list
                            .SelectMany(r => r.District)
                            // convert the bridge to district
                            .Select(dfr => dfr.District)
                            // load its bridge table
                            .Include(d => d.Zip)
                            // convert bridge table to one list
                            .SelectMany(d => d.Zip)
                            // convert bridge table to zip
                            .Select(zfd => zfd.Zip)
                            // load its addresses
                            .Include(z => z.Residents)
                            // convert addresses to one list
                            .SelectMany(z => z.Residents)
                            // load the user
                            .Include(a => a.User)
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
                    _EmailService.SendEmailAsync(user, subject, body);
                }

                _Context.Ballot.Add(data);

                _Context.SaveChanges();

                return RedirectToAction(nameof(Dashboard));
            }

            return View(model);
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

                _Context.Candidate.Add(data);

                _Context.SaveChanges();

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
            ApplicationUser[] users = _Context.Users
                
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
            ApplicationUser user = _Context.Users.Where(u => u.UserName == Username).FirstOrDefault();
            return View(new ManageUserViewModel(user));
            //return RedirectToAction(nameof(UserManagement));
        }

        [HttpPost]
        public IActionResult Edit(ManageUserViewModel model)
        {
            ApplicationUser user = _Context.Users.Where(u => u.UserName == model.PrevUsername).FirstOrDefault();
            user.EmailConfirmed = model.EmailConfirmed == "Yes" ? true : model.EmailConfirmed == "No" ?false: user.EmailConfirmed;
            user.UserName = model.Username;
            _Context.Users.Update(user);
            _Context.SaveChanges();
            return RedirectToAction(nameof(UserManagement));
        }

        // TODO: needs to use the user manager to delete a user.
        /******IMPORTANT****** TODO: need to change in CSHTML so that the button performs a POST instead of a GET ***********/
        [HttpGet] // change to [HttpPost]
        public async Task<IActionResult> Delete(string Username)
        {
            ApplicationUser user = _Context.Users.Where(u => u.UserName == Username).FirstOrDefault();
            await _UserManager.DeleteAsync(user);
            return RedirectToAction(nameof(UserManagement));
        }
        
        public JsonResult AddDistrict(string districtName, HashSet<string> values)
        {
            var district = new DistrictDataModel()
            {
                DistrictName = districtName,
                Zip = values.Select(z => new ZipFillsDistrict() { ZipCode = int.Parse(z), DistrictName = districtName }).ToList()
            };
            
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
