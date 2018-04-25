using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.ViewModels
{
    public class BasicCandidateSearchViewModel
    {
        public bool CandidateSelected;

        [HiddenInput]
        public string UserId { get; set; }

        [HiddenInput]
        public string BallotId { get; set; }
    }
}
