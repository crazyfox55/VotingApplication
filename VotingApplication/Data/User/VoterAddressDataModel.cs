using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotingApplication.ViewModels;

namespace VotingApplication
{
    public class VoterAddressDataModel
    {
        public VoterAddressDataModel()
        {

        }

        public VoterAddressDataModel(string userId, VoterAddressViewModel model)
        {
            UserId = userId;
            Update(model);
        }

        public void Update(VoterAddressViewModel model)
        {
            AddressLineOne = model.AddressLineOne;
            AddressLineTwo = model.AddressLineTwo;
            City = model.City;
            ZipCode = int.Parse(model.ZipCode);
            State = model.State;
        }

        [Key]
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        // virtual is required for EF to override these navigation properties
        public virtual ApplicationUser User { get; set; }

        [Required]
        [MaxLength(64)]
        public string AddressLineOne { get; set; }
        
        [MaxLength(64)]
        public string AddressLineTwo { get; set; }

        [Required]
        [MaxLength(64)]
        public string City { get; set; }

        [Required]
        [ForeignKey(nameof(ZipDataModel))]
        public int? ZipCode { get; set; }

        public virtual ZipDataModel Zip { get; set; }

        [Required]
        [MaxLength(2)]
        public string State { get; set; }
    }
}
