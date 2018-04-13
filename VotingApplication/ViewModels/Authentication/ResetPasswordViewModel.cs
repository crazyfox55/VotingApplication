using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace VotingApplication.ViewModels
{
    public class ResetPasswordViewModel
    {
        [HiddenInput]
        public string Token { get; set; }

        [HiddenInput]
        public string Email { get; set; }

        [Required]
        [Remote(action: "VerifyPassword", controller: "UserRegistration")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation")]
        public string ConfirmPassword { get; set; }
    }
}
