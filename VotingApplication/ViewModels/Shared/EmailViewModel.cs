using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VotingApplication.Controllers;

namespace VotingApplication.ViewModels
{
    public class EmailViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: nameof(VerifyController.VerifyEmailExists), controller: "Verify")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
