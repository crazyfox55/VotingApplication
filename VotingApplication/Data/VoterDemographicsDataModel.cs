using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication
{
    public class VoterDemographicsDataModel
    {
        [Key]
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        // virtual is required for EF to override these navigation properties
        public virtual ApplicationUser User { get; set; }

        [Required]
        [MaxLength(64)]
        public string AddressLineOne { get; set; }

        [Required]
        [MaxLength(64)]
        public string AddressLineTwo { get; set; }

        [Required]
        [MaxLength(64)]
        public string City { get; set; }

        [Required]
        [MaxLength(5)]
        public string ZipCode { get; set; }

        [Required]
        [MaxLength(2)]
        public string State { get; set; }

        [Required]
        [MaxLength(10)]
        public string DOB { get; set; }

        [Required]
        [MaxLength(32)]
        public string Party { get; set; }

        [Required]
        [MaxLength(32)]
        public string Ethnicity { get; set; }

        [Required]
        [MaxLength(16)]
        public string Sex { get; set; }

        [Required]
        [MaxLength(32)]
        public string IncomeRange { get; set; }

        [Required]
        [MaxLength(32)]
        public string VoterReadiness { get; set; }
    }
}
