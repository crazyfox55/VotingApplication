using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VotingApplication.Data.Voting;

namespace VotingApplication
{
    public class ApplicationUser : IdentityUser
    {
        // these are properties apply to the users login account not their voting data.
        [MaxLength(256)]
        public string SecurityQuestionOne { get; set; }
        
        [MaxLength(256)]
        public string SecurityQuestionTwo { get; set; }
        
        [MaxLength(256)]
        public string SecurityAnswerOne { get; set; }
        
        [MaxLength(256)]
        public string SecurityAnswerTwo { get; set; }

        // virtual is required for EF to override these navigation properties
        public virtual VoterRegistrationDataModel Registration { get; set; }

        public virtual VoterAddressDataModel Address { get; set; }

        public virtual VoterDemographicsDataModel Demographics { get; set; }

        public virtual CandidateDataModel Candidate { get; set; }

        public virtual ICollection<VoterVotesBallot> Ballot { get; set; }
    }
}
