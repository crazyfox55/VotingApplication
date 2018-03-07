using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApplication
{
    public class VoterRegistrationDataModel
    {
        [Key]
        public string Id { get; set; }

        //FixMe TODO
        //[Required]
        //[ForeignKey(nameof(ApplicationUser)+"RefId")]
        //public ApplicationUser Account { get; set; }

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
