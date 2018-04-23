using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.Data.Voting
{
    public class VoterVotesBallot
    {
        public String UserName { get; set; }
        public String CandidateName { get; set; }
        public String BallotName { get; set; }

      
        public ApplicationUser User { get; set; }
        public CandidateDataModel Candidate { get; set; }
        public BallotDataModel Ballot { get; set; }

    
    }
}
