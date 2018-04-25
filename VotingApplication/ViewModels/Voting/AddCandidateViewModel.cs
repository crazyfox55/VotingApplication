using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VotingApplication.Controllers;

namespace VotingApplication.ViewModels
{
    public class AddCandidateViewModel
    {
        public static string CandidateSelectActionViewComponent = "CandidateSelect";
        public static string CandidateDeselectActionViewComponent = "CandidateDeselect";
        public static string BallotSelectActionViewComponent = "BallotSelect";
        public static string BallotDeselectActionViewComponent = "BallotDeselect";
        
        [Required]
        [HiddenInput]
        public string BallotId { get; set; }

        [Required]
        [HiddenInput]
        public string UserId { get; set; }

        public BasicUserSearchViewModel UserSearch { get; set; }

        public BasicBallotSearchViewModel BallotSearch { get; set; }
    }
}
