using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotingApplication.ViewModels;

namespace VotingApplication
{
    public class VoterRegistrationDataModel
    {
        public VoterRegistrationDataModel()
        {

        }

        public VoterRegistrationDataModel(string userId, VoterRegistrationViewModel model)
        {
            UserId = userId;
            FirstName = model.FirstName;
            LastName = model.LastName;
            DOB = model.DOB;
            Identification = model.Identification;
            SSNumber = model.SSNumber;
        }

        public void Update(VoterRegistrationViewModel model)
        {
            FirstName = model.FirstName;
            LastName = model.LastName;
            DOB = model.DOB;
            Identification = model.Identification;
            SSNumber = model.SSNumber;
        }

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
        [Column(TypeName = "Date")]
        public DateTime DOB { get; set; }

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
