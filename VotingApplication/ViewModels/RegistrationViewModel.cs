using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace VotingApplication.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        [Remote(action: "VerifyUser", controller: "Registration")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Remote(action: "VerifyUser", controller: "Registration")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [Remote(action: "VerifyPassword", controller: "Registration")]
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
