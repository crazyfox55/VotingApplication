using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication
{
    public class ZipCodeDataModel
    {
        [Key]
        public int Zip { get; set; }

        [Required]
        [MaxLength(64)]
        public string PrimaryCity { get; set; }

        [Required]
        [MaxLength(2)]
        public string State { get; set; }

        [Required]
        [MaxLength(64)]
        public string County { get; set; }

        [Required]
        [MaxLength(64)]
        public string Timezone { get; set; }

        [Required]
        [MaxLength(2)]
        public string Country { get; set; }

        [Required]
        [Column(TypeName = "numeric(8,3)")]
        public decimal Latitude { get; set; }

        [Required]
        [Column(TypeName = "numeric(8,3)")]
        public decimal Longitude { get; set; }

        [Required]
        [MaxLength(512)]
        public string Geometry { get; set; }
    }
}
