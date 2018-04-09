using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace VotingApplication.ViewModels
{
    public class DemographicsEntryViewModel
    {
        public DemographicsEntryViewModel()
        {

        }

        public DemographicsEntryViewModel(VoterDemographicsDataModel data)
        {
            if (data == null)
                return;
            AddressLineOne = data.AddressLineOne;
            AddressLineTwo = data.AddressLineTwo;
            City = data.City;
            ZipCode = data.ZipCode.ToString();
            State = data.State;
            DOB = data.DOB;
            Party = data.Party;
            Ethnicity = data.Ethnicity;
            Sex = data.Sex;
            IncomeRange = data.IncomeRange;
            VoterReadiness = data.VoterReadiness;
        }

        [Required]
        [StringLength(maximumLength: 64, ErrorMessage = "Too long")]
        [Display(Name = "Address Line One")]
        public string AddressLineOne { get; set; }

        [StringLength(maximumLength: 64, ErrorMessage = "Too long")]
        [Display(Name = "Address Line Two")]
        public string AddressLineTwo { get; set; }

        [Required]
        [StringLength(maximumLength: 64, ErrorMessage = "Too long")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [StringLength(maximumLength: 5, ErrorMessage = "Maximum length of 5")]
        [Display(Name = "Zip Code")]
        [RegularExpression(@"^\d{5}$",ErrorMessage = "Must be 5 digits")]
        [Remote(action: "VerifyZip", controller: "Admin")]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(maximumLength: 2, ErrorMessage = "Maximum length of 2")]
        [Display(Name = "State")]
        [RegularExpression(@"^(-i:A[LKSZRAEP]|C[AOT]|D[EC]|F[LM]|G[AU]|HI|I[ADLN]|K[SY]|LA|M[ADEHINOPST]|N[CDEHJMVY]|O[HKR]|P[ARW]|RI|S[CD]|T[NX]|UT|V[AIT]|W[AIVY])$", ErrorMessage = "Invalid State (must be uppercase)")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

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
