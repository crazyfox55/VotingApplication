using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VotingApplication.ViewModels
{
    public class AddCandidateViewModel
    {
        //not an input this is just to store possible options for OfficeName
        public IEnumerable<string> AllOffices { get; set; }

        [Required]
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Office Title")]
        public string OfficeName { get; set; }

        [Required]
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Party")]
        public string Party { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [StringLength(maximumLength: 10, ErrorMessage = "Maximum length of 10")]
        [Display(Name = "Date of Birth")]
        public string DOB { get; set; }

    }
}
