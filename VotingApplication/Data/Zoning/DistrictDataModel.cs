using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication
{
    public class DistrictDataModel
    {
        [Key]
        [MaxLength(64)]
        public string DistrictName { get; set; }
        
        public ICollection<ZipFillsDistrict> Zip { get; set; }

        public ICollection<DistrictFillsRegion> Region { get; set; }

        public ICollection<BallotDataModel> Ballots { get; set; }

    }
}
