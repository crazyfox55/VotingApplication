using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.ViewModels
{
    public class BallotVoteViewModel
    {
        public static string CandidateSelectActionViewComponent = "CandidateSelect";
        public static string CandidateDeselectActionViewComponent = "CandidateDeselect";
        
        [Required]
        [HiddenInput]
        public string BallotId { get; set; }

        [Required]
        [HiddenInput]
        public string UserId { get; set; }
        
        public BasicCandidateSearchViewModel CandidateSearch { get; set; }
    }
}
