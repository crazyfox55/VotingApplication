using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.ViewModels
{
    public class FilteredCandidateViewModel
    {
        public bool CandidateSelected;

        public IEnumerable<CandidateViewModel> FilteredCandidates { get; set; }
        public struct CandidateViewModel
        {
            [HiddenInput]
            public string UserId { get; set; }

            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Party")]
            public string Party { get; set; }
        }
    }
}
