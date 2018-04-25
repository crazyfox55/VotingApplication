using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using VotingApplication.Controllers;

namespace VotingApplication.ViewModels
{
    public class UserSearchViewModel
    {
        public UserSearchViewModel()
        {

        }

        // User registration
        [Remote(action: nameof(VerifyController.VerifyUniqueUserAsync), controller: "Verify")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [EmailAddress]
        [Remote(action: nameof(VerifyController.VerifyUniqueUserAsync), controller: "Verify")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        // Voter registration
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [StringLength(maximumLength: 256, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Identification")]
        public string Identification { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Social Security Number")]
        [RegularExpression(@"^(\d{3}-\d{2}-\d{4}|XXX-XX-XXXX)$", ErrorMessage = "You must provide a proper social security number seperated by \'-\'.")]
        public string SSNumber { get; set; }

        // Voter address
        [StringLength(maximumLength: 64, ErrorMessage = "Too long")]
        [Display(Name = "Address Line One")]
        public string AddressLineOne { get; set; }

        [StringLength(maximumLength: 64, ErrorMessage = "Too long")]
        [Display(Name = "Address Line Two")]
        public string AddressLineTwo { get; set; }

        [StringLength(maximumLength: 64, ErrorMessage = "Too long")]
        [Display(Name = "City")]
        public string City { get; set; }

        [StringLength(maximumLength: 5, ErrorMessage = "Maximum length of 5")]
        [Display(Name = "Zip Code")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Must be 5 digits")]
        [Remote(action: nameof(VerifyController.VerifyZipExistsAsync), controller: "Verify")]
        public string ZipCode { get; set; }

        [StringLength(maximumLength: 2, ErrorMessage = "Maximum length of 2")]
        [Display(Name = "State")]
        [RegularExpression(@"^(-i:A[LKSZRAEP]|C[AOT]|D[EC]|F[LM]|G[AU]|HI|I[ADLN]|K[SY]|LA|M[ADEHINOPST]|N[CDEHJMVY]|O[HKR]|P[ARW]|RI|S[CD]|T[NX]|UT|V[AIT]|W[AIVY])$", ErrorMessage = "Invalid State (must be uppercase)")]
        public string State { get; set; }

        // Voter demographics
        [Display(Name = "Party")]
        public string Party { get; set; }

        [Display(Name = "Ethnicity")]
        public string Ethnicity { get; set; }

        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [Display(Name = "Income Range")]
        public string IncomeRange { get; set; }

        [Display(Name = "Readiness Level")]
        public string VoterReadiness { get; set; }
    }
}