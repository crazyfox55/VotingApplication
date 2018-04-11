using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VotingApplication.ViewModels
{
    public class AddCandidateViewModel
    {
        //not an input this is just to store possible options for OfficeName
        // use a view model so we can change how this looks for the browser
        // Ballot Name, Election Day, Date Format "{0:d}".
        // i.e - [DisplayFormat(DataFormatString = "{0:d}")]
        public IEnumerable<BallotDataModel> FilteredBallots { get; set; }

        public IEnumerable<ApplicationUser> FilteredUsers { get; set; }

        [Required]
        [HiddenInput]
        public string BallotId { get; set; }
        [Required]
        [HiddenInput]
        public string UserId { get; set; }

        [Remote(action: "VerifyBallotName", controller: "Registration")]
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Ballot Name")]
        public string BallotName { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Election Day")]
        public DateTime? ElectionDay { get; set; }

        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Party")]
        public string Party { get; set; }
        
        [Display(Name = "Username")]
        public string Username { get; set; }

    }
}
