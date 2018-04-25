using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VotingApplication.Controllers;

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
        [Remote(action: nameof(VerifyController.VerifyRegionExistsAsync), controller: "Verify")]
        public string RegionName { get; set; }

        [StringLength(maximumLength: 5, ErrorMessage = "Maximum length of 5")]
        [Display(Name = "Zip Code")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Must be 5 digits")]
        [Remote(action: nameof(VerifyController.VerifyZipExistsAsync), controller: "Verify")]
        public string ZipCode { get; set; }

        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Remote(action: nameof(VerifyController.VerifyDistrictExistsAsync), controller: "Verify")]
        public string DistrictName { get; set; }
    }
}
