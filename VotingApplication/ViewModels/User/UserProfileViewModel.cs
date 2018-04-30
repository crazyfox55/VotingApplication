using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VotingApplication.Controllers;
using VotingApplication.CustomAttributes;
using Microsoft.AspNetCore.Identity;
using System;

namespace VotingApplication.ViewModels
{
    

    public class UserProfileViewModel
    {
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

       
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

      
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

      
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

      
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
    }

    
}
