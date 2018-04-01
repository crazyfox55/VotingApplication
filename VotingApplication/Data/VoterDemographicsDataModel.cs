using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VotingApplication.ViewModels;

namespace VotingApplication
{
    public class VoterDemographicsDataModel
    {
        public VoterDemographicsDataModel()
        {

        }

        public VoterDemographicsDataModel(string userId, DemographicsEntryViewModel model)
        {
            UserId = userId;
            AddressLineOne = model.AddressLineOne;
            AddressLineTwo = model.AddressLineTwo;
            City = model.City;
            ZipCode = model.ZipCode;
            State = model.State;
            DOB = model.DOB;
            Party = model.Party;
            Ethnicity = model.Ethnicity;
            Sex = model.Sex;
            IncomeRange = model.IncomeRange;
            VoterReadiness = model.VoterReadiness;
        }

        public void Update(DemographicsEntryViewModel model)
        {
            AddressLineOne = model.AddressLineOne;
            AddressLineTwo = model.AddressLineTwo;
            City = model.City;
            ZipCode = model.ZipCode;
            State = model.State;
            DOB = model.DOB;
            Party = model.Party;
            Ethnicity = model.Ethnicity;
            Sex = model.Sex;
            IncomeRange = model.IncomeRange;
            VoterReadiness = model.VoterReadiness;
        }

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
