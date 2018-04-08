using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
                var data = new OfficeDataModel(model);

                _Context.Office.Add(data);

                _Context.SaveChanges();

                return RedirectToAction(nameof(Dashboard));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AddCandidate()
        {
            var model = new AddCandidateViewModel()
            {
                AllOffices = _Context.Office.Select(o => o.OfficeName)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddCandidate(AddCandidateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = new CandidateDataModel(model);

                _Context.Candidate.Add(data);

                _Context.SaveChanges();

                return RedirectToAction(nameof(Dashboard));
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
            foreach (ZipCodeDataModel zipCode in _Context.ZipCode.Where(zip => state != null && zip.State == StateAbbreviation(state)))
            {
                ZipCodeFeature feature = new ZipCodeFeature();
                feature.properties = new Properties(zipCode);
                feature.geometry = new Geometry(zipCode);
                collection.features.Add(feature);
            }
            ZipCodeFeatureCollectionViewModel jsonData = new ZipCodeFeatureCollectionViewModel();
            jsonData.ZipCodes = collection;
            return Content(JsonConvert.SerializeObject(collection), "application/json");
        }
    }
}
