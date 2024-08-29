using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotingApplication.ViewModels;

namespace VotingApplication
{
    public class VoterDemographicsDataModel
    {
        public VoterDemographicsDataModel()
        {

        }

        public VoterDemographicsDataModel(string userId, VoterDemographicsViewModel model)
        {
            UserId = userId;
            Update(model);
        }

        public void Update(VoterDemographicsViewModel model)
        {
            Party = model.Party;
            Ethnicity = model.Ethnicity;
            Sex = model.Sex;
            IncomeRange = model.IncomeRange;
            VoterReadiness = model.VoterReadiness;
        }

        [Key]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        // virtual is required for EF to override these navigation properties
        public virtual ApplicationUser User { get; set; }

        [Required]
        [MaxLength(64)]
        public string Party { get; set; }

        [Required]
        [MaxLength(64)]
        public string Ethnicity { get; set; }

        [Required]
        [MaxLength(32)]
        public string Sex { get; set; }

        [Required]
        [MaxLength(64)]
        public string IncomeRange { get; set; }

        [Required]
        [MaxLength(64)]
        public string VoterReadiness { get; set; }
    }
}
