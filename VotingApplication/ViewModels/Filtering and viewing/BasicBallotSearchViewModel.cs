using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VotingApplication.Controllers;

namespace VotingApplication.ViewModels
{
    public class BasicBallotSearchViewModel
    {
        public string ActionViewComponent;

        [HiddenInput]
        public string BallotId { get; set; }

        [Remote(action: nameof(VerifyController.VerifyBallotExistsAsync), controller: "Verify")]
        [StringLength(maximumLength: 64, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Ballot Name")]
        public string BallotName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Election Day")]
        public DateTime? ElectionDay { get; set; }

    }
}
