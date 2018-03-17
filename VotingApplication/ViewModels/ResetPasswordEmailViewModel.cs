using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace VotingApplication.ViewModels
{
    public class ResetPasswordEmailViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "VerifyEmail", controller: "Authentication")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
