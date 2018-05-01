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
        public string VoterName { get; set; }
        public string BallotName { get; set; }
        public string CandidateName { get; set; }
        
        public ApplicationUser Voter { get; set; }
        public BallotDataModel Ballot { get; set; }
        public CandidateDataModel Candidate { get; set; }
    }
}
