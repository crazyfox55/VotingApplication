using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VotingApplication.Controllers;

namespace VotingApplication.ViewModels
{
    public class VoterAddressViewModel
    {
        public VoterAddressViewModel()
        {

        }

        public VoterAddressViewModel(VoterAddressDataModel data)
        {
            if (data == null)
                return;
            AddressLineOne = data.AddressLineOne;
            AddressLineTwo = data.AddressLineTwo;
            City = data.City;
            ZipCode = data.ZipCode.ToString();
            State = data.State;
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
        [Remote(action: nameof(VerifyController.VerifyZipExistsAsync), controller: "Verify")]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(maximumLength: 2, ErrorMessage = "Maximum length of 2")]
        [Display(Name = "State")]
        [RegularExpression(@"^(-i:A[LKSZRAEP]|C[AOT]|D[EC]|F[LM]|G[AU]|HI|I[ADLN]|K[SY]|LA|M[ADEHINOPST]|N[CDEHJMVY]|O[HKR]|P[ARW]|RI|S[CD]|T[NX]|UT|V[AIT]|W[AIVY])$", ErrorMessage = "Invalid State")]
        public string State { get; set; }
    }
}
