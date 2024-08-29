using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

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

        /*
         * This should be a collection such that a user can be a Candidate for multiple ballot.
         * We only want them to be a Candidate for each ballot one at a time.
         * Currently with this as a one candidate the user can only run for one race one time.
         * Once the user is part of a ballot they cannot be changed to a new one. This is
         * a desirable feature until the ballot has expired... then this becomes a bad feature...
         */
        public virtual CandidateDataModel Candidate { get; set; }

        public ICollection<VoterVotesBallot> VoteGiven { get; set; }
    }
}
