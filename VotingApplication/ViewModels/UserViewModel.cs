using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace VotingApplication.ViewModels
{
    public class UserViewModel
    
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

    }
}
