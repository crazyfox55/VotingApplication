using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.ViewModels
{
    public class VoterBallotSearchViewModel
    {
        public string ActionViewComponent;

        public IEnumerable<FilteredBallotViewModel.BallotViewModel> Ballots { get; set; }
    }
}
