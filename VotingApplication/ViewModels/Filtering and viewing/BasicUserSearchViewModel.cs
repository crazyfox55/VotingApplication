using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VotingApplication.Controllers;

namespace VotingApplication.ViewModels
{
    public class BasicUserSearchViewModel
    {
        public string ActionViewComponent;
        
        [HiddenInput]
        public string UserId { get; set; }
        
        [Display(Name = "Username")]
        public string Username { get; set; }

        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Party")]
        public string Party { get; set; }
        
    }
}
