using System;
using System.ComponentModel.DataAnnotations;

namespace VotingApplication.ViewModels
{
    public class VoterRegistrationViewModel
    {
        public VoterRegistrationViewModel()
        {

        }

        public VoterRegistrationViewModel(VoterRegistrationDataModel data)
        {
            if (data == null)
                return;
            FirstName = data.FirstName;
            LastName = data.LastName;
            DOB = data.DOB;
            Identification = data.Identification;
            SSNumber = data.SSNumber;
        }

        [Required]
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        [StringLength(maximumLength: 256, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Identification")]
        public string Identification { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Social Security Number")]
        [RegularExpression(@"^(\d{3}-\d{2}-\d{4}|XXX-XX-XXXX)$", ErrorMessage = "You must provide a proper social security number seperated by \'-\'.")]
        public string SSNumber { get; set; }
    }
}
