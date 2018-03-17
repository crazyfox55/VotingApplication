using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.ViewModels
{
    public class ConfirmResetPasswordViewModel
    {
        public enum Status
        {
            Sent,
            Request,
            Error,
        }

        [HiddenInput]
        public Status State { get; set; }

        [Required]
        [EmailAddress]
        [Remote(action: "VerifyEmail", controller: "Authentication")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
