using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication
{
    public class ApplicationUser : IdentityUser
    {
        // these are properties apply to the users login account not their voting data.
        [MaxLength(256)]
        public string SecurityQuestionOne { get; set; }
        
        [MaxLength(256)]
        public string SecurityQuestionTwo { get; set; }
        
        [MaxLength(256)]
        public string SecurityAnswerOne { get; set; }
        
        [MaxLength(256)]
        public string SecurityAnswerTwo { get; set; }
    }
}
