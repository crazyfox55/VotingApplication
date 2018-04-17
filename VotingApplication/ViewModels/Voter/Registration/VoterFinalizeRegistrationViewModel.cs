using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace VotingApplication.ViewModels
{
    public class VoterFinalizeRegistrationViewModel
    {
        public VoterFinalizeRegistrationViewModel()
        {

        }

        public VoterFinalizeRegistrationViewModel(VoterRegistrationDataModel registrationData, VoterAddressDataModel addressData, VoterDemographicsDataModel demographicsData)
        {
            if (registrationData == null || addressData == null || demographicsData == null)
                return;
            FirstName = registrationData.FirstName;
            LastName = registrationData.LastName;
            DOB = registrationData.DOB;
            Identification = registrationData.Identification;
            SSNumber = registrationData.SSNumber;

            AddressLineOne = addressData.AddressLineOne;
            AddressLineTwo = addressData.AddressLineTwo;
            City = addressData.City;
            ZipCode = addressData.ZipCode.ToString();
            State = addressData.State;

            Party = demographicsData.Party;
            Ethnicity = demographicsData.Ethnicity;
            Sex = demographicsData.Sex;
            IncomeRange = demographicsData.IncomeRange;
            VoterReadiness = demographicsData.VoterReadiness;
        }

        /* REGISTRATION */
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        [Display(Name = "Identification")]
        public string Identification { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Social Security Number")]
        public string SSNumber { get; set; }

        /* ADDRESS */
        [Required]
        [Display(Name = "Address Line One")]
        public string AddressLineOne { get; set; }
        
        [Display(Name = "Address Line Two")]
        public string AddressLineTwo { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        /* DEMOGRAPHICS */
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
