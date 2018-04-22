using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication
{
    public class RegionDataModel
    {
        [Key]
        [MaxLength(64)]
        public string RegionName { get; set; }

        public ICollection<DistrictFillsRegion> District { get; set; }

        public ICollection<BallotDataModel> Ballots { get; set; }
    }
}
