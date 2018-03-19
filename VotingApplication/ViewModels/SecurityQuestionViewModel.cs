using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using VotingApplication.CustomAttributes;

namespace VotingApplication.ViewModels
{
    public class SecurityQuestionViewModel
    {
        [Required]
        [StringLength(maximumLength: 256, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Security Question One")]
        public string SecurityQuestionOne { get; set; }

        [Required]
        [StringLength(maximumLength: 256, ErrorMessage = "Maximum length of {0}")]
        [DifferentFrom("SecurityQuestionOne")]
        [Display(Name = "Security Question Two")]
        public string SecurityQuestionTwo { get; set; }

        [Required]
        [StringLength(maximumLength: 256, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Question One Answer")]
        public string SecurityAnswerOne { get; set; }

        [Required]
        [StringLength(maximumLength: 256, ErrorMessage = "Maximum length of {0}")]
        [DifferentFrom("SecurityAnswerOne")]
        [Display(Name = "Question Two Answer")]
        public string SecurityAnswerTwo { get; set; }
    }
}
