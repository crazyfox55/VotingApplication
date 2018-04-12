using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.ViewModels
{
    public class AddBallotViewModel
    {
        [Required]
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Ballot Name")]
        public string BallotName { get; set; }
        
        [Required]
        [Display(Name = "Election Day")]
        [DataType(DataType.Date)]
        public DateTime ElectionDay { get; set; }

        [Required]
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Office Title")]
        public string OfficeName { get; set; }

        public IEnumerable<string> OfficeNames { get; set; }

        [Required]
        public string Zone { get; set; }

        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Remote(action: "VerifyRegion", controller: "Admin")]
        public string RegionName { get; set; }

        [StringLength(maximumLength: 5, ErrorMessage = "Maximum length of 5")]
        [Display(Name = "Zip Code")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Must be 5 digits")]
        [Remote(action: "VerifyZip", controller: "Admin")]
        public string ZipCode { get; set; }

        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Remote(action: "VerifyDistrict", controller: "Admin")]
        public string DistrictName { get; set; }
    }
}
