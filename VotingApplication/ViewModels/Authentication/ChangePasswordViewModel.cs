using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VotingApplication.Controllers;
using VotingApplication.CustomAttributes;

namespace VotingApplication.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string Password { get; set; }

        [Required]
        [Remote(action: nameof(UserController.VerifyNewPassword), controller: "User")]
        [DataType(DataType.Password)]
        [DifferentFrom("Password", ErrorMessage = "Cannot be same as old password")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        [DifferentFrom("Password", ErrorMessage = "Cannot be same as old password")]
        [Display(Name = "Confirmation")]
        public string ConfirmPassword { get; set; }
    }
}
