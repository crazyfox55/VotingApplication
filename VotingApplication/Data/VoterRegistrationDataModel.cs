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
        public string FirstName { get; set; }

        [Required]
        [MaxLength(64)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(256)]
        public string Identification { get; set; }

        [Required]
        [MaxLength(10)]
        public string SSNumber { get; set; }

    }
}
