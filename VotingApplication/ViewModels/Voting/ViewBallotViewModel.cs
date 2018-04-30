using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.ViewModels
{
    public class ViewBallotViewModel
    {
        [Display(Name = "Ballot Name")]
        public string BallotName { get; set; }

        [DataType(DataType.Date)]
        public DateTime ElectionDay { get; set; }


        public float CompleteVotePercent { get; set; }
        public Dictionary<string, float> EthnicityPercent { get; set; }
        public Dictionary<string, float> PartyPercent { get; set; }
        public Dictionary<string, float> IncomePercent { get; set; }
        public Dictionary<string, float> SexPercent { get; set; }
        public Dictionary<string, float> ReadinessPercent { get; set; }

        public Dictionary<string, float> PerCandidatePercent { get; set; }
    }
}
