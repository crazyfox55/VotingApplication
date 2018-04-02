using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VotingApplication.ViewModels;

namespace VotingApplication
{
    public class CandidateDataModel
    {
        public CandidateDataModel()
        {

        }

        public CandidateDataModel(AddCandidateViewModel model)
        {
            if (model == null)
                return;
            OfficeName = model.OfficeName;
            FirstName = model.FirstName;
            LastName = model.LastName;
            Party = model.Party;
            DOB = model.DOB;
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [ForeignKey(nameof(OfficeDataModel))]
        public string OfficeName { get; set; }

        public virtual OfficeDataModel Office { get; set; }

        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(64)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(64)]
        public string Party { get; set; }

        [Required]
        [MaxLength(10)]
        public string DOB { get; set; }
    }
}
