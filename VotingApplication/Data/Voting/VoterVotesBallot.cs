using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication
{
    public class VoterVotesBallot
    {
        public String VoterName { get; set; }
        public String BallotName { get; set; }
        
        public ApplicationUser Voter { get; set; }
        public BallotDataModel Ballot { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public String CandidateName { get; set; }

        public virtual CandidateDataModel Candidate { get; set; }
    }
}
