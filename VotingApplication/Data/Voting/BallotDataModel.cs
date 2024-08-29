using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApplication
{
    public class BallotDataModel
    {
        [Key]
        [MaxLength(64)]
        public string BallotName { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime ElectionDay { get; set; }
        
        [Required]
        [ForeignKey(nameof(Office))]
        public string OfficeName { get; set; }
        
        public virtual OfficeDataModel Office { get; set; }

        public ICollection<CandidateDataModel> Cadidates { get; set; }
        
        public ICollection<VoterVotesBallot> Voter { get; set; }

        #region One of the three is required
        [ForeignKey(nameof(Region))]
        public string RegionName { get; set; }

        public virtual RegionDataModel Region { get; set; }

        [ForeignKey(nameof(Zip))]
        public int? ZipCode { get; set; }

        public virtual ZipDataModel Zip { get; set; }

        [ForeignKey(nameof(District))]
        public string DistrictName { get; set; }

        public virtual DistrictDataModel District { get; set; }
        #endregion
    }
}
