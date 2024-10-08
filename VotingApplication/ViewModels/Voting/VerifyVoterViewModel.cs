﻿using System.ComponentModel.DataAnnotations;

namespace VotingApplication.ViewModels
{
    public class VerifyVoterViewModel
    {
        public VerifyVoterViewModel()
        {

        }
        [Required]
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Social Security Number")]
        [RegularExpression(@"^(\d{3}-\d{2}-\d{4}|XXX-XX-XXXX)$", ErrorMessage = "You must provide a proper social security number seperated by \'-\'.")]
        public string SSNumber { get; set; }
    }
}
