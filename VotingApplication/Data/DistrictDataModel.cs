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
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        
        public ICollection<ZipCodeFillsDistrict> ZipCode { get; set; }

        public ICollection<DistrictFillsRegion> Region { get; set; }
        
    }
}
