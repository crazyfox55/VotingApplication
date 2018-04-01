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
    public class UserQueryViewModel
    {
        [Required]
        [StringLength(maximumLength: 256, ErrorMessage = "Maximum length of {0}")]
        [Display(Name = "Query By")]
        public string Option { get; set; }
    }
}
