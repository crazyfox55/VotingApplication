using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.ViewModels
{
    public class FilteredBallotViewModel
    {
        public string ActionViewComponent;

        public IEnumerable<BallotViewModel> FilteredBallots { get; set; }
        public struct BallotViewModel
        {
            [Display(Name = "Ballot Name")]
            public string BallotName { get; set; }

            [Display(Name = "Election Day")]
            [DataType(DataType.Date)]
            public DateTime ElectionDay { get; set; }

            [Display(Name = "Office Title")]
            public string OfficeName { get; set; }

            [Display(Name = "Location")]
            public string Zone { get; set; }
        }
    }
}
