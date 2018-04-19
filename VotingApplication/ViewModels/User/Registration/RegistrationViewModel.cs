using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using VotingApplication.Controllers;

namespace VotingApplication.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        [Remote(action: nameof(VerifyController.VerifyUniqueUserAsync), controller: "Verify")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Remote(action: nameof(VerifyController.VerifyUniqueUserAsync), controller: "Verify")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        // This remote forces the client to get a varification on the password they submit.
        // Check out the VerifyPasswork method in the Registration controller for more details.
        [Remote(action: nameof(VerifyController.VerifyStrongPasswordAsync), controller: "Verify")]
        [DataType(DataType.Password)]
        // The password is enforced by the database instead of client side code. 
        // Check out the line 48 of the startup.cs class. There are options to change the strength of password.
        //[RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation")]
        public string ConfirmPassword { get; set; }
    }
}
