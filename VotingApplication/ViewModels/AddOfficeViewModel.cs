using System.ComponentModel.DataAnnotations;

namespace VotingApplication.ViewModels
{
    public class AddOfficeViewModel
    {
        [Required]
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Office Title")]
        public string OfficeName { get; set; }

        [Required]
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Description")]
        public string OfficeDescription { get; set; }
    }
}
