using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace VotingApplication.ViewModels
{
    public class VoterDemographicsViewModel
    {
        public VoterDemographicsViewModel()
        {

        }

        public VoterDemographicsViewModel(VoterDemographicsDataModel data)
        {
            if (data == null)
                return;
            Party = data.Party;
            Ethnicity = data.Ethnicity;
            Sex = data.Sex;
            IncomeRange = data.IncomeRange;
            VoterReadiness = data.VoterReadiness;
        }
        
        [Required]
        [Display(Name = "Party")]
        public string Party { get; set; }

        [Required]
        [Display(Name = "Ethnicity")]
        public string Ethnicity { get; set; }

        [Required]
        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [Required]
        [Display(Name = "Income Range")]
        public string IncomeRange { get; set; }

        [Required]
        [Display(Name = "Readiness Level")]
        public string VoterReadiness { get; set; }
    }
}
