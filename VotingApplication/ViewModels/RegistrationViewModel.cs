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
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation")]
        public string ConfirmPassword { get; set; }
    }
}
