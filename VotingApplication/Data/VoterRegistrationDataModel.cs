using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApplication
{
    public class VoterRegistrationDataModel
    {
        [Key]
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }
        
        // virtual is required for EF to override these navigation properties
        public virtual ApplicationUser User { get; set; }

        [Required]
        [MaxLength(64)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(64)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(256)]
        [Display(Name = "Identification")]
        public string Identification { get; set; }

        [Required]
        [MaxLength(11)]
        [Display(Name = "Social Security Number")]
        public string SSNumber { get; set; }

    }
}
