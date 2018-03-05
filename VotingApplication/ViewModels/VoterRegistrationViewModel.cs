using System.ComponentModel.DataAnnotations;

namespace VotingApplication.ViewModels
{
    public class VoterRegistrationViewModel
    {
        [Required]
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Identification")]
        public string Identification { get; set; }

        [Required]
        [StringLength(maximumLength: 9, MinimumLength = 9, ErrorMessage = "Not a valid Social Security Number, must be 9 numbers")]
        [DataType(DataType.Password)]
        [Display(Name = "Social Security Number")]
        public string SSNumber { get; set; }
    }
}
