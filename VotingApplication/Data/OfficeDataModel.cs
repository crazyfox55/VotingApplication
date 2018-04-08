using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VotingApplication.ViewModels;

namespace VotingApplication
{
    public class OfficeDataModel
    {
        public OfficeDataModel()
        {

        }

        public OfficeDataModel(AddOfficeViewModel model)
        {
            if (model == null)
                return;
            OfficeName = model.OfficeName;
            OfficeDescription = model.OfficeDescription;
        }

        [Key]
        [MaxLength(64)]
        public string OfficeName { get; set; }
        
        [Required]
        [MaxLength(64)]
        public string OfficeDescription { get; set; }

        [Required]
        [MaxLength(64)]
        public string OfficeLevel { get; set; }

        public ICollection<BallotDataModel> Ballot { get; set; }
    }
}
