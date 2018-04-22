using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.ViewModels
{
    public class ViewZipViewModel
    {
        public IEnumerable<ZipViewModel> FilteredZips { get; set; }
        public struct ZipViewModel
        {
            [Display(Name = "Zip Code")]
            public int? ZipCode { get; set; }

            [Display(Name = "Primary City")]
            public string PrimaryCity { get; set; }

            public string State { get; set; }

            public string County { get; set; }

            public string Timezone { get; set; }

            public decimal Latitude { get; set; }

            public decimal Longitude { get; set; }
        }
        [HiddenInput]
        public int Page { get; set; }

        [HiddenInput]
        public string District { get; set; }
        [HiddenInput]
        public string User { get; set; }

        [StringLength(maximumLength: 5, ErrorMessage = "Maximum length of 5")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Must be 5 digits")]
        [Remote(action: "VerifyZip", controller: "Admin")]
        [Display(Name = "Zip Code")]
        public int? ZipCode { get; set; }

        [StringLength(maximumLength: 64, ErrorMessage = "Too long")]
        [Display(Name = "Primary City")]
        public string PrimaryCity { get; set; }

        [StringLength(maximumLength: 2, ErrorMessage = "Maximum length of 2")]
        [RegularExpression(@"^(-i:A[LKSZRAEP]|C[AOT]|D[EC]|F[LM]|G[AU]|HI|I[ADLN]|K[SY]|LA|M[ADEHINOPST]|N[CDEHJMVY]|O[HKR]|P[ARW]|RI|S[CD]|T[NX]|UT|V[AIT]|W[AIVY])$", ErrorMessage = "Invalid State (must be uppercase)")]
        public string State { get; set; }

        [StringLength(maximumLength: 64, ErrorMessage = "Too long")]
        public string County { get; set; }

        [StringLength(maximumLength: 64, ErrorMessage = "Too long")]
        public string Timezone { get; set; }
        
        public decimal Latitude { get; set; }
        
        public decimal Longitude { get; set; }
    }
}
