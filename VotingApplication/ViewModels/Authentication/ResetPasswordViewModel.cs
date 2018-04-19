using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VotingApplication.Controllers;

namespace VotingApplication.ViewModels
{
    public class ResetPasswordViewModel
    {
        [HiddenInput]
        public string Token { get; set; }

        [HiddenInput]
        public string Email { get; set; }

        [Required]
        [Remote(action: nameof(VerifyController.VerifyStrongPasswordAsync), controller: "Verify")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation")]
        public string ConfirmPassword { get; set; }
    }
}
