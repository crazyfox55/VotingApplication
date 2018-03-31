using System.ComponentModel.DataAnnotations;

namespace VotingApplication.ViewModels
{
    public class DemographicsEntryViewModel
    {
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
        public string ZipCode { get; set; }

        [Required]
        [StringLength(maximumLength: 2, ErrorMessage = "Maximum length of 2")]
        [Display(Name = "State")]
        [RegularExpression(@"^(-i:A[LKSZRAEP]|C[AOT]|D[EC]|F[LM]|G[AU]|HI|I[ADLN]|K[SY]|LA|M[ADEHINOPST]|N[CDEHJMVY]|O[HKR]|P[ARW]|RI|S[CD]|T[NX]|UT|V[AIT]|W[AIVY])$", ErrorMessage = "Invalid State (must be uppercase)")]
        public string State { get; set; }

        [Required]
        [StringLength(maximumLength: 10, ErrorMessage = "Maximum length of 10")]
        [Display(Name = "Date of Birth")]
        [RegularExpression(@"(?=\d)^(?:(?!(?:10\D(?:0?[5-9]|1[0-4])\D(?:1582))|(?:0?9\D(?:0?[3-9]|1[0-3])\D(?:1752)))((?:0?[13578]|1[02])|(?:0?[469]|11)(?!\/31)(?!-31)(?!\.31)|(?:0?2(?=.?(?:(?:29.(?!000[04]|(?:(?:1[^0-6]|[2468][^048]|[3579][^26])00))(?:(?:(?:\d\d)(?:[02468][048]|[13579][26])(?!\x20BC))|(?:00(?:42|3[0369]|2[147]|1[258]|09)\x20BC))))))|(?:0?2(?=.(?:(?:\d\D)|(?:[01]\d)|(?:2[0-8])))))([-.\/])(0?[1-9]|[12]\d|3[01])\2(?!0000)((?=(?:00(?:4[0-5]|[0-3]?\d)\x20BC)|(?:\d{4}(?!\x20BC)))\d{4}(?:\x20BC)?)(?:$|(?=\x20\d)\x20))?((?:(?:0?[1-9]|1[012])(?::[0-5]\d){0,2}(?:\x20[aApP][mM]))|(?:[01]\d|2[0-3])(?::[0-5]\d){1,2})?$", ErrorMessage = "You must provide a proper date of birth: MM/DD/YYYY")]
        public string DOB { get; set; }

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
