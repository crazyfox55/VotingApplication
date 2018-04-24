using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VotingApplication.Data.Voting;
using VotingApplication.ViewModels;

namespace VotingApplication
{
    public class CandidateDataModel
    {
        public CandidateDataModel()
        {

        }

        public CandidateDataModel(AddCandidateViewModel model)
        {
            if (model == null)
                return;
        }

        // should use username instead, same thing with the registration and 
        [Key]
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        // virtual is required for EF to override these navigation properties
        public virtual ApplicationUser User { get; set; }

        [Required]
        [ForeignKey(nameof(BallotDataModel))]
        public string BallotName { get; set; }

        public virtual BallotDataModel Ballot { get; set; }

        public virtual ICollection<VoterVotesBallot> VoteReceived { get; set; }

    }
}
